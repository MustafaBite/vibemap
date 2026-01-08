namespace VibeMap.Forms
{
    partial class FrmHome
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlTopNav = new DevExpress.XtraEditors.PanelControl();
            this.btnThemeToggle = new DevExpress.XtraEditors.SimpleButton();
            this.lblNavSettings = new DevExpress.XtraEditors.LabelControl();
            this.lblNavWishlist = new DevExpress.XtraEditors.LabelControl();
            this.lblNavWatched = new DevExpress.XtraEditors.LabelControl();
            this.pnlProfile = new DevExpress.XtraEditors.PanelControl();
            this.lblUsername = new DevExpress.XtraEditors.LabelControl();
            this.picProfile = new DevExpress.XtraEditors.PictureEdit();
            this.lblNavOldRecs = new DevExpress.XtraEditors.LabelControl();
            this.lblNavEverything = new DevExpress.XtraEditors.LabelControl();
            this.lblNavHome = new DevExpress.XtraEditors.LabelControl();
            this.pnlSettings = new DevExpress.XtraEditors.PanelControl();
            this.lblChangePicture = new DevExpress.XtraEditors.LabelControl();
            this.lblChangeName = new DevExpress.XtraEditors.LabelControl();
            this.pnlMagnified = new DevExpress.XtraEditors.PanelControl();
            this.picLargeProfile = new DevExpress.XtraEditors.PictureEdit();
            this.pnlCategories = new DevExpress.XtraEditors.PanelControl();
            this.btnSelectMovies = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectSeries = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectGames = new DevExpress.XtraEditors.SimpleButton();
            this.chkMovies = new DevExpress.XtraEditors.CheckEdit();
            this.chkSeries = new DevExpress.XtraEditors.CheckEdit();
            this.chkGames = new DevExpress.XtraEditors.CheckEdit();
            this.picHeroMain = new DevExpress.XtraEditors.PictureEdit();
            this.btnMainSelect = new DevExpress.XtraEditors.SimpleButton();
            this.picRobot = new DevExpress.XtraEditors.PictureEdit();
            this.tmrRobot = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopNav)).BeginInit();
            this.pnlTopNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProfile)).BeginInit();
            this.pnlProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProfile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSettings)).BeginInit();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMagnified)).BeginInit();
            this.pnlMagnified.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLargeProfile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCategories)).BeginInit();
            this.pnlCategories.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkMovies.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSeries.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGames.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeroMain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRobot.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTopNav
            // 
            this.pnlTopNav.Controls.Add(this.btnThemeToggle);
            this.pnlTopNav.Controls.Add(this.lblNavSettings);
            this.pnlTopNav.Controls.Add(this.lblNavWishlist);
            this.pnlTopNav.Controls.Add(this.lblNavWatched);
            this.pnlTopNav.Controls.Add(this.pnlProfile);
            this.pnlTopNav.Controls.Add(this.lblNavOldRecs);
            this.pnlTopNav.Controls.Add(this.lblNavEverything);
            this.pnlTopNav.Controls.Add(this.lblNavHome);
            this.pnlTopNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopNav.Location = new System.Drawing.Point(0, 0);
            this.pnlTopNav.Name = "pnlTopNav";
            this.pnlTopNav.Size = new System.Drawing.Size(1035, 85);
            this.pnlTopNav.TabIndex = 0;
            // 
            // btnThemeToggle
            // 
            this.btnThemeToggle.Location = new System.Drawing.Point(953, 50);
            this.btnThemeToggle.Name = "btnThemeToggle";
            this.btnThemeToggle.Size = new System.Drawing.Size(50, 30);
            this.btnThemeToggle.TabIndex = 5;
            this.btnThemeToggle.Text = "🌙";
            this.btnThemeToggle.Click += new System.EventHandler(this.btnThemeToggle_Click);
            // 
            // lblNavSettings
            // 
            this.lblNavSettings.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNavSettings.Appearance.Options.UseFont = true;
            this.lblNavSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNavSettings.Location = new System.Drawing.Point(872, 35);
            this.lblNavSettings.Name = "lblNavSettings";
            this.lblNavSettings.Size = new System.Drawing.Size(58, 23);
            this.lblNavSettings.TabIndex = 4;
            this.lblNavSettings.Text = "Ayarlar";
            this.lblNavSettings.Click += new System.EventHandler(this.lblNavSettings_Click);
            // 
            // lblNavWishlist
            // 
            this.lblNavWishlist.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNavWishlist.Appearance.Options.UseFont = true;
            this.lblNavWishlist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNavWishlist.Location = new System.Drawing.Point(740, 35);
            this.lblNavWishlist.Name = "lblNavWishlist";
            this.lblNavWishlist.Size = new System.Drawing.Size(92, 23);
            this.lblNavWishlist.TabIndex = 3;
            this.lblNavWishlist.Text = "Daha Sonra";
            this.lblNavWishlist.Click += new System.EventHandler(this.lblNavList_Click);
            // 
            // lblNavWatched
            // 
            this.lblNavWatched.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNavWatched.Appearance.Options.UseFont = true;
            this.lblNavWatched.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNavWatched.Location = new System.Drawing.Point(566, 35);
            this.lblNavWatched.Name = "lblNavWatched";
            this.lblNavWatched.Size = new System.Drawing.Size(131, 23);
            this.lblNavWatched.TabIndex = 2;
            this.lblNavWatched.Text = "Aktivite Geçmişi";
            this.lblNavWatched.Click += new System.EventHandler(this.lblNavList_Click);
            // 
            // pnlProfile
            // 
            this.pnlProfile.Controls.Add(this.lblUsername);
            this.pnlProfile.Controls.Add(this.picProfile);
            this.pnlProfile.Location = new System.Drawing.Point(12, 5);
            this.pnlProfile.Name = "pnlProfile";
            this.pnlProfile.Size = new System.Drawing.Size(204, 80);
            this.pnlProfile.TabIndex = 1;
            // 
            // lblUsername
            // 
            this.lblUsername.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblUsername.Appearance.Options.UseFont = true;
            this.lblUsername.Location = new System.Drawing.Point(80, 25);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(120, 28);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Kullanıcı Adı";
            // 
            // picProfile
            // 
            this.picProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picProfile.Location = new System.Drawing.Point(10, 10);
            this.picProfile.Name = "picProfile";
            this.picProfile.Properties.ShowMenu = false;
            this.picProfile.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picProfile.Size = new System.Drawing.Size(60, 60);
            this.picProfile.TabIndex = 0;
            this.picProfile.Click += new System.EventHandler(this.picProfile_Click);
            // 
            // lblNavOldRecs
            // 
            this.lblNavOldRecs.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNavOldRecs.Appearance.Options.UseFont = true;
            this.lblNavOldRecs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNavOldRecs.Location = new System.Drawing.Point(468, 35);
            this.lblNavOldRecs.Name = "lblNavOldRecs";
            this.lblNavOldRecs.Size = new System.Drawing.Size(32, 23);
            this.lblNavOldRecs.TabIndex = 1;
            this.lblNavOldRecs.Text = "Çöp";
            this.lblNavOldRecs.Click += new System.EventHandler(this.lblNavList_Click);
            // 
            // lblNavEverything
            // 
            this.lblNavEverything.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNavEverything.Appearance.Options.UseFont = true;
            this.lblNavEverything.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNavEverything.Location = new System.Drawing.Point(360, 35);
            this.lblNavEverything.Name = "lblNavEverything";
            this.lblNavEverything.Size = new System.Drawing.Size(62, 23);
            this.lblNavEverything.TabIndex = 6;
            this.lblNavEverything.Text = "Her Şey";
            this.lblNavEverything.Click += new System.EventHandler(this.lblNavEverything_Click);
            // 
            // lblNavHome
            // 
            this.lblNavHome.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNavHome.Appearance.Options.UseFont = true;
            this.lblNavHome.Location = new System.Drawing.Point(243, 35);
            this.lblNavHome.Name = "lblNavHome";
            this.lblNavHome.Size = new System.Drawing.Size(80, 23);
            this.lblNavHome.TabIndex = 0;
            this.lblNavHome.Text = "Ana Sayfa";
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.lblChangePicture);
            this.pnlSettings.Controls.Add(this.lblChangeName);
            this.pnlSettings.Location = new System.Drawing.Point(820, 85);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(160, 70);
            this.pnlSettings.TabIndex = 4;
            this.pnlSettings.Visible = false;
            // 
            // lblChangePicture
            // 
            this.lblChangePicture.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            this.lblChangePicture.Appearance.Options.UseFont = true;
            this.lblChangePicture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblChangePicture.Location = new System.Drawing.Point(10, 10);
            this.lblChangePicture.Name = "lblChangePicture";
            this.lblChangePicture.Size = new System.Drawing.Size(117, 20);
            this.lblChangePicture.TabIndex = 0;
            this.lblChangePicture.Text = "Fotoğrafı Değiştir";
            this.lblChangePicture.Click += new System.EventHandler(this.lblChangePicture_Click);
            // 
            // lblChangeName
            // 
            this.lblChangeName.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            this.lblChangeName.Appearance.Options.UseFont = true;
            this.lblChangeName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblChangeName.Location = new System.Drawing.Point(10, 40);
            this.lblChangeName.Name = "lblChangeName";
            this.lblChangeName.Size = new System.Drawing.Size(83, 20);
            this.lblChangeName.TabIndex = 1;
            this.lblChangeName.Text = "İsmi Değiştir";
            this.lblChangeName.Click += new System.EventHandler(this.lblChangeName_Click);
            // 
            // pnlMagnified
            // 
            this.pnlMagnified.Controls.Add(this.picLargeProfile);
            this.pnlMagnified.Location = new System.Drawing.Point(250, 100);
            this.pnlMagnified.Name = "pnlMagnified";
            this.pnlMagnified.Size = new System.Drawing.Size(500, 450);
            this.pnlMagnified.TabIndex = 5;
            this.pnlMagnified.Visible = false;
            this.pnlMagnified.Click += new System.EventHandler(this.pnlMagnified_Click);
            // 
            // picLargeProfile
            // 
            this.picLargeProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLargeProfile.Location = new System.Drawing.Point(2, 2);
            this.picLargeProfile.Name = "picLargeProfile";
            this.picLargeProfile.Properties.ShowMenu = false;
            this.picLargeProfile.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picLargeProfile.Size = new System.Drawing.Size(496, 446);
            this.picLargeProfile.TabIndex = 0;
            this.picLargeProfile.Click += new System.EventHandler(this.pnlMagnified_Click);
            // 
            // pnlCategories
            // 
            this.pnlCategories.Controls.Add(this.btnSelectMovies);
            this.pnlCategories.Controls.Add(this.btnSelectSeries);
            this.pnlCategories.Controls.Add(this.btnSelectGames);
            this.pnlCategories.Controls.Add(this.chkMovies);
            this.pnlCategories.Controls.Add(this.chkSeries);
            this.pnlCategories.Controls.Add(this.chkGames);
            this.pnlCategories.Location = new System.Drawing.Point(198, 100);
            this.pnlCategories.Name = "pnlCategories";
            this.pnlCategories.Size = new System.Drawing.Size(600, 130);
            this.pnlCategories.TabIndex = 2;
            // 
            // btnSelectMovies
            // 
            this.btnSelectMovies.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSelectMovies.Appearance.Options.UseFont = true;
            this.btnSelectMovies.Location = new System.Drawing.Point(420, 70);
            this.btnSelectMovies.Name = "btnSelectMovies";
            this.btnSelectMovies.Size = new System.Drawing.Size(160, 40);
            this.btnSelectMovies.TabIndex = 5;
            this.btnSelectMovies.Text = "SEÇ";
            this.btnSelectMovies.Visible = false;
            this.btnSelectMovies.Click += new System.EventHandler(this.GenreButton_Click);
            // 
            // btnSelectSeries
            // 
            this.btnSelectSeries.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSelectSeries.Appearance.Options.UseFont = true;
            this.btnSelectSeries.Location = new System.Drawing.Point(220, 70);
            this.btnSelectSeries.Name = "btnSelectSeries";
            this.btnSelectSeries.Size = new System.Drawing.Size(160, 40);
            this.btnSelectSeries.TabIndex = 4;
            this.btnSelectSeries.Text = "SEÇ";
            this.btnSelectSeries.Visible = false;
            this.btnSelectSeries.Click += new System.EventHandler(this.GenreButton_Click);
            // 
            // btnSelectGames
            // 
            this.btnSelectGames.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSelectGames.Appearance.Options.UseFont = true;
            this.btnSelectGames.Location = new System.Drawing.Point(20, 70);
            this.btnSelectGames.Name = "btnSelectGames";
            this.btnSelectGames.Size = new System.Drawing.Size(160, 40);
            this.btnSelectGames.TabIndex = 3;
            this.btnSelectGames.Text = "SEÇ";
            this.btnSelectGames.Visible = false;
            this.btnSelectGames.Click += new System.EventHandler(this.GenreButton_Click);
            // 
            // chkMovies
            // 
            this.chkMovies.EditValue = true;
            this.chkMovies.Location = new System.Drawing.Point(420, 25);
            this.chkMovies.Name = "chkMovies";
            this.chkMovies.Properties.AllowFocused = false;
            this.chkMovies.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.chkMovies.Properties.Appearance.Options.UseFont = true;
            this.chkMovies.Properties.Caption = "FİLMLER";
            this.chkMovies.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkMovies.Properties.RadioGroupIndex = 1;
            this.chkMovies.Size = new System.Drawing.Size(160, 32);
            this.chkMovies.TabIndex = 2;
            this.chkMovies.CheckedChanged += new System.EventHandler(this.CategoryCheck_Changed);
            // 
            // chkSeries
            // 
            this.chkSeries.Location = new System.Drawing.Point(220, 25);
            this.chkSeries.Name = "chkSeries";
            this.chkSeries.Properties.AllowFocused = false;
            this.chkSeries.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.chkSeries.Properties.Appearance.Options.UseFont = true;
            this.chkSeries.Properties.Caption = "DİZİLER";
            this.chkSeries.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkSeries.Properties.RadioGroupIndex = 1;
            this.chkSeries.Size = new System.Drawing.Size(160, 32);
            this.chkSeries.TabIndex = 1;
            this.chkSeries.TabStop = false;
            this.chkSeries.CheckedChanged += new System.EventHandler(this.CategoryCheck_Changed);
            // 
            // chkGames
            // 
            this.chkGames.Location = new System.Drawing.Point(20, 25);
            this.chkGames.Name = "chkGames";
            this.chkGames.Properties.AllowFocused = false;
            this.chkGames.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.chkGames.Properties.Appearance.Options.UseFont = true;
            this.chkGames.Properties.Caption = "OYUNLAR";
            this.chkGames.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkGames.Properties.RadioGroupIndex = 1;
            this.chkGames.Size = new System.Drawing.Size(160, 32);
            this.chkGames.TabIndex = 0;
            this.chkGames.TabStop = false;
            this.chkGames.CheckedChanged += new System.EventHandler(this.CategoryCheck_Changed);
            // 
            // picHeroMain
            // 
            this.picHeroMain.Location = new System.Drawing.Point(180, 236);
            this.picHeroMain.Name = "picHeroMain";
            this.picHeroMain.Properties.ShowMenu = false;
            this.picHeroMain.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picHeroMain.Size = new System.Drawing.Size(640, 343);
            this.picHeroMain.TabIndex = 3;
            // 
            // btnMainSelect
            // 
            this.btnMainSelect.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnMainSelect.Appearance.Options.UseFont = true;
            this.btnMainSelect.Location = new System.Drawing.Point(395, 580);
            this.btnMainSelect.Name = "btnMainSelect";
            this.btnMainSelect.Size = new System.Drawing.Size(200, 60);
            this.btnMainSelect.TabIndex = 4;
            this.btnMainSelect.Text = "SEÇ";
            this.btnMainSelect.Visible = false;
            this.btnMainSelect.Click += new System.EventHandler(this.GenreButton_Click);
            // 
            // picRobot
            // 
            this.picRobot.Location = new System.Drawing.Point(830, 379);
            this.picRobot.Name = "picRobot";
            this.picRobot.Properties.AllowFocused = false;
            this.picRobot.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picRobot.Properties.Appearance.Options.UseBackColor = true;
            this.picRobot.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picRobot.Properties.ShowMenu = false;
            this.picRobot.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picRobot.Size = new System.Drawing.Size(100, 100);
            this.picRobot.TabIndex = 6;
            // 
            // tmrRobot
            // 
            this.tmrRobot.Enabled = true;
            this.tmrRobot.Interval = 50;
            this.tmrRobot.Tick += new System.EventHandler(this.tmrRobot_Tick);
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 650);
            this.Controls.Add(this.picRobot);
            this.Controls.Add(this.btnMainSelect);
            this.Controls.Add(this.picHeroMain);
            this.Controls.Add(this.pnlMagnified);
            this.Controls.Add(this.pnlSettings);
            this.Controls.Add(this.pnlCategories);
            this.Controls.Add(this.pnlTopNav);
            this.Name = "FrmHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VibeMap | Ana Sayfa";
            this.Load += new System.EventHandler(this.FrmHome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopNav)).EndInit();
            this.pnlTopNav.ResumeLayout(false);
            this.pnlTopNav.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProfile)).EndInit();
            this.pnlProfile.ResumeLayout(false);
            this.pnlProfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProfile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSettings)).EndInit();
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMagnified)).EndInit();
            this.pnlMagnified.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLargeProfile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCategories)).EndInit();
            this.pnlCategories.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkMovies.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSeries.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGames.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeroMain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRobot.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlTopNav;
        private DevExpress.XtraEditors.SimpleButton btnThemeToggle;
        private DevExpress.XtraEditors.LabelControl lblNavHome;
        private DevExpress.XtraEditors.LabelControl lblNavOldRecs;
        private DevExpress.XtraEditors.LabelControl lblNavWatched;
        private DevExpress.XtraEditors.LabelControl lblNavWishlist;
        private DevExpress.XtraEditors.LabelControl lblNavEverything;
        private DevExpress.XtraEditors.LabelControl lblNavSettings;
        private DevExpress.XtraEditors.PanelControl pnlSettings;
        private DevExpress.XtraEditors.LabelControl lblChangePicture;
        private DevExpress.XtraEditors.LabelControl lblChangeName;
        private DevExpress.XtraEditors.PanelControl pnlMagnified;
        private DevExpress.XtraEditors.PictureEdit picLargeProfile;
        private DevExpress.XtraEditors.PanelControl pnlProfile;
        private DevExpress.XtraEditors.LabelControl lblUsername;
        private DevExpress.XtraEditors.PictureEdit picProfile;
        private DevExpress.XtraEditors.PanelControl pnlCategories;
        private DevExpress.XtraEditors.CheckEdit chkGames;
        private DevExpress.XtraEditors.CheckEdit chkSeries;
        private DevExpress.XtraEditors.CheckEdit chkMovies;
        private DevExpress.XtraEditors.SimpleButton btnSelectGames;
        private DevExpress.XtraEditors.SimpleButton btnSelectSeries;
        private DevExpress.XtraEditors.SimpleButton btnSelectMovies;
        private DevExpress.XtraEditors.PictureEdit picHeroMain;
        private DevExpress.XtraEditors.SimpleButton btnMainSelect;
        private DevExpress.XtraEditors.PictureEdit picRobot;
        private System.Windows.Forms.Timer tmrRobot;
    }
}
