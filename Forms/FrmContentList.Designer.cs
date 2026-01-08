namespace VibeMap.Forms
{
    partial class FrmContentList
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.gridContents = new DevExpress.XtraGrid.GridControl();
            this.gvContents = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMove = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnMoveRep = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDeleteRep = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridContents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvContents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveRep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRep)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Appearance.Options.UseFont = true;
            this.lblHeader.Appearance.Options.UseTextOptions = true;
            this.lblHeader.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblHeader.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(700, 70);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Liste Başlığı";
            // 
            // gridContents
            // 
            this.gridContents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridContents.Location = new System.Drawing.Point(20, 80);
            this.gridContents.MainView = this.gvContents;
            this.gridContents.Name = "gridContents";
            this.gridContents.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnMoveRep,
            this.btnDeleteRep});
            this.gridContents.Size = new System.Drawing.Size(660, 480);
            this.gridContents.TabIndex = 2;
            this.gridContents.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvContents});
            // 
            // gvContents
            // 
            this.gvContents.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTitle,
            this.colCategory,
            this.colMove,
            this.colDelete});
            this.gvContents.GridControl = this.gridContents;
            this.gvContents.Name = "gvContents";
            this.gvContents.OptionsView.ShowGroupPanel = false;
            this.gvContents.OptionsView.ShowIndicator = false;
            this.gvContents.RowHeight = 40;
            // 
            // colTitle
            // 
            this.colTitle.Caption = "Başlık";
            this.colTitle.FieldName = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.OptionsColumn.AllowEdit = false;
            this.colTitle.Visible = true;
            this.colTitle.VisibleIndex = 0;
            this.colTitle.Width = 350;
            // 
            // colCategory
            // 
            this.colCategory.Caption = "Kategori";
            this.colCategory.FieldName = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.OptionsColumn.AllowEdit = false;
            this.colCategory.Visible = true;
            this.colCategory.VisibleIndex = 1;
            this.colCategory.Width = 150;
            // 
            // colMove
            // 
            this.colMove.Caption = "Taşı";
            this.colMove.ColumnEdit = this.btnMoveRep;
            this.colMove.Name = "colMove";
            this.colMove.Visible = true;
            this.colMove.VisibleIndex = 2;
            this.colMove.Width = 60;
            // 
            // btnMoveRep
            // 
            this.btnMoveRep.AutoHeight = false;
            editorButtonImageOptions1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnMoveRep.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "+", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnMoveRep.Name = "btnMoveRep";
            this.btnMoveRep.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnMoveRep.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnMoveRep_ButtonClick);
            // 
            // colDelete
            // 
            this.colDelete.Caption = "Sil";
            this.colDelete.ColumnEdit = this.btnDeleteRep;
            this.colDelete.Name = "colDelete";
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 3;
            this.colDelete.Width = 60;
            // 
            // btnDeleteRep
            // 
            this.btnDeleteRep.AutoHeight = false;
            editorButtonImageOptions2.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnDeleteRep.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "🗑", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnDeleteRep.Name = "btnDeleteRep";
            this.btnDeleteRep.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnDeleteRep.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnDeleteRep_ButtonClick);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(20, 20);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(80, 30);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Geri";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // FrmContentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 600);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.gridContents);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmContentList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Listeniz";
            ((System.ComponentModel.ISupportInitialize)(this.gridContents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvContents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveRep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRep)).EndInit();
            this.ResumeLayout(false);

        }

        private DevExpress.XtraGrid.GridControl gridContents;
        private DevExpress.XtraGrid.Views.Grid.GridView gvContents;
        private DevExpress.XtraGrid.Columns.GridColumn colTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colMove;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnMoveRep;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDeleteRep;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private DevExpress.XtraEditors.LabelControl lblHeader;
    }
}
