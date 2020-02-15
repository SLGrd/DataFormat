namespace grdData
{
    partial class FrmDetails
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
            this.dgvCultureDetails = new System.Windows.Forms.DataGridView();
            this.lblCultureName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCultureDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCultureDetails
            // 
            this.dgvCultureDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCultureDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCultureDetails.Location = new System.Drawing.Point(12, 42);
            this.dgvCultureDetails.Name = "dgvCultureDetails";
            this.dgvCultureDetails.Size = new System.Drawing.Size(786, 390);
            this.dgvCultureDetails.TabIndex = 0;
            // 
            // lblCultureName
            // 
            this.lblCultureName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCultureName.Location = new System.Drawing.Point(184, 9);
            this.lblCultureName.Name = "lblCultureName";
            this.lblCultureName.Size = new System.Drawing.Size(440, 26);
            this.lblCultureName.TabIndex = 1;
            this.lblCultureName.Text = "exee";
            this.lblCultureName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmDetails
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(808, 450);
            this.Controls.Add(this.lblCultureName);
            this.Controls.Add(this.dgvCultureDetails);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDetails";
            this.ShowIcon = false;
            this.Text = "Detalhes da Regionalização";
            this.Load += new System.EventHandler(this.FrmDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCultureDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCultureDetails;
        private System.Windows.Forms.Label lblCultureName;
    }
}