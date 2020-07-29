namespace PTD
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MSK = new System.Windows.Forms.ToolStripMenuItem();
            this.WIDMO = new System.Windows.Forms.ToolStripMenuItem();
            this.WIDMO_LOG = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MSK,
            this.WIDMO,
            this.WIDMO_LOG});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MSK
            // 
            this.MSK.Name = "MSK";
            this.MSK.Size = new System.Drawing.Size(51, 24);
            this.MSK.Text = "MSK";
            this.MSK.Click += new System.EventHandler(this.MSK_Click);
            // 
            // WIDMO
            // 
            this.WIDMO.Name = "WIDMO";
            this.WIDMO.Size = new System.Drawing.Size(74, 24);
            this.WIDMO.Text = "WIDMO";
            this.WIDMO.Click += new System.EventHandler(this.WIDMO_Click);
            // 
            // WIDMO_LOG
            // 
            this.WIDMO_LOG.Name = "WIDMO_LOG";
            this.WIDMO_LOG.Size = new System.Drawing.Size(106, 24);
            this.WIDMO_LOG.Text = "WIDMO LOG";
            this.WIDMO_LOG.Click += new System.EventHandler(this.WIDMO_LOG_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem WIDMO;
        private System.Windows.Forms.ToolStripMenuItem MSK;
        private System.Windows.Forms.ToolStripMenuItem WIDMO_LOG;
    }
}

