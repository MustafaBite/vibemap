using System;
using System.Collections.Generic;
using System.Linq;
using VibeMap.Models;

namespace VibeMap.Services
{
    /// <summary>
    /// Smart Recommendation Engine - Local, offline mood analysis and genre matching system.
    /// Uses keyword scoring, weighted matching, and mood-genre mapping tables.
    /// </summary>
    public static class SmartRecommendationEngine
    {
        #region Keyword Scoring Tables

        // Keyword weights for different mood dimensions
        private static readonly Dictionary<string, MoodScores> KeywordScores = new Dictionary<string, MoodScores>(StringComparer.OrdinalIgnoreCase)
        {
            // Mutlu / Keyifli - Happy/Pleasant
            { "mutlu", new MoodScores { Happiness = 20, Energy = 5 } },
            { "keyifli", new MoodScores { Happiness = 18, Calmness = 10 } },
            
            // Üzgün - Sad
            { "üzgün", new MoodScores { Tension = 20, Darkness = 18, Happiness = -15 } },
            
            // Yorgun - Tired
            { "yorgun", new MoodScores { Tension = 12, Energy = -20, Calmness = 15 } },
            
            // Stresli - Stressed
            { "stresli", new MoodScores { Tension = 25, Darkness = 15, Energy = -5 } },
            
            // Yalnız - Lonely
            { "yalnız", new MoodScores { Tension = 18, Darkness = 20, Happiness = -10 } },
            
            // Sıkılmış - Bored
            { "sıkılmış", new MoodScores { Tension = 8, Energy = -10, Excitement = -15 } },
            
            // Heyecanlı - Excited
            { "heyecanlı", new MoodScores { Energy = 20, Excitement = 25, Happiness = 12 } },
            
            // Öfkeli - Angry
            { "öfkeli", new MoodScores { Tension = 28, Darkness = 22, Energy = 18 } }
        };

        #endregion

        #region Mood-to-Genre Mapping

        // Genre recommendations based on mood profile
        private static readonly Dictionary<string, GenreMapping> GenreMappings = new Dictionary<string, GenreMapping>
        {
            // MOVIES
            { "FİLMLER_Happy", new GenreMapping { RequiredMood = "Happy", Genres = new[] { "Komedi", "Romantik Komedi", "Aile", "Animasyon", "Macera" } } },
            { "FİLMLER_Energetic", new GenreMapping { RequiredMood = "Energetic", Genres = new[] { "Aksiyon", "Macera", "Bilim Kurgu", "Süper Kahraman", "Gerilim" } } },
            { "FİLMLER_Calm", new GenreMapping { RequiredMood = "Calm", Genres = new[] { "Drama", "Romantik", "Belgesel", "Fantastik Drama", "Sanat Filmi" } } },
            { "FİLMLER_Tense", new GenreMapping { RequiredMood = "Tense", Genres = new[] { "Gerilim", "Psikolojik Gerilim", "Korku", "Drama", "Gizem" } } },
            { "FİLMLER_Dark", new GenreMapping { RequiredMood = "Dark", Genres = new[] { "Korku", "Psikolojik Gerilim", "Noir", "Drama", "Distopya" } } },
            
            // SERIES
            { "DİZİLER_Happy", new GenreMapping { RequiredMood = "Happy", Genres = new[] { "Komedi", "Sitcom", "Romantik Komedi", "Aile", "Hafif Drama" } } },
            { "DİZİLER_Energetic", new GenreMapping { RequiredMood = "Energetic", Genres = new[] { "Aksiyon", "Macera", "Bilim Kurgu", "Fantastik", "Gerilim" } } },
            { "DİZİLER_Calm", new GenreMapping { RequiredMood = "Calm", Genres = new[] { "Drama", "Romantik", "Belgesel", "Slice of Life", "Doğa" } } },
            { "DİZİLER_Tense", new GenreMapping { RequiredMood = "Tense", Genres = new[] { "Gerilim", "Suç", "Gizem", "Drama", "Psikolojik" } } },
            { "DİZİLER_Dark", new GenreMapping { RequiredMood = "Dark", Genres = new[] { "Korku", "Psikolojik", "Noir", "Suç", "Distopya" } } },
            
            // GAMES
            { "OYUNLAR_Happy", new GenreMapping { RequiredMood = "Happy", Genres = new[] { "Platform", "Parti Oyunu", "Simülasyon", "Casual", "Multiplayer" } } },
            { "OYUNLAR_Energetic", new GenreMapping { RequiredMood = "Energetic", Genres = new[] { "Aksiyon", "FPS", "Yarış", "Dövüş", "Battle Royale" } } },
            { "OYUNLAR_Calm", new GenreMapping { RequiredMood = "Calm", Genres = new[] { "Puzzle", "Strateji", "Simülasyon", "Indie", "Hikaye Odaklı" } } },
            { "OYUNLAR_Tense", new GenreMapping { RequiredMood = "Tense", Genres = new[] { "Survival", "Korku", "Gerilim", "Stealth", "RPG" } } },
            { "OYUNLAR_Dark", new GenreMapping { RequiredMood = "Dark", Genres = new[] { "Korku", "Psikolojik Korku", "Survival Horror", "Dark Fantasy", "Souls-like" } } }
        };

