namespace GUI
{
    partial class FormDialogCreateMatches
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle51 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle52 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle53 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle54 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle55 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblCreateMatches = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2ControlBox4 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.dgvClubs = new Guna.UI2.WinForms.Guna2DataGridView();
            this.ClubID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogoFileName = new System.Windows.Forms.DataGridViewImageColumn();
            this.ImageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClubName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelContent = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2DragControl2 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.cboRound = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnOpenDialogCreateSeason = new Guna.UI2.WinForms.Guna2Button();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClubs)).BeginInit();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.panelTop.Controls.Add(this.lblCreateMatches);
            this.panelTop.Controls.Add(this.guna2ControlBox4);
            this.panelTop.Controls.Add(this.guna2ControlBox3);
            this.panelTop.CustomBorderThickness = new System.Windows.Forms.Padding(1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(850, 147);
            this.panelTop.TabIndex = 25;
            // 
            // lblCreateMatches
            // 
            this.lblCreateMatches.BackColor = System.Drawing.Color.Transparent;
            this.lblCreateMatches.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateMatches.ForeColor = System.Drawing.Color.White;
            this.lblCreateMatches.Location = new System.Drawing.Point(48, 57);
            this.lblCreateMatches.Name = "lblCreateMatches";
            this.lblCreateMatches.Size = new System.Drawing.Size(163, 33);
            this.lblCreateMatches.TabIndex = 0;
            this.lblCreateMatches.Text = "Create Matches";
            // 
            // guna2ControlBox4
            // 
            this.guna2ControlBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox4.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox4.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2ControlBox4.ForeColor = System.Drawing.Color.White;
            this.guna2ControlBox4.HoverState.FillColor = System.Drawing.SystemColors.Control;
            this.guna2ControlBox4.HoverState.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.guna2ControlBox4.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox4.Location = new System.Drawing.Point(787, 0);
            this.guna2ControlBox4.Name = "guna2ControlBox4";
            this.guna2ControlBox4.Size = new System.Drawing.Size(63, 43);
            this.guna2ControlBox4.TabIndex = 1;
            // 
            // guna2ControlBox3
            // 
            this.guna2ControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox3.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox3.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox3.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2ControlBox3.ForeColor = System.Drawing.Color.White;
            this.guna2ControlBox3.HoverState.FillColor = System.Drawing.SystemColors.Control;
            this.guna2ControlBox3.HoverState.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.guna2ControlBox3.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox3.Location = new System.Drawing.Point(724, 0);
            this.guna2ControlBox3.Name = "guna2ControlBox3";
            this.guna2ControlBox3.Size = new System.Drawing.Size(63, 43);
            this.guna2ControlBox3.TabIndex = 0;
            // 
            // dgvClubs
            // 
            this.dgvClubs.AllowUserToDeleteRows = false;
            dataGridViewCellStyle51.BackColor = System.Drawing.Color.White;
            this.dgvClubs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle51;
            dataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle52.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle52.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle52.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle52.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle52.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvClubs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle52;
            this.dgvClubs.ColumnHeadersHeight = 40;
            this.dgvClubs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClubID,
            this.LogoFileName,
            this.ImageName,
            this.ClubName});
            dataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle53.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle53.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle53.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle53.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle53.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle53.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvClubs.DefaultCellStyle = dataGridViewCellStyle53;
            this.dgvClubs.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.dgvClubs.Location = new System.Drawing.Point(16, 16);
            this.dgvClubs.Name = "dgvClubs";
            this.dgvClubs.ReadOnly = true;
            dataGridViewCellStyle54.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle54.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle54.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle54.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle54.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle54.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle54.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvClubs.RowHeadersDefaultCellStyle = dataGridViewCellStyle54;
            this.dgvClubs.RowHeadersVisible = false;
            this.dgvClubs.RowHeadersWidth = 50;
            this.dgvClubs.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle55.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dgvClubs.RowsDefaultCellStyle = dataGridViewCellStyle55;
            this.dgvClubs.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvClubs.RowTemplate.DividerHeight = 7;
            this.dgvClubs.RowTemplate.Height = 55;
            this.dgvClubs.Size = new System.Drawing.Size(428, 692);
            this.dgvClubs.TabIndex = 32;
            this.dgvClubs.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvClubs.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvClubs.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvClubs.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvClubs.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvClubs.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvClubs.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.dgvClubs.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.dgvClubs.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvClubs.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvClubs.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.DimGray;
            this.dgvClubs.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvClubs.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvClubs.ThemeStyle.ReadOnly = true;
            this.dgvClubs.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvClubs.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvClubs.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvClubs.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvClubs.ThemeStyle.RowsStyle.Height = 55;
            this.dgvClubs.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvClubs.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // ClubID
            // 
            this.ClubID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ClubID.DataPropertyName = "ClubID";
            this.ClubID.FillWeight = 60.79636F;
            this.ClubID.Frozen = true;
            this.ClubID.HeaderText = "";
            this.ClubID.MinimumWidth = 6;
            this.ClubID.Name = "ClubID";
            this.ClubID.ReadOnly = true;
            this.ClubID.Width = 30;
            // 
            // LogoFileName
            // 
            this.LogoFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.LogoFileName.FillWeight = 213.9037F;
            this.LogoFileName.HeaderText = "";
            this.LogoFileName.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.LogoFileName.MinimumWidth = 6;
            this.LogoFileName.Name = "LogoFileName";
            this.LogoFileName.ReadOnly = true;
            this.LogoFileName.Width = 70;
            // 
            // ImageName
            // 
            this.ImageName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ImageName.HeaderText = "";
            this.ImageName.MinimumWidth = 6;
            this.ImageName.Name = "ImageName";
            this.ImageName.ReadOnly = true;
            this.ImageName.Width = 30;
            // 
            // ClubName
            // 
            this.ClubName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ClubName.DataPropertyName = "ClubName";
            this.ClubName.FillWeight = 53.63456F;
            this.ClubName.HeaderText = "Club";
            this.ClubName.MinimumWidth = 6;
            this.ClubName.Name = "ClubName";
            this.ClubName.ReadOnly = true;
            this.ClubName.Width = 300;
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BackColor = System.Drawing.Color.White;
            this.panelContent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.panelContent.Controls.Add(this.btnOpenDialogCreateSeason);
            this.panelContent.Controls.Add(this.cboRound);
            this.panelContent.Controls.Add(this.dgvClubs);
            this.panelContent.CustomBorderThickness = new System.Windows.Forms.Padding(1);
            this.panelContent.Location = new System.Drawing.Point(12, 161);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(826, 724);
            this.panelContent.TabIndex = 33;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.panelTop;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2DragControl2
            // 
            this.guna2DragControl2.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl2.TargetControl = this.panelContent;
            this.guna2DragControl2.UseTransparentDrag = true;
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // cboRound
            // 
            this.cboRound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.cboRound.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.cboRound.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboRound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRound.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.cboRound.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.cboRound.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.cboRound.FocusedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.cboRound.FocusedState.ForeColor = System.Drawing.Color.White;
            this.cboRound.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.cboRound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.cboRound.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.cboRound.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.cboRound.HoverState.ForeColor = System.Drawing.Color.White;
            this.cboRound.ItemHeight = 30;
            this.cboRound.Location = new System.Drawing.Point(544, 16);
            this.cboRound.Name = "cboRound";
            this.cboRound.Size = new System.Drawing.Size(185, 36);
            this.cboRound.TabIndex = 33;
            // 
            // btnOpenDialogCreateSeason
            // 
            this.btnOpenDialogCreateSeason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenDialogCreateSeason.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenDialogCreateSeason.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.btnOpenDialogCreateSeason.BorderRadius = 3;
            this.btnOpenDialogCreateSeason.BorderThickness = 2;
            this.btnOpenDialogCreateSeason.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenDialogCreateSeason.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnOpenDialogCreateSeason.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnOpenDialogCreateSeason.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnOpenDialogCreateSeason.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnOpenDialogCreateSeason.FillColor = System.Drawing.Color.White;
            this.btnOpenDialogCreateSeason.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnOpenDialogCreateSeason.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.btnOpenDialogCreateSeason.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.btnOpenDialogCreateSeason.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.btnOpenDialogCreateSeason.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnOpenDialogCreateSeason.Location = new System.Drawing.Point(544, 86);
            this.btnOpenDialogCreateSeason.Name = "btnOpenDialogCreateSeason";
            this.btnOpenDialogCreateSeason.Size = new System.Drawing.Size(185, 40);
            this.btnOpenDialogCreateSeason.TabIndex = 34;
            this.btnOpenDialogCreateSeason.Text = "Create";
            this.btnOpenDialogCreateSeason.Click += new System.EventHandler(this.btnOpenDialogCreateSeason_Click);
            // 
            // FormDialogCreateMatches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 900);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDialogCreateMatches";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormDialogCreateMatches";
            this.Load += new System.EventHandler(this.FormDialogCreateMatches_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClubs)).EndInit();
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelTop;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCreateMatches;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox4;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox3;
        private Guna.UI2.WinForms.Guna2DataGridView dgvClubs;
        private Guna.UI2.WinForms.Guna2Panel panelContent;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl2;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClubID;
        private System.Windows.Forms.DataGridViewImageColumn LogoFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImageName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClubName;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ComboBox cboRound;
        private Guna.UI2.WinForms.Guna2Button btnOpenDialogCreateSeason;
    }
}