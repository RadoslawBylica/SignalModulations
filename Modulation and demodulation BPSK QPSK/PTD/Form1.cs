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
using FFTWSharp;
using System.Runtime.InteropServices;

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
            public List<XY<double>> bpsk;
            public List<XY<double>> bpsksp;
            public List<XY<double>> qpsk;
            public List<XY<double>> qpsksp;
            public List<XY<double>> debpsk;
            public List<XY<double>> deqpsk;

            public List<XY<double>> XB;
            public List<XY<double>> PB;
            public List<XY<double>> XQ;
            public List<XY<double>> PQ;

            public Widmo widmo_bpsk;
            public Widmo widmo_qpsk;

            public List<double> wiadomosc;

            public byte[,] Wiadomosc;
            private int WiadaomoscLength;
            private int WiadaomoscLengthByte;

            private int SamplesPerByte;
            private double TimePerByte;

            private double[] Time;
            private int TimeIterator;

            public double[] YToDoubleArray(List<XY<double>> xy, int LiczbaElementow)
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
                this.Time = Form1.linespace(0, TimePerByte * WiadaomoscLength * WiadaomoscLengthByte, SamplesPerByte * WiadaomoscLength * WiadaomoscLengthByte);

                BPSK(Frequency: DefaultFrequency);
                QPSK(Frequency: DefaultFrequency);
                Demodulacja("BPSK", 24);
                Demodulacja("QPSK", 75, -75);

                int LiczbaElementow = SamplesPerByte * WiadaomoscLength * WiadaomoscLengthByte;
                widmo_bpsk = Widmo.NewWidmo(YToDoubleArray(bpsk, LiczbaElementow), DefaultFrequency);
                widmo_qpsk = Widmo.NewWidmo(YToDoubleArray(qpsk, LiczbaElementow), DefaultFrequency);
            }

            public void BPSK(double Frequency)
            {
                bpsk = new List<XY<double>>();
                bpsksp = new List<XY<double>>();
                double x, y;
                TimeIterator = 0;
                for (int NumberOfLetter = 0; NumberOfLetter < WiadaomoscLength; NumberOfLetter++)
                {
                    for (int NumberOfByte = 0; NumberOfByte < WiadaomoscLengthByte; NumberOfByte++)
                        for (int NumberOfSample = 0; NumberOfSample < SamplesPerByte; NumberOfSample++)
                        {
                            x = Time[TimeIterator++];
                            if (Wiadomosc[NumberOfLetter, NumberOfByte] == 0)
                            {
                                y = Math.Sin(2.0 * Math.PI * Frequency * x);
                            }
                            else
                            {
                                y = Math.Sin(2.0 * Math.PI * Frequency * x + Math.PI);
                            }
                            bpsk.Add(new XY<double>(x, y));
                            y = Math.Sin(2.0 * Math.PI * Frequency * x);
                            bpsksp.Add(new XY<double>(x, y));
                        }
                }
            }

            public void QPSK(double Frequency)
            {
                qpsk = new List<XY<double>>();
                qpsksp = new List<XY<double>>();
                double x, y;
                TimeIterator = 0;
                for (int NumberOfLetter = 0; NumberOfLetter < WiadaomoscLength; NumberOfLetter++)
                {
                    for (int NumberOfByte = 0; NumberOfByte < WiadaomoscLengthByte; NumberOfByte += 2)
                        for (int NumberOfSample = 0; NumberOfSample < SamplesPerByte * 2; NumberOfSample++)
                        {
                            x = Time[TimeIterator++];
                            if (Wiadomosc[NumberOfLetter, NumberOfByte] == 0)
                            {
                                if (Wiadomosc[NumberOfLetter, NumberOfByte + 1] == 0)
                                {//00
                                    y = Math.Sin(2.0 * Math.PI * Frequency * x);
                                }
                                else
                                {//01
                                    y = Math.Sin(2.0 * Math.PI * Frequency * x + 3.0 / 4.0 * Math.PI);
                                }
                            }
                            else
                            {
                                if (Wiadomosc[NumberOfLetter, NumberOfByte + 1] == 0)
                                {//10
                                    y = Math.Sin(2.0 * Math.PI * Frequency * x + 7.0 / 4.0 * Math.PI);
                                }
                                else
                                {//11
                                    y = Math.Sin(2.0 * Math.PI * Frequency * x + 5.0 / 4.0 * Math.PI);
                                }
                                
                            }
                            qpsk.Add(new XY<double>(x, y));
                            qpsksp.Add(new XY<double>(x, Math.Sin(2.0 * Math.PI * Frequency * x)));
                        }
                }
            }

            public void Demodulacja(string TypKluczowania, double WartoscProgu, double WartoscProgu2 = 0)
            {
                int ElementIterator = 0;
                int NumberOfByte = 0;
                int zero = 0, jeden = 0;
               
                wiadomosc = new List<double>();

                switch (TypKluczowania)
                {
                    case "BPSK":
                        XB = new List<XY<double>>();
                        PB = new List<XY<double>>();
                        debpsk = new List<XY<double>>();
                        ElementIterator = 0;
                        NumberOfByte = 0;
                        zero = 0;
                        jeden = 0;
                        foreach (var Element in bpsk)
                        {
                            int p = NumberOfByte * SamplesPerByte + ElementIterator;
                            XB.Add(new XY<double>(Element.X, Element.Y * bpsksp[p].Y));
                            if (ElementIterator == 0)
                                PB.Add(XB[p]);
                            else
                                PB.Add(new XY<double>(Element.X, XB[p].Y + PB[p - 1].Y));

                            if (PB[p].Y >= WartoscProgu)
                            {
                                debpsk.Add(new XY<double>(Element.X, 0.0));
                                zero++;
                            }
                            else
                            {
                                debpsk.Add(new XY<double>(Element.X, 1.0));
                                jeden++;
                            }
                            if (++ElementIterator == SamplesPerByte)
                            {
                                for (int i = SamplesPerByte * NumberOfByte; i < SamplesPerByte * NumberOfByte + SamplesPerByte; i++)
                                    if (jeden >= zero) debpsk[i].Y = 1.0; else debpsk[i].Y = 0.0;

                                ElementIterator = 0;
                                NumberOfByte++;

                                zero = 0;
                                jeden = 0;
                            }
                        }
                        break;

                    case "QPSK":
                        XQ = new List<XY<double>>();
                        PQ = new List<XY<double>>();
                        deqpsk = new List<XY<double>>();

                        ElementIterator = 0;
                        NumberOfByte = 0;
                        foreach (var Element in qpsk)
                        {
                            deqpsk.Add(new XY<double>(Element.X, 0));
                            int p = NumberOfByte * SamplesPerByte + ElementIterator;
                            XQ.Add(new XY<double>(Element.X, Element.Y * qpsksp[p].Y));
                            if (ElementIterator == 0)
                                PQ.Add(XQ[p]);
                            else
                                PQ.Add(new XY<double>(Element.X, XQ[p].Y + PQ[p - 1].Y));
                            
                            if (++ElementIterator == SamplesPerByte * 2)
                            {
                                if (PQ[NumberOfByte * SamplesPerByte + SamplesPerByte * 2 - 1].Y >= WartoscProgu)
                                    for (int i = p; i > NumberOfByte * SamplesPerByte - 1; i--)
                                        deqpsk[i].Y = 2.0;

                                else if (PQ[NumberOfByte * SamplesPerByte + SamplesPerByte * 2 - 1].Y <= WartoscProgu2)
                                    for (int i = p; i > NumberOfByte * SamplesPerByte - 1; i--)
                                        deqpsk[i].Y = -2.0;

                                else if (PQ[NumberOfByte * SamplesPerByte + SamplesPerByte * 2 - 1].Y > 0)
                                    for (int i = p; i > NumberOfByte * SamplesPerByte - 1; i--)
                                        deqpsk[i].Y = 1.0;

                                else if (PQ[NumberOfByte * SamplesPerByte + SamplesPerByte * 2 - 1].Y <= 0)
                                    for (int i = p; i > NumberOfByte * SamplesPerByte - 1; i--)
                                        deqpsk[i].Y = -1.0;

                                if(deqpsk[NumberOfByte * SamplesPerByte + ElementIterator-1].Y == 1)
                                {
                                    wiadomosc.Add(1);
                                    wiadomosc.Add(0);
                                }
                                if (deqpsk[NumberOfByte * SamplesPerByte + ElementIterator - 1].Y == 2)
                                {
                                    wiadomosc.Add(0);
                                    wiadomosc.Add(0);
                                }
                                if (deqpsk[NumberOfByte * SamplesPerByte + ElementIterator - 1].Y == -1)
                                {
                                    wiadomosc.Add(0);
                                    wiadomosc.Add(1);
                                }
                                if (deqpsk[NumberOfByte * SamplesPerByte + ElementIterator - 1].Y == -2)
                                {
                                    wiadomosc.Add(1);
                                    wiadomosc.Add(1);
                                }
                                ElementIterator = 0;
                                NumberOfByte += 2;
                            }
                        }

                        break;
                }
            }
        }
        public class Widmo
        {
            public double[] x;
            public double[] y;
            public double fmin, fmax, w;
            public double fmint, fmaxt;

            public Widmo(int n)
            {
                this.x = new double[n / 2 - 1];
                this.y = new double[n / 2 - 1];
            }

            public static double[] ToComplex(double[] real)
            {
                int n = real.Length;
                double[] comp = new double[n * 2];
                for (int i = 0; i < n; i++)
                    comp[i * 2] = real[i];
                return comp;
            }

            public static double[] FFT(double[] data, bool real)
            {
                if (real)
                    data = ToComplex(data);
                int n = data.Length;
                IntPtr ptr = fftw.malloc(n * sizeof(double));
                Marshal.Copy(data, 0, ptr, n);
                IntPtr plan = fftw.dft_1d(n / 2, ptr, ptr, fftw_direction.Forward, fftw_flags.Estimate);
                fftw.execute(plan);
                double[] fft = new double[n];
                Marshal.Copy(ptr, fft, 0, n);
                fftw.destroy_plan(plan); fftw.free(ptr); fftw.cleanup();
                return fft;
            }

            public static Widmo NewWidmo(double[] wykres, double fs)
            {
                double[] fft = FFT(wykres, true);
                Widmo widmo = new Widmo(wykres.Length);
                for (int i = 0, f = 0; i < widmo.y.Length; i++, f += 2)
                {
                    widmo.x[i] = i * fs / wykres.Length;
                    widmo.y[i] = Math.Sqrt(Math.Pow(fft[f], 2) + Math.Pow(fft[f + 1], 2));
                    widmo.y[i] = 10.0 * Math.Log10(widmo.y[i]);
                }
                return widmo;
            }
        }
        public class MD
        {
            public Modulator k;
            public MD(string Wiadomosc, int SamplesPerByte, double TimePerByte, double DefaultAmplitude, double DefaultFrequency) => k = new Modulator(Form1.StringToByteArray(Wiadomosc), SamplesPerByte, TimePerByte, DefaultAmplitude, DefaultFrequency);
        }

        public MD hub = new MD(Wiadomosc: "a", SamplesPerByte: 100, TimePerByte: 1, DefaultAmplitude: 1.0, DefaultFrequency: 1.0);

        /*Funkcje i zmienne dla oxyplota*/
        public OxyPlot.WindowsForms.PlotView pv;
        public PlotModel pm;
        public bool export = true;
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

        public void DodajWykres(FunctionSeries fs)
        {
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
        public static FunctionSeries PointsToFunctionSeries(double x1, double y1, double x2, double y2)
        {
            FunctionSeries fs = new FunctionSeries();
            fs.Points.Add(new DataPoint(x1, y1));
            fs.Points.Add(new DataPoint(x2, y2));
            return fs;
        }
        /*Koniec*/
        
        private void MBPSK_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(hub.k.bpsk), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "MBPSK.png", 1900, 800, OxyColors.White);
        }

        private void DBPSK_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(hub.k.debpsk), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "DBPSK.png", 1900, 800, OxyColors.White);
        }

        private void MQPSK_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(hub.k.qpsk), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "MQPSK.png", 1900, 800, OxyColors.White);
        }

        private void DQPSK_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(hub.k.deqpsk), "time", "value");
            string wiadomosc = "";
            foreach (var i in hub.k.Wiadomosc)
                wiadomosc += i.ToString();
            string wiadomosc_po_zakodowaniu = "";
            foreach (var i in hub.k.wiadomosc)
                wiadomosc_po_zakodowaniu += i.ToString();
            MessageBox.Show("Wiadomość przed zakodowaniem: " + wiadomosc + "\nWiadomość przed zakodowaniem: " + wiadomosc_po_zakodowaniu);
            if (export) PngExporter.Export(this.pv.ActualModel, "MQPSK.png", 1900, 800, OxyColors.White);
        }

        private void WBPSK_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(hub.k.widmo_bpsk.x, hub.k.widmo_bpsk.y), "Frequency", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "WBPSK.png", 1900, 800, OxyColors.White);
        }

        private void WQPSK_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(hub.k.widmo_qpsk.x, hub.k.widmo_qpsk.y), "Frequency", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "WQPSK.png", 1900, 800, OxyColors.White);
        }

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

        private void DEMODULACJAXB_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(hub.k.XB), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "DBPSK X.png", 1900, 800, OxyColors.White);
        }

        private void DEMODULACJAPB_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(hub.k.PB), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "DBPSK P.png", 1900, 800, OxyColors.White);
        }

        private void DEMODULACJAXQ_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(hub.k.XQ), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "DQPSK X.png", 1900, 800, OxyColors.White);
        }

        private void DEMODULACJAPQ_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(hub.k.PQ), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "DQPSK P.png", 1900, 800, OxyColors.White);
        }
    }
}
