using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;
using VibeMap.Utils;
using System.Data.SQLite;
using System.IO;

using VibeMap.Services;

namespace VibeMap.Forms
{
    public partial class FrmHome : XtraForm
    {
        private string _username;
        private string selectedCategory = "FİLMLER";
        private bool _isChangingSelection = false;
        private PanelControl pnlNavMenu; // Container for centering nav items

        public FrmHome(string username)
        {
            InitializeComponent();
            _username = username;
            ThemeManager.ApplyGlobalBackground(this);
            LoadUserProfile();
            SetupNavigationContainer();
            ThemeManager.ApplyTheme(this, ThemeManager.CurrentTheme);
            btnThemeToggle.Text = ThemeManager.CurrentTheme == ThemeManager.ThemeMode.Dark ? "🌙" : "☀️";
            this.Resize += FrmHome_Resize;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            FrmHome_Resize(null, null);
        }

        private void SetupNavigationContainer()
        {
            pnlNavMenu = new PanelControl();
            pnlNavMenu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pnlNavMenu.BackColor = Color.Transparent;
            pnlNavMenu.Height = pnlTopNav.Height;
            pnlNavMenu.Tag = "KeepTransparent"; // Hint for ThemeManager maybe? (Optional)
            
            // Collect nav labels (order matters for visual flow)
            LabelControl[] navLabels = { lblNavHome, lblNavEverything, lblNavOldRecs, lblNavWatched, lblNavWishlist, lblNavSettings };
            
            int currentX = 0;
            int spacing = 30;

            foreach (var lbl in navLabels)
            {
                lbl.Parent = pnlNavMenu;
                lbl.Location = new Point(currentX, 35); // Keep original Y
                currentX += lbl.Width + spacing;
            }

            pnlNavMenu.Width = currentX - spacing;
            pnlTopNav.Controls.Add(pnlNavMenu);
            
            // Move theme toggle to the far right relative to screen or keep it in nav?
            // Let's keep it on the far right of the top bar for better UX
            btnThemeToggle.Parent = pnlTopNav; 
        }

        private void FrmHome_Resize(object sender, EventArgs e)
        {
            // Center Navigation Menu
            if (pnlNavMenu != null) LayoutHelper.CenterHorizontal(pnlNavMenu);

            // Theme toggle to far right
            btnThemeToggle.Left = pnlTopNav.Width - btnThemeToggle.Width - 20;

            // Center Categories Panel
            LayoutHelper.CenterHorizontal(pnlCategories, 100);

            // Center Hero Image relative to window
            LayoutHelper.CenterHorizontal(picHeroMain, pnlCategories.Bottom + 5);

            // Center SELECT button if visible
            LayoutHelper.CenterHorizontal(btnMainSelect, picHeroMain.Bottom + 5);
            
            // Robot position (dynamic right-side sticking)
            picRobot.Left = this.ClientSize.Width - picRobot.Width - 100;
            picRobot.Top = this.ClientSize.Height - picRobot.Height - 150;

            // Magnified panel centering
            if (pnlMagnified.Visible) LayoutHelper.CenterControl(pnlMagnified);
        }

