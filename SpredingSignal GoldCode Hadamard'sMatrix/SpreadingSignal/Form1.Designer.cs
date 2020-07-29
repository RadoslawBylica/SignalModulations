namespace SpreadingSignal
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
            this.Wiadomosc = new System.Windows.Forms.TextBox();
            this.Kod = new System.Windows.Forms.TextBox();
            this.DodajWiadomosc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_GenerateMatrix = new System.Windows.Forms.Button();
            this.numericGenerateMatrix = new System.Windows.Forms.NumericUpDown();
            this.plotView1 = new OxyPlot.WindowsForms.PlotView();
            this.KodOdkoduj = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.WiadomoscOdkoduj = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_odkoduj_wiadomosc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericGenerateMatrix)).BeginInit();
            this.SuspendLayout();
            // 
            // Wiadomosc
            // 
            this.Wiadomosc.Location = new System.Drawing.Point(12, 12);
            this.Wiadomosc.Name = "Wiadomosc";
            this.Wiadomosc.Size = new System.Drawing.Size(100, 22);
            this.Wiadomosc.TabIndex = 0;
            // 
            // Kod
            // 
            this.Kod.Location = new System.Drawing.Point(12, 40);
            this.Kod.Name = "Kod";
            this.Kod.Size = new System.Drawing.Size(100, 22);
            this.Kod.TabIndex = 1;
            // 
            // DodajWiadomosc
            // 
            this.DodajWiadomosc.Location = new System.Drawing.Point(12, 68);
            this.DodajWiadomosc.Name = "DodajWiadomosc";
            this.DodajWiadomosc.Size = new System.Drawing.Size(187, 29);
            this.DodajWiadomosc.TabIndex = 2;
            this.DodajWiadomosc.Text = "Dodaj wiadomość";
            this.DodajWiadomosc.UseVisualStyleBackColor = true;
            this.DodajWiadomosc.Click += new System.EventHandler(this.DodajWiadomosc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Wiadomość";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Kod";
            // 
            // button_GenerateMatrix
            // 
            this.button_GenerateMatrix.Location = new System.Drawing.Point(668, 40);
            this.button_GenerateMatrix.Name = "button_GenerateMatrix";
            this.button_GenerateMatrix.Size = new System.Drawing.Size(120, 23);
            this.button_GenerateMatrix.TabIndex = 5;
            this.button_GenerateMatrix.Text = "Generate Matrix";
            this.button_GenerateMatrix.UseVisualStyleBackColor = true;
            this.button_GenerateMatrix.Click += new System.EventHandler(this.button_GenerateMatrix_Click);
            // 
            // numericGenerateMatrix
            // 
            this.numericGenerateMatrix.Location = new System.Drawing.Point(668, 12);
            this.numericGenerateMatrix.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericGenerateMatrix.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericGenerateMatrix.Name = "numericGenerateMatrix";
            this.numericGenerateMatrix.Size = new System.Drawing.Size(120, 22);
            this.numericGenerateMatrix.TabIndex = 6;
            this.numericGenerateMatrix.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // plotView1
            // 
            this.plotView1.Location = new System.Drawing.Point(12, 103);
            this.plotView1.Name = "plotView1";
            this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView1.Size = new System.Drawing.Size(775, 335);
            this.plotView1.TabIndex = 7;
            this.plotView1.Text = "plotView1";
            this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // KodOdkoduj
            // 
            this.KodOdkoduj.Location = new System.Drawing.Point(308, 40);
            this.KodOdkoduj.Name = "KodOdkoduj";
            this.KodOdkoduj.Size = new System.Drawing.Size(100, 22);
            this.KodOdkoduj.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(414, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Kod";
            // 
            // WiadomoscOdkoduj
            // 
            this.WiadomoscOdkoduj.Enabled = false;
            this.WiadomoscOdkoduj.Location = new System.Drawing.Point(308, 14);
            this.WiadomoscOdkoduj.Name = "WiadomoscOdkoduj";
            this.WiadomoscOdkoduj.Size = new System.Drawing.Size(100, 22);
            this.WiadomoscOdkoduj.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(414, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Wiadomość";
            // 
            // button_odkoduj_wiadomosc
            // 
            this.button_odkoduj_wiadomosc.Location = new System.Drawing.Point(308, 68);
            this.button_odkoduj_wiadomosc.Name = "button_odkoduj_wiadomosc";
            this.button_odkoduj_wiadomosc.Size = new System.Drawing.Size(187, 29);
            this.button_odkoduj_wiadomosc.TabIndex = 12;
            this.button_odkoduj_wiadomosc.Text = "Odkoduj wiadomość";
            this.button_odkoduj_wiadomosc.UseVisualStyleBackColor = true;
            this.button_odkoduj_wiadomosc.Click += new System.EventHandler(this.button_odkoduj_wiadomosc_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_odkoduj_wiadomosc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.WiadomoscOdkoduj);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.KodOdkoduj);
            this.Controls.Add(this.plotView1);
            this.Controls.Add(this.numericGenerateMatrix);
            this.Controls.Add(this.button_GenerateMatrix);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DodajWiadomosc);
            this.Controls.Add(this.Kod);
            this.Controls.Add(this.Wiadomosc);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericGenerateMatrix)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Wiadomosc;
        private System.Windows.Forms.TextBox Kod;
        private System.Windows.Forms.Button DodajWiadomosc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_GenerateMatrix;
        private System.Windows.Forms.NumericUpDown numericGenerateMatrix;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private System.Windows.Forms.TextBox KodOdkoduj;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox WiadomoscOdkoduj;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_odkoduj_wiadomosc;
    }
}

