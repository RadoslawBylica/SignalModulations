using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace SpreadingSignal
{
    public partial class Form1 : Form
    {
        private static GoldCode GC = new GoldCode();

        public Form1()
        {
            InitializeComponent();
            int[] Code = GoldCode.GenerateGoldCode(new int[] {1,0,0,0,0});
            
        }

        private void DodajWiadomosc_Click(object sender, EventArgs e)
        {
            GC.Add(Wiadomosc.Text, Kod.Text);
            Wykres(plotView1, ToFunctionSeries(linespace(0, (double)GC.GetSignal.Length / GC.SamplesPerValue, GC.GetSignal.Length),GC.GetSignal), "Samples", "Value");

        }

        private void button_odkoduj_wiadomosc_Click(object sender, EventArgs e)
        {
            int[] MyMessage = GC.GetMyMessage(KodOdkoduj.Text);
            string Message = "Twoja odkodowana wiadomość to:\n";
            foreach (var Value in MyMessage)
                Message += Value.ToString();

            MessageBox.Show(Message);
        }

        private void button_GenerateMatrix_Click(object sender, EventArgs e)
        {
            var MatrixH = GoldCode.GenerateMatrixH((int)numericGenerateMatrix.Value);
            if (MatrixH == (object)-1)
                MessageBox.Show("Podana wartość jest nieprawidłowa.");
            else
            {
                String Message = "Udało się wygenerować macierz Hadamard-a:\n";
                for(int i = 0; i < ((double[,])MatrixH).GetLength(0); i++)
                {
                    for (int j = 0; j < ((double[,])MatrixH).GetLength(1); j++)
                        Message += ((double[,])MatrixH)[i, j].ToString();
                    Message += "\n";
                }
                   
                MessageBox.Show(Message);
            }
                
        }

        /*Funkcje i zmienne dla oxyplota*/
        public OxyPlot.WindowsForms.PlotView pv;
        public PlotModel pm;
        public bool export = false;
        public void Wykres(PlotView pv, FunctionSeries fs, string x, string y)
        {
            pm = new PlotModel();
            pm.Axes.Add(new LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom, Title = x, AxisTitleDistance = 5 });
            pm.Axes.Add(new LinearAxis { Position = OxyPlot.Axes.AxisPosition.Left, Title = y, AxisTitleDistance = 5 });

            pv.Model = pm;
            pv.Model.Series.Add(fs);
        }
        public void DodajWykres(FunctionSeries fs)
        {
            pv.Model.Series.Add(fs);
        }
        public FunctionSeries ToFunctionSeries(double[] x, double[] y)
        {
            FunctionSeries fs = new FunctionSeries();
            for (int i = 0; i < y.Length; i++)
                fs.Points.Add(new DataPoint(x[i], y[i]));
            return fs;
        }
        /*Koniec*/

        public static double[] linespace(double min, double max, int n)
        {
            double[] linspace = new double[n];
            double krok = (max - min) / (n - 1);
            for (int i = 0; i < n; i++)
                linspace[i] = min + krok * i;
            return linspace;
        } 
    }
}