        #endregion

        #region Public API

        /// <summary>
        /// Analyzes user mood text and returns structured mood analysis with genre recommendations.
        /// Pure local logic - no external API calls.
        /// </summary>
        /// <param name="userMoodText">Free-text mood description from user (can include emojis)</param>
        /// <param name="category">Content category (OYUNLAR, DİZİLER, FİLMLER)</param>
        /// <returns>Structured mood analysis result</returns>
        public static MoodAnalysisResult AnalyzeMood(string userMoodText, string category)
        {
            if (string.IsNullOrWhiteSpace(userMoodText))
            {
                return GetDefaultMoodResult();
            }

            // Step 1: Extract and score keywords
            var moodScores = CalculateMoodScores(userMoodText);

            // Step 2: Detect primary mood dimensions
            var primaryMoods = DetectPrimaryMoods(moodScores);

            // Step 3: Determine energy level
            string energyLevel = DetermineEnergyLevel(moodScores);

            // Step 4: Map to genres based on category and mood
            var genres = MapMoodsToGenres(primaryMoods, category);

            return new MoodAnalysisResult
            {
                Mood = primaryMoods,
                EnergyLevel = energyLevel,
                PreferredGenres = genres
            };
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Calculates weighted mood scores from user input text.
        /// </summary>
        private static MoodScores CalculateMoodScores(string text)
        {
            var totalScores = new MoodScores();
            var words = text.ToLower().Split(new[] { ' ', ',', '.', '!', '?', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                // Direct keyword match
                if (KeywordScores.TryGetValue(word, out var scores))
                {
                    totalScores.Add(scores);
                    continue;
                }

                // Partial match (for composite words or emojis within text)
                foreach (var kvp in KeywordScores)
                {
                    if (word.Contains(kvp.Key) || kvp.Key.Contains(word))
                    {
                        totalScores.Add(kvp.Value, 0.7); // Reduced weight for partial matches
                    }
                }
            }

            return totalScores;
        }

        /// <summary>
        /// Detects the dominant mood dimensions from calculated scores.
        /// </summary>
        private static List<string> DetectPrimaryMoods(MoodScores scores)
        {
            var moods = new List<string>();

            // Threshold-based detection with weighted priority
            if (scores.Happiness > 15) moods.Add("Mutlu");
            if (scores.Energy > 15) moods.Add("Enerjik");
            if (scores.Calmness > 20) moods.Add("Sakin");
            if (scores.Tension > 15) moods.Add("Gergin");
            if (scores.Darkness > 15) moods.Add("Karanlık");
            if (scores.Excitement > 15) moods.Add("Heyecanlı");
            if (scores.Focus > 18) moods.Add("Odaklı");

            // Fallback: if no strong mood detected, pick highest score
            if (moods.Count == 0)
            {
                var max = Math.Max(scores.Happiness, Math.Max(scores.Energy, Math.Max(scores.Calmness, scores.Tension)));
                if (max == scores.Happiness) moods.Add("Mutlu");
                else if (max == scores.Energy) moods.Add("Enerjik");
                else if (max == scores.Calmness) moods.Add("Sakin");
                else moods.Add("Gergin");
            }

            return moods;
        }

        /// <summary>
        /// Determines energy level based on mood scores.
        /// </summary>
        private static string DetermineEnergyLevel(MoodScores scores)
        {
            double energyScore = scores.Energy + scores.Excitement - (scores.Calmness * 0.5) - (scores.Darkness * 0.3);

            if (energyScore > 20) return "high";
            if (energyScore < 5) return "low";
            return "medium";
        }

        /// <summary>
        /// Maps detected moods to appropriate content genres.
        /// </summary>
        private static List<string> MapMoodsToGenres(List<string> primaryMoods, string category)
        {
            var genres = new HashSet<string>();

            foreach (var mood in primaryMoods)
            {
                // Try to map mood to genre
                string key = $"{category}_{GetMoodCategory(mood)}";
                if (GenreMappings.TryGetValue(key, out var mapping))
                {
                    foreach (var genre in mapping.Genres)
                    {
                        genres.Add(genre);
                    }
                }
            }

            // Fallback to default category genres
            if (genres.Count == 0)
            {
                string fallbackKey = $"{category}_Happy";
                if (GenreMappings.TryGetValue(fallbackKey, out var fallbackMapping))
                {
                    foreach (var genre in fallbackMapping.Genres)
                    {
                        genres.Add(genre);
                    }
                }
            }

            return genres.ToList();
        }

        /// <summary>
        /// Maps Turkish mood tags to English mood categories for genre mapping.
        /// </summary>
        private static string GetMoodCategory(string mood)
        {
            switch (mood)
            {
                case "Mutlu": return "Happy";
                case "Enerjik": return "Energetic";
                case "Heyecanlı": return "Energetic";
                case "Sakin": return "Calm";
                case "Gergin": return "Tense";
                case "Karanlık": return "Dark";
                case "Odaklı": return "Calm";
                default: return "Happy";
            }
        }

        /// <summary>
        /// Returns a default mood result for edge cases.
        /// </summary>
        private static MoodAnalysisResult GetDefaultMoodResult()
        {
            return new MoodAnalysisResult
            {
                Mood = new List<string> { "Mutlu" },
                EnergyLevel = "medium",
                PreferredGenres = new List<string> { "Komedi", "Drama", "Aksiyon" }
            };
        }

        #endregion

        #region Helper Classes

        /// <summary>
        /// Represents multi-dimensional mood scores.
        /// </summary>
        private class MoodScores
        {
            public double Happiness { get; set; }
            public double Energy { get; set; }
            public double Tension { get; set; }
            public double Darkness { get; set; }
            public double Calmness { get; set; }
            public double Excitement { get; set; }
            public double Focus { get; set; }

            public void Add(MoodScores other, double weight = 1.0)
            {
                Happiness += other.Happiness * weight;
                Energy += other.Energy * weight;
                Tension += other.Tension * weight;
                Darkness += other.Darkness * weight;
                Calmness += other.Calmness * weight;
                Excitement += other.Excitement * weight;
                Focus += other.Focus * weight;
            }
        }

        /// <summary>
        /// Maps mood to genres for a specific category.
        /// </summary>
        private class GenreMapping
        {
            public string RequiredMood { get; set; }
            public string[] Genres { get; set; }
        }

        #endregion
    }
}
