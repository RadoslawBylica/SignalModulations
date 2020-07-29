using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace PTD
{
    public partial class Form1 : Form
    {
        public class XY<typ>
        where typ : new()
        {
            private typ x;
            private typ y;

            public typ X
            {
                get => this.x;
                set => this.x = value;
            }

            public typ Y
            {
                get => this.y;
                set => this.y = value;
            }

            public XY(typ x, typ y)
            {
                X = x;
                Y = y;
            }
        }

        public class Modulator
        {
            public List<XY<double>> msk;
            public List<XY<double>> msk_odd;
            public List<XY<double>> msk_even;

            public Widmo widmo_msk;

            public List<double> wiadomosc;

            public byte[,] Wiadomosc;
            private int WiadaomoscLength;
            private int WiadaomoscLengthByte;

            private int SamplesPerByte;
            private double TimePerByte;

            private double[] Time;

            public double[] YToDoubleArray(List<XY<double>> xy, double LiczbaElementow)
            {
                double[] ret = new double[(int)LiczbaElementow];
                int iterator = 0;
                foreach (var i in xy)
                {
                    ret[iterator++] = i.Y;
                }
                return ret;
            }

            public Modulator(byte[,] wiadomosc, int SamplesPerByte, double TimePerByte, double DefaultAmplitude, double DefaultFrequency)
            {
                this.Wiadomosc = wiadomosc;
                this.WiadaomoscLength = wiadomosc.GetLength(0);
                this.WiadaomoscLengthByte = wiadomosc.GetLength(1);

                this.SamplesPerByte = SamplesPerByte;
                this.TimePerByte = TimePerByte;
                this.Time = Form1.linespace(0, (TimePerByte * WiadaomoscLength * WiadaomoscLengthByte), SamplesPerByte * WiadaomoscLength * WiadaomoscLengthByte);

                MSK(Amplitude: DefaultAmplitude, Frequency: DefaultFrequency);

                int LiczbaElementow = SamplesPerByte * WiadaomoscLength * WiadaomoscLengthByte;
                widmo_msk = Widmo.NewWidmo(YToDoubleArray(msk, LiczbaElementow), DefaultFrequency);
            }

            public void MSK(double Amplitude = 1.0, double Frequency = 1.0)
            {
                msk = new List<XY<double>>();
                msk_odd = new List<XY<double>>();
                msk_even = new List<XY<double>>();
                double x, y, t;
                int TimeIterator_odd = SamplesPerByte;
                int TimeIterator_even = 0;
                for (int NumberOfLetter = 0; NumberOfLetter < WiadaomoscLength; NumberOfLetter++)
                {
                    for (int NumberOfByte = 0; NumberOfByte < WiadaomoscLengthByte; NumberOfByte++)
                        for (int NumberOfSample = 0; NumberOfSample < SamplesPerByte * 2; NumberOfSample++)
                        {
                            t = (double)NumberOfSample / ((double)SamplesPerByte) / 4.0;
                            if(NumberOfByte%2 == 0)
                            {
                                x = Time[TimeIterator_even++];
                                if (Wiadomosc[NumberOfLetter, NumberOfByte] == 0)
                                {
                                    y = Amplitude * Math.Sin(2.0 * Math.PI * Frequency * t + Math.PI);
                                }
                                else
                                {
                                    y = Amplitude * Math.Sin(2.0 * Math.PI * Frequency * t);
                                }
                                msk_even.Add(new XY<double>(x, y));

                                if(NumberOfByte == 0)
                                    msk_odd.Add(new XY<double>(x, 0.0));
                            }
                            else
                            {
                                if (NumberOfByte == WiadaomoscLength * WiadaomoscLengthByte - 1 && NumberOfSample == SamplesPerByte - 1)
                                    break;

                                x = Time[TimeIterator_odd++];
                                if (Wiadomosc[NumberOfLetter, NumberOfByte] == 0)
                                {
                                    y = Amplitude * Math.Sin(2.0 * Math.PI * Frequency * t + Math.PI);
                                }
                                else
                                {
                                    y = Amplitude * Math.Sin(2.0 * Math.PI * Frequency * t);
                                }
                                msk_odd.Add(new XY<double>(x, y));
                            }
                        }
                }
                for(int i = 0; i< msk_even.Count; i++)
                {
                    msk.Add(new XY<double>(msk_even[i].X, msk_even[i].Y + msk_odd[i].Y));
                }
            }
        }
        public class MD
        {
            public Modulator k;
            public MD(string Wiadomosc, int SamplesPerByte, double TimePerByte, double DefaultAmplitude, double DefaultFrequency) => k = new Modulator(Form1.StringToByteArray(Wiadomosc), SamplesPerByte, TimePerByte, DefaultAmplitude, DefaultFrequency);
        }

        public MD hub = new MD(Wiadomosc: "a", SamplesPerByte: 100, TimePerByte: 1, DefaultAmplitude: 1.0, DefaultFrequency: 100.0);

        /*Funkcje i zmienne dla oxyplota*/
        public OxyPlot.WindowsForms.PlotView pv;
        public PlotModel pm;
        public bool export = false;
        public void Wykres(FunctionSeries fs, string x, string y)
        {
            this.Controls.Remove(pv);
            pv = new PlotView();
            pv.Dock = DockStyle.Bottom;
            pv.Height = 700;
            this.Controls.Add(pv);

            pm = new PlotModel();
            pm.Axes.Add(new LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom, Title = x, AxisTitleDistance = 5 });
            pm.Axes.Add(new LinearAxis { Position = OxyPlot.Axes.AxisPosition.Left, Title = y, AxisTitleDistance = 5 });

            pv.Model = pm;
            pv.Model.Series.Add(fs);
        }
        public static FunctionSeries ToFunctionSeries(List<XY<double>> xy)
        {
            FunctionSeries fs = new FunctionSeries();
            foreach (var element in xy)
                fs.Points.Add(new DataPoint(element.X, element.Y));
            return fs;
        }
        public static FunctionSeries ToFunctionSeries(double[] x, double[] y)
        {
            FunctionSeries fs = new FunctionSeries();
            for (int i = 0; i < y.Length; i++)
                fs.Points.Add(new DataPoint(x[i], y[i]));
            return fs;
        }
        /*Koniec*/

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(1900, 800);
        }

        public static double[] linespace(double min, double max, int n)
        {
            double[] linspace = new double[n];
            double krok = (max - min) / (n - 1);
            for (int i = 0; i < n; i++)
                linspace[i] = min + krok * i;
            return linspace;
        }

        public static byte[,] StringToByteArray(string s)
        {
            byte[,] b = new byte[s.Length, sizeof(char) * 8];
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < sizeof(char) * 8; j++)
                    b[i, j] = (byte)((s[i] >> j) & 0x01);
            return b;
        }

        private void MSK_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(hub.k.msk), "time", "amplitude");
            if (export) PngExporter.Export(this.pv.ActualModel, "msk.png", 1900, 800, OxyColors.White);
        }

        private void WIDMO_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(hub.k.widmo_msk.x, hub.k.widmo_msk.y), "frequency", "amplitude");
            if (export) PngExporter.Export(this.pv.ActualModel, "widmo amplitudowe.png", 1900, 800, OxyColors.White);
        }

        private void WIDMO_LOG_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(hub.k.widmo_msk.x, hub.k.widmo_msk.yLog), "frequency", "log");
            if (export) PngExporter.Export(this.pv.ActualModel, "widmo logarytmiczne.png", 1900, 800, OxyColors.White);
        }
    }
}