        private void LoadUserProfile()
        {
            SetDefaultProfilePicture(); // Fallback
            lblUsername.Text = _username; // Default to username

            try
            {
                using (var con = DbConnection.GetUserConnection())
                {
                    con.Open();
                    using (var cmd = new SQLiteCommand("SELECT ProfilePicturePath, DisplayName FROM Users WHERE Username = @u", con))
                    {
                        cmd.Parameters.AddWithValue("@u", _username);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Load Picture
                                string path = reader["ProfilePicturePath"]?.ToString();
                                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                                {
                                    try { picProfile.Image = Image.FromFile(path); } catch { }
                                }

                                // Load Name
                                string displayName = reader["DisplayName"]?.ToString();
                                if (!string.IsNullOrEmpty(displayName))
                                {
                                    lblUsername.Text = displayName;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Simple warning, don't crash the whole app just for profile info
                Console.WriteLine("Could not load full profile: " + ex.Message);
            }
        }

        private void SetDefaultProfilePicture()
        {
            string brainPath = @"C:\Users\MSİ\.gemini\antigravity\brain\cd1dad2d-f6ca-4c3e-a029-f51795f5fae9\";

            string avatarPath = Path.Combine(brainPath, "human_default_avatar_1766822941858.png");
            if (File.Exists(avatarPath))
            {
                picProfile.Image = Image.FromFile(avatarPath);
            }

            // --- HERO IMAGE ---
            SyncCategoryUI();

            // --- ROBOT IMAGE ---
            string robotPath = @"C:\Users\MSİ\.gemini\antigravity\brain\703709a7-ea6d-4f33-b648-39208d596c61\uploaded_image_1767213944680.png";
            if (File.Exists(robotPath))
            {
                try
                {
                    Bitmap bmp = new Bitmap(robotPath);
                    bmp.MakeTransparent(Color.White); // Match the generated asset's background
                    picRobot.Image = bmp;
                }
                catch { picRobot.Image = Image.FromFile(robotPath); }
            }
            picRobot.BackColor = Color.Transparent;

            // Ensure initial sync
            SyncCategoryUI();
        }

        private int _robotOffset = 0;
        private int _robotStep = 1;

        private void tmrRobot_Tick(object sender, EventArgs e)
        {
            _robotOffset += _robotStep;
            if (Math.Abs(_robotOffset) > 10) _robotStep *= -1;
            picRobot.Top += _robotStep;
        }

        private void CategoryCheck_Changed(object sender, EventArgs e)
        {
            // Simply trigger sync when any check state changes
            if (_isChangingSelection) return;
            SyncCategoryUI();
        }

        private void SyncCategoryUI()
        {
            _isChangingSelection = true;
            try
            {
                // 1. Resolve active state from controls (Source of Truth)
                if (chkGames.Checked) selectedCategory = "OYUNLAR";
                else if (chkSeries.Checked) selectedCategory = "DİZİLER";
                else if (chkMovies.Checked) selectedCategory = "FİLMLER";

                // 2. Update Hero Image
                string brainPath = @"C:\Users\MSİ\.gemini\antigravity\brain\1ea163e8-df9e-4d55-8390-d996c366e94c\";
                string fileName = "";

                if (selectedCategory == "OYUNLAR") fileName = "uploaded_image_1_1767278774795.png";
                else if (selectedCategory == "DİZİLER") fileName = "uploaded_image_0_1767278774795.png";
                else if (selectedCategory == "FİLMLER") fileName = "uploaded_image_2_1767278774795.png";

                string fullPath = Path.Combine(brainPath, fileName);
                if (File.Exists(fullPath))
                {
                    picHeroMain.Image = Image.FromFile(fullPath);
                }

                // 3. Update SELECT Button Visibility
                btnSelectGames.Visible = (selectedCategory == "OYUNLAR");
                btnSelectSeries.Visible = (selectedCategory == "DİZİLER");
                btnSelectMovies.Visible = (selectedCategory == "FİLMLER");
            }
            finally
            {
                _isChangingSelection = false;
            }
        }


        private void ApplyCosmicBackground() { }

        private void FrmHome_Load(object sender, EventArgs e)
        {
        }

        private void GenreButton_Click(object sender, EventArgs e)
        {
            using (FrmMood frmMood = new FrmMood())
            {
                if (frmMood.ShowDialog() == DialogResult.OK)
                {
                    string mood = frmMood.UserMood;
                    var rec = RecommendationService.GetRecommendation(_username, selectedCategory, mood);
                    
                    if (rec != null)
                    {
                        using (var frmRec = new FrmRecommendation(_username, selectedCategory, rec.Title, rec.Description, rec.ImagePath, rec.ActionLink))
                        {
                            frmRec.ShowDialog();
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show($"Henüz bu ruh hali için bir önerimiz yok, ancak seçiminiz kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void lblNavSettings_Click(object sender, EventArgs e)
        {
            pnlSettings.Visible = !pnlSettings.Visible;
            if (pnlSettings.Visible)
            {
                pnlSettings.BringToFront();
            }
        }

        private void lblNavEverything_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmCatalog(_username))
            {
                frm.ShowDialog();
            }
        }

        private void lblNavList_Click(object sender, EventArgs e)
        {
            if (!(sender is LabelControl lbl)) return;

            string filter = "Daha Sonra";
            string title = "Daha Sonra";

            if (lbl == lblNavWatched)
            {
                filter = "İzlenenler";
                title = "Aktivite Geçmişi";
            }
            else if (lbl == lblNavOldRecs)
            {
                filter = "Çöp";
                title = "Çöp";
            }

            using (var frm = new FrmContentList(_username, filter, title))
            {
                frm.ShowDialog();
            }
        }

        private void lblChangePicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Select Profile Picture";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string uploadsDir = Path.Combine(Application.StartupPath, "Uploads");
                        if (!Directory.Exists(uploadsDir)) Directory.CreateDirectory(uploadsDir);

                        string ext = Path.GetExtension(ofd.FileName);
                        string newFileName = $"profile_{_username}_{DateTime.Now.Ticks}{ext}";
                        string destPath = Path.Combine(uploadsDir, newFileName);

                        File.Copy(ofd.FileName, destPath, true);

                        // Update database
                        using (var con = DbConnection.GetUserConnection())
                        {
                            con.Open();
                            using (var cmd = new SQLiteCommand("UPDATE Users SET ProfilePicturePath = @p WHERE Username = @u", con))
                            {
                                cmd.Parameters.AddWithValue("@p", destPath);
                                cmd.Parameters.AddWithValue("@u", _username);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        picProfile.Image = Image.FromFile(destPath);
                        XtraMessageBox.Show("Profil fotoğrafı güncellendi ve kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnlSettings.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void lblChangeName_Click(object sender, EventArgs e)
        {
            string newName = XtraInputBox.Show("Yeni isminizi girin:", "Profil İsmini Değiştir", lblUsername.Text);
            if (!string.IsNullOrEmpty(newName))
            {
                try
                {
                    using (var con = DbConnection.GetUserConnection())
                    {
                        con.Open();
                        using (var cmd = new SQLiteCommand("UPDATE Users SET DisplayName = @n WHERE Username = @u", con))
                        {
                            cmd.Parameters.AddWithValue("@n", newName);
                            cmd.Parameters.AddWithValue("@u", _username);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    lblUsername.Text = newName;
                    XtraMessageBox.Show("Profil ismi başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pnlSettings.Visible = false;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void picProfile_Click(object sender, EventArgs e)
        {
            if (picProfile.Image != null)
            {
                picLargeProfile.Image = picProfile.Image;
                pnlMagnified.Visible = true;
                LayoutHelper.CenterControl(pnlMagnified);
                pnlMagnified.BringToFront();
            }
        }

        private void pnlMagnified_Click(object sender, EventArgs e)
        {
            pnlMagnified.Visible = false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Ensure the entire application exits when the Home form is closed
            Application.Exit();
        }

        private void btnThemeToggle_Click(object sender, EventArgs e)
        {
            ThemeManager.ToggleTheme(this);
            btnThemeToggle.Text = ThemeManager.CurrentTheme == ThemeManager.ThemeMode.Dark ? "🌙" : "☀️";
        }
    }
}
