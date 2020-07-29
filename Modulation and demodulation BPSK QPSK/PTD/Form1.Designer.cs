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
            this.bPSKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MBPSK = new System.Windows.Forms.ToolStripMenuItem();
            this.DBPSK = new System.Windows.Forms.ToolStripMenuItem();
            this.qPSKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MQPSK = new System.Windows.Forms.ToolStripMenuItem();
            this.DQPSK = new System.Windows.Forms.ToolStripMenuItem();
            this.wIDMAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WBPSK = new System.Windows.Forms.ToolStripMenuItem();
            this.WQPSK = new System.Windows.Forms.ToolStripMenuItem();
            this.DEMODULACJAXB = new System.Windows.Forms.ToolStripMenuItem();
            this.DEMODULACJAPB = new System.Windows.Forms.ToolStripMenuItem();
            this.DEMODULACJAXQ = new System.Windows.Forms.ToolStripMenuItem();
            this.DEMODULACJAPQ = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bPSKToolStripMenuItem,
            this.qPSKToolStripMenuItem,
            this.wIDMAToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bPSKToolStripMenuItem
            // 
            this.bPSKToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MBPSK,
            this.DBPSK,
            this.DEMODULACJAXB,
            this.DEMODULACJAPB});
            this.bPSKToolStripMenuItem.Name = "bPSKToolStripMenuItem";
            this.bPSKToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.bPSKToolStripMenuItem.Text = "BPSK";
            // 
            // MBPSK
            // 
            this.MBPSK.Name = "MBPSK";
            this.MBPSK.Size = new System.Drawing.Size(216, 26);
            this.MBPSK.Text = "MODULACJA";
            this.MBPSK.Click += new System.EventHandler(this.MBPSK_Click);
            // 
            // DBPSK
            // 
            this.DBPSK.Name = "DBPSK";
            this.DBPSK.Size = new System.Drawing.Size(216, 26);
            this.DBPSK.Text = "DEMODULACJA";
            this.DBPSK.Click += new System.EventHandler(this.DBPSK_Click);
            // 
            // qPSKToolStripMenuItem
            // 
            this.qPSKToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MQPSK,
            this.DQPSK,
            this.DEMODULACJAXQ,
            this.DEMODULACJAPQ});
            this.qPSKToolStripMenuItem.Name = "qPSKToolStripMenuItem";
            this.qPSKToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.qPSKToolStripMenuItem.Text = "QPSK";
            // 
            // MQPSK
            // 
            this.MQPSK.Name = "MQPSK";
            this.MQPSK.Size = new System.Drawing.Size(216, 26);
            this.MQPSK.Text = "MODULACJA";
            this.MQPSK.Click += new System.EventHandler(this.MQPSK_Click);
            // 
            // DQPSK
            // 
            this.DQPSK.Name = "DQPSK";
            this.DQPSK.Size = new System.Drawing.Size(216, 26);
            this.DQPSK.Text = "DEMODULACJA";
            this.DQPSK.Click += new System.EventHandler(this.DQPSK_Click);
            // 
            // wIDMAToolStripMenuItem
            // 
            this.wIDMAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WBPSK,
            this.WQPSK});
            this.wIDMAToolStripMenuItem.Name = "wIDMAToolStripMenuItem";
            this.wIDMAToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.wIDMAToolStripMenuItem.Text = "WIDMA";
            // 
            // WBPSK
            // 
            this.WBPSK.Name = "WBPSK";
            this.WBPSK.Size = new System.Drawing.Size(120, 26);
            this.WBPSK.Text = "BPSK";
            this.WBPSK.Click += new System.EventHandler(this.WBPSK_Click);
            // 
            // WQPSK
            // 
            this.WQPSK.Name = "WQPSK";
            this.WQPSK.Size = new System.Drawing.Size(120, 26);
            this.WQPSK.Text = "QPSK";
            this.WQPSK.Click += new System.EventHandler(this.WQPSK_Click);
            // 
            // DEMODULACJAXB
            // 
            this.DEMODULACJAXB.Name = "DEMODULACJAXB";
            this.DEMODULACJAXB.Size = new System.Drawing.Size(216, 26);
            this.DEMODULACJAXB.Text = "DEMODULACJA X";
            this.DEMODULACJAXB.Click += new System.EventHandler(this.DEMODULACJAXB_Click);
            // 
            // DEMODULACJAPB
            // 
            this.DEMODULACJAPB.Name = "DEMODULACJAPB";
            this.DEMODULACJAPB.Size = new System.Drawing.Size(216, 26);
            this.DEMODULACJAPB.Text = "DEMODULACJA P";
            this.DEMODULACJAPB.Click += new System.EventHandler(this.DEMODULACJAPB_Click);
            // 
            // DEMODULACJAXQ
            // 
            this.DEMODULACJAXQ.Name = "DEMODULACJAXQ";
            this.DEMODULACJAXQ.Size = new System.Drawing.Size(216, 26);
            this.DEMODULACJAXQ.Text = "DEMODULACJA X";
            this.DEMODULACJAXQ.Click += new System.EventHandler(this.DEMODULACJAXQ_Click);
            // 
            // DEMODULACJAPQ
            // 
            this.DEMODULACJAPQ.Name = "DEMODULACJAPQ";
            this.DEMODULACJAPQ.Size = new System.Drawing.Size(216, 26);
            this.DEMODULACJAPQ.Text = "DEMODULACJA P";
            this.DEMODULACJAPQ.Click += new System.EventHandler(this.DEMODULACJAPQ_Click);
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
        private System.Windows.Forms.ToolStripMenuItem bPSKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MBPSK;
        private System.Windows.Forms.ToolStripMenuItem DBPSK;
        private System.Windows.Forms.ToolStripMenuItem qPSKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MQPSK;
        private System.Windows.Forms.ToolStripMenuItem DQPSK;
        private System.Windows.Forms.ToolStripMenuItem wIDMAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WBPSK;
        private System.Windows.Forms.ToolStripMenuItem WQPSK;
        private System.Windows.Forms.ToolStripMenuItem DEMODULACJAXB;
        private System.Windows.Forms.ToolStripMenuItem DEMODULACJAPB;
        private System.Windows.Forms.ToolStripMenuItem DEMODULACJAXQ;
        private System.Windows.Forms.ToolStripMenuItem DEMODULACJAPQ;
    }
}

