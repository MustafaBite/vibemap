namespace VibeMap.Forms
{
    partial class FrmMood
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.btnContinue = new DevExpress.XtraEditors.SimpleButton();
            this.pnlCard = new DevExpress.XtraEditors.PanelControl();
            this.flowMoods = new System.Windows.Forms.FlowLayoutPanel();
            this.btnThemeToggle = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCard)).BeginInit();
            this.pnlCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Location = new System.Drawing.Point(90, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(290, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Bugün nasıl hissediyorsun?";
            // 
            // btnContinue
            // 
            this.btnContinue.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnContinue.Appearance.Options.UseFont = true;
            this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnContinue.Location = new System.Drawing.Point(15, 504);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(427, 45);
            this.btnContinue.TabIndex = 2;
            this.btnContinue.Text = "Devam Et";
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // pnlCard
            // 
            this.pnlCard.Controls.Add(this.flowMoods);
            this.pnlCard.Controls.Add(this.btnContinue);
            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Location = new System.Drawing.Point(305, 27);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(461, 558);
            this.pnlCard.TabIndex = 3;
            // 
            // flowMoods
            // 
            this.flowMoods.Location = new System.Drawing.Point(15, 60);
            this.flowMoods.Name = "flowMoods";
            this.flowMoods.Size = new System.Drawing.Size(427, 438);
            this.flowMoods.TabIndex = 3;
            // 
            // btnThemeToggle
            // 
            this.btnThemeToggle.Location = new System.Drawing.Point(950, 12);
            this.btnThemeToggle.Name = "btnThemeToggle";
            this.btnThemeToggle.Size = new System.Drawing.Size(38, 35);
            this.btnThemeToggle.TabIndex = 4;
            this.btnThemeToggle.Text = "🌙";
            this.btnThemeToggle.Click += new System.EventHandler(this.btnThemeToggle_Click);
            // 
            // FrmMood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.btnThemeToggle);
            this.Controls.Add(this.pnlCard);
            this.MinimizeBox = false;
            this.Name = "FrmMood";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VibeMap - Ruh Hali";
            this.Load += new System.EventHandler(this.FrmMood_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCard)).EndInit();
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.SimpleButton btnContinue;
        private DevExpress.XtraEditors.PanelControl pnlCard;
        private System.Windows.Forms.FlowLayoutPanel flowMoods;
        private DevExpress.XtraEditors.SimpleButton btnThemeToggle;
    }
}