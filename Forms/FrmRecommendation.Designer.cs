namespace VibeMap.Forms
{
    partial class FrmRecommendation
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

        private void InitializeComponent()
        {
            this.picHero = new DevExpress.XtraEditors.PictureEdit();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.btnAction = new DevExpress.XtraEditors.SimpleButton();
            this.btnIgnore = new DevExpress.XtraEditors.SimpleButton();
            this.btnWatched = new DevExpress.XtraEditors.SimpleButton();
            this.btnWishlist = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.picHero.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // picHero
            // 
            this.picHero.Dock = System.Windows.Forms.DockStyle.Top;
            this.picHero.Location = new System.Drawing.Point(0, 0);
            this.picHero.Name = "picHero";
            this.picHero.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picHero.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picHero.Size = new System.Drawing.Size(500, 300);
            this.picHero.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 300);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 20, 0, 10);
            this.lblTitle.Size = new System.Drawing.Size(500, 60);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Önerilen Başlık";
            // 
            // lblDescription
            // 
            this.lblDescription.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescription.Appearance.Options.UseFont = true;
            this.lblDescription.Appearance.Options.UseTextOptions = true;
            this.lblDescription.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblDescription.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblDescription.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDescription.Location = new System.Drawing.Point(0, 360);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.lblDescription.Size = new System.Drawing.Size(500, 120);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Bu, önerinin ayrıntılı bir açıklamasıdır. Konuyu veya oynanışı kapsar ve mevcut ruh halinize neden uyduğunu açıklar.";
            // 
            // btnAction
            // 
            this.btnAction.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAction.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAction.Appearance.Options.UseFont = true;
            this.btnAction.Location = new System.Drawing.Point(150, 490);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(200, 40);
            this.btnAction.TabIndex = 3;
            this.btnAction.Text = "Hemen İzle Oyna";
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // btnIgnore
            // 
            this.btnIgnore.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnIgnore.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnIgnore.Appearance.Options.UseFont = true;
            this.btnIgnore.Location = new System.Drawing.Point(50, 540);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(120, 35);
            this.btnIgnore.TabIndex = 4;
            this.btnIgnore.Text = "Beğenmedim";
            this.btnIgnore.Click += new System.EventHandler(this.btnTrackAction_Click);
            // 
            // btnWatched
            // 
            this.btnWatched.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnWatched.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnWatched.Appearance.Options.UseFont = true;
            this.btnWatched.Location = new System.Drawing.Point(190, 540);
            this.btnWatched.Name = "btnWatched";
            this.btnWatched.Size = new System.Drawing.Size(120, 35);
            this.btnWatched.TabIndex = 5;
            this.btnWatched.Text = "Bitti";
            this.btnWatched.Visible = false;
            this.btnWatched.Click += new System.EventHandler(this.btnTrackAction_Click);
            // 
            // btnWishlist
            // 
            this.btnWishlist.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnWishlist.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnWishlist.Appearance.Options.UseFont = true;
            this.btnWishlist.Location = new System.Drawing.Point(330, 540);
            this.btnWishlist.Name = "btnWishlist";
            this.btnWishlist.Size = new System.Drawing.Size(120, 35);
            this.btnWishlist.TabIndex = 6;
            this.btnWishlist.Text = "Beğendim";
            this.btnWishlist.Click += new System.EventHandler(this.btnTrackAction_Click);
            // 
            // FrmRecommendation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 620);
            this.Controls.Add(this.btnIgnore);
            this.Controls.Add(this.btnWatched);
            this.Controls.Add(this.btnWishlist);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picHero);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRecommendation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sizin İçin Önerimiz";
            ((System.ComponentModel.ISupportInitialize)(this.picHero.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        private DevExpress.XtraEditors.PictureEdit picHero;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.SimpleButton btnAction;
        private DevExpress.XtraEditors.SimpleButton btnIgnore;
        private DevExpress.XtraEditors.SimpleButton btnWatched;
        private DevExpress.XtraEditors.SimpleButton btnWishlist;
    }
}
