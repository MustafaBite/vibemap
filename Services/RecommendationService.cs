using System;
using System.Collections.Generic;
using System.Linq;
using VibeMap.Utils;
using System.Data.SQLite;

namespace VibeMap.Services
{
    public class Recommendation
    {
        public string Category { get; set; }
        public string Mood { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string ActionLink { get; set; }
    }

    public static class RecommendationService
    {
        private static Random _rng = new Random();

        public static List<Recommendation> GetAllRecommendations()
        {
            var list = new List<Recommendation>();
            try
            {
                using (var con = DbConnection.GetCatalogConnection())
                {
                    con.Open();
                    using (var cmd = new SQLiteCommand("SELECT * FROM Recommendations", con))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Recommendation
                                {
                                    Category = reader["Category"].ToString(),
                                    Mood = reader["Mood"].ToString(),
                                    Title = reader["Title"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    ImagePath = reader["ImagePath"].ToString(),
                                    ActionLink = reader["ActionLink"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch { /* Log error */ }
            return list;
        }

        public static Recommendation GetRecommendation(string username, string category, string mood)
        {
            // 1. Get tracked titles
            var trackedTitles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                using (var con = DbConnection.GetUserConnection())
                {
                    con.Open();
                    using (var cmd = new SQLiteCommand("SELECT Title FROM UserActions WHERE Username = @u", con))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read()) trackedTitles.Add(reader["Title"].ToString());
                        }
                    }
                }
            }
            catch { }

            // 2. Use Smart Recommendation Engine to analyze mood
            var moodAnalysis = SmartRecommendationEngine.AnalyzeMood(mood, category);
            
            // Extract mood tags for database matching
            var targetMoods = new HashSet<string>(moodAnalysis.Mood, StringComparer.OrdinalIgnoreCase);
            
            // Add broader mood categories for better matching
            foreach (var detectedMood in moodAnalysis.Mood)
            {
                // Map to database mood categories
                if (detectedMood.Equals("Mutlu", StringComparison.OrdinalIgnoreCase) || 
                    detectedMood.Equals("Heyecanlı", StringComparison.OrdinalIgnoreCase))
                {
                    targetMoods.Add("Mutlu");
                }
                
                if (detectedMood.Equals("Enerjik", StringComparison.OrdinalIgnoreCase) || 
                    detectedMood.Equals("Heyecanlı", StringComparison.OrdinalIgnoreCase))
                {
                    targetMoods.Add("Enerjik");
                }
                
                if (detectedMood.Equals("Gergin", StringComparison.OrdinalIgnoreCase) || 
                    detectedMood.Equals("Karanlık", StringComparison.OrdinalIgnoreCase) ||
                    detectedMood.Equals("Sakin", StringComparison.OrdinalIgnoreCase))
                {
                    targetMoods.Add("Gergin");
                }
            }

            if (targetMoods.Count == 0) targetMoods.Add("Mutlu"); // Default fallback

            // 3. Fetch from DB with strict image and category filter
            var filteredRecs = new List<Recommendation>();
            try
            {
                using (var con = DbConnection.GetCatalogConnection())
                {
                    con.Open();
                    // CRITICAL: Strict image filter added here
                    string query = @"SELECT * FROM Recommendations 
                                     WHERE Category = @c 
                                     AND ImagePath IS NOT NULL 
                                     AND ImagePath != '' 
                                     AND ImagePath != 'No image data'
                                     AND ImagePath NOT LIKE '%placeholder%'";

                    using (var cmd = new SQLiteCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@c", category);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string title = reader["Title"].ToString();
                                if (trackedTitles.Contains(title)) continue;

                                filteredRecs.Add(new Recommendation
                                {
                                    Category = reader["Category"].ToString(),
                                    Mood = reader["Mood"].ToString(),
                                    Title = title,
                                    Description = reader["Description"].ToString(),
                                    ImagePath = reader["ImagePath"].ToString(),
                                    ActionLink = reader["ActionLink"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch { }

            if (filteredRecs.Count == 0) return null;

            // 4. Match any target mood
            var moodMatches = filteredRecs.Where(r => targetMoods.Contains(r.Mood)).ToList();
            if (moodMatches.Count > 0) return moodMatches[_rng.Next(moodMatches.Count)];

            // Fallback (still within correct category and has image)
            return filteredRecs[_rng.Next(filteredRecs.Count)];
        }
    }
}
