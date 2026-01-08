using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using VibeMap.Utils;

namespace VibeMap.Forms
{
    public partial class FrmRecommendation : XtraForm
    {
        private string _link;
        private string _username;
        private string _category;
        private string _title;

        public FrmRecommendation(string username, string category, string title, string description, string imagePath, string link)
        {
            InitializeComponent();
            ThemeManager.ApplyGlobalBackground(this);
            ThemeManager.ApplyTheme(this, ThemeManager.CurrentTheme);
            
            _username = username;
            _category = category;
            _title = title;
            _link = link;

            lblTitle.Text = title;
            lblDescription.Text = description;
            
            LoadImage(imagePath);
        }

        private void LoadImage(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            try
            {
                if (path.StartsWith("http"))
                {
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        byte[] data = wc.DownloadData(path);
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream(data))
                        {
                            picHero.Image = Image.FromStream(ms);
                        }
                    }
                }
                else if (System.IO.File.Exists(path))
                {
                    picHero.Image = Image.FromFile(path);
                }
            }
            catch
            {
                // Fallback or placeholder
            }
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_link))
            {
                try
                {
                    // Track as "İzlenenler" automatically when opening the link
                    UpdateUserAction("İzlenenler");
                    Process.Start(new ProcessStartInfo(_link) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Link açılamadı: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTrackAction_Click(object sender, EventArgs e)
        {
            if (!(sender is SimpleButton btn)) return;

            string status = "Çöp";
            if (btn == btnWatched) status = "İzlenenler";
            if (btn == btnWishlist) status = "Daha Sonra";

            if (UpdateUserAction(status))
            {
                string msg = status == "Daha Sonra" ? "Daha Sonra listesine eklendi!" : 
                             status == "İzlenenler" ? "Aktivite Geçmişine eklendi!" : 
                             "Seçiminiz kaydedildi.";

                XtraMessageBox.Show(msg, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool UpdateUserAction(string status)
        {
            try
            {
                using (var con = DbConnection.GetUserConnection())
                {
                    con.Open();
                    using (var cmd = new System.Data.SQLite.SQLiteCommand(con))
                    {
                        // Enforce one-list-only: Remove any existing tracking for this title/user
                        cmd.CommandText = "DELETE FROM UserActions WHERE Username = @u AND Title = @t";
                        cmd.Parameters.AddWithValue("@u", _username);
                        cmd.Parameters.AddWithValue("@t", _title);
                        cmd.ExecuteNonQuery();

                        // Now insert the new status
                        cmd.CommandText = "INSERT INTO UserActions (Username, Category, Title, Status, Timestamp) VALUES (@u, @c, @t, @s, @tm)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@u", _username);
                        cmd.Parameters.AddWithValue("@c", _category);
                        cmd.Parameters.AddWithValue("@t", _title);
                        cmd.Parameters.AddWithValue("@s", status);
                        cmd.Parameters.AddWithValue("@tm", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
