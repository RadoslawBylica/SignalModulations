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
using System.Runtime.InteropServices;

namespace Lab5
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

    public class Kluczowanie
    {
        public List<XY<double>> ask;
        public List<XY<double>> fsk;
        public List<XY<double>> psk;

        public List<XY<double>> sa;
        public List<XY<double>>[] sn;
        public List<XY<double>> sp;

        public List<XY<double>> askX;
        public List<XY<double>>[] fskX;
        public List<XY<double>> pskX;

        public List<XY<double>> askP;
        public List<XY<double>> fskP;
        public List<XY<double>> pskP;

        public List<XY<double>> askM;
        public List<XY<double>> fskM;
        public List<XY<double>> pskM;

        private byte[,] Wiadomosc;
        private int WiadaomoscLength;
        private int WiadaomoscLengthByte;

        private int SamplesPerByte;
        private double TimePerByte;

        private double[] Time;
        private int TimeIterator;

        public Kluczowanie(byte[,] wiadomosc, int SamplesPerByte, double TimePerByte, double DefaultAmplitude, double DefaultFrequency)
        {
            this.Wiadomosc = wiadomosc;
            this.WiadaomoscLength = wiadomosc.GetLength(0);
            this.WiadaomoscLengthByte = wiadomosc.GetLength(1);

            this.SamplesPerByte = SamplesPerByte;
            this.TimePerByte = TimePerByte;
            this.Time = Form1.linespace(0, TimePerByte * WiadaomoscLength * WiadaomoscLengthByte, SamplesPerByte * WiadaomoscLength * WiadaomoscLengthByte);

            ASK(AmplitudeWhen0: DefaultAmplitude, AmplitudeWhen1: DefaultAmplitude * 5.0, Frequency: DefaultFrequency);
            FSK(FrequencyWhen0: DefaultFrequency, FrequencyWhen1: DefaultFrequency * 5.0);
            PSK(Frequency: DefaultFrequency);

            Demodulacja("ASK", 100);
            Demodulacja("FSK", 20);
            Demodulacja("PSK", 20);
        }

        public void ASK(double AmplitudeWhen0, double AmplitudeWhen1, double Frequency)
        {
            ask = new List<XY<double>>();
            sa = new List<XY<double>>();
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
                            y = AmplitudeWhen0 * Math.Sin(2.0 * Math.PI * Frequency * x);
                        }
                        else
                        {
                            y = AmplitudeWhen1 * Math.Sin(2.0 * Math.PI * Frequency * x);
                        }
                        ask.Add(new XY<double>(x, y));
                        sa.Add(new XY<double>(x, y));
                    }
            }
        }

        public void FSK(double FrequencyWhen0, double FrequencyWhen1)
        {
            fsk = new List<XY<double>>();
            sn = new List<XY<double>>[2];
            sn[0] = new List<XY<double>>();
            sn[1] = new List<XY<double>>();
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
                            y = Math.Sin(2.0 * Math.PI * FrequencyWhen0 * x);
                        }
                        else
                        {
                            y = Math.Sin(2.0 * Math.PI * FrequencyWhen1 * x);
                        }
                        fsk.Add(new XY<double>(x, y));

                        y = Math.Sin(2.0 * Math.PI * FrequencyWhen0 * x);
                        sn[0].Add(new XY<double>(x, y));

                        y = Math.Sin(2.0 * Math.PI * FrequencyWhen1 * x);
                        sn[1].Add(new XY<double>(x, y));
                    }
            }
        }

        public void PSK(double Frequency)
        {
            psk = new List<XY<double>>();
            sp = new List<XY<double>>();
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
                        psk.Add(new XY<double>(x, y));

                        y = Math.Sin(2.0 * Math.PI * Frequency * x);
                        sp.Add(new XY<double>(x, y));
                    }
            }
        }

        public void Demodulacja(string TypKluczowania, double WartoscProgu)
        {
            int ElementIterator = 0;
            int NumberOfByte = 0;
            int zero = 0, jeden = 0;

            switch (TypKluczowania)
            {
                case "ASK":
                    askX = new List<XY<double>>();
                    askP = new List<XY<double>>();
                    askM = new List<XY<double>>();
                    ElementIterator = 0;
                    NumberOfByte = 0;
                    zero = 0; 
                    jeden = 0;
                    foreach(var Element in ask)
                    {
                        int p = NumberOfByte * SamplesPerByte + ElementIterator;
                        askX.Add(new XY<double>(Element.X, Element.Y * sa[p].Y));
                        if (ElementIterator == 0)
                            askP.Add(askX[p]);
                        else
                            askP.Add(new XY<double>(Element.X, askX[p].Y + askP[p - 1].Y));

                        if (askP[p].Y < WartoscProgu)
                        {
                            askM.Add(new XY<double>(Element.X, 0.0));
                            zero++;
                        }
                        else
                        {
                            askM.Add(new XY<double>(Element.X, 1.0));
                            jeden++;
                        }
                            
                        ElementIterator++;
                        if (ElementIterator == SamplesPerByte)
                        {
                            for (int i = SamplesPerByte * NumberOfByte; i < SamplesPerByte * NumberOfByte + SamplesPerByte; i++)
                                if (jeden >= zero) askM[i].Y = 1.0; else askM[i].Y = 0.0;

                            ElementIterator = 0;
                            NumberOfByte++;

                            zero = 0;
                            jeden = 0;
                        }
                    }
                    break;

                case "PSK":
                    pskX = new List<XY<double>>();
                    pskP = new List<XY<double>>();
                    pskM = new List<XY<double>>();
                    ElementIterator = 0;
                    NumberOfByte = 0;
                    zero = 0;
                    jeden = 0;
                    foreach (var Element in psk)
                    {
                        int p = NumberOfByte * SamplesPerByte + ElementIterator;
                        pskX.Add(new XY<double>(Element.X, Element.Y * sp[p].Y));
                        if (ElementIterator == 0)
                            pskP.Add(pskX[p]);
                        else
                            pskP.Add(new XY<double>(Element.X, pskX[p].Y + pskP[p - 1].Y));

                        if (pskP[p].Y >= WartoscProgu)
                        {
                            pskM.Add(new XY<double>(Element.X, 0.0));
                            zero++;
                        }
                        else
                        {
                            pskM.Add(new XY<double>(Element.X, 1.0));
                            jeden++;
                        }

                        ElementIterator++;
                        if (ElementIterator == SamplesPerByte)
                        {
                            for (int i = SamplesPerByte * NumberOfByte; i < SamplesPerByte * NumberOfByte + SamplesPerByte; i++)
                                if (jeden >= zero) pskM[i].Y = 1.0; else pskM[i].Y = 0.0;

                            ElementIterator = 0;
                            NumberOfByte++;

                            zero = 0;
                            jeden = 0;
                        }
                    }
                    break;

                case "FSK":
                    fskX = new List<XY<double>>[2]; fskX[0] = new List<XY<double>>(); fskX[1] = new List<XY<double>>();
                    fskP = new List<XY<double>>();
                    fskM = new List<XY<double>>();
                    ElementIterator = 0;
                    NumberOfByte = 0;
                    zero = 0;
                    jeden = 0;
                    foreach (var Element in fsk)
                    {
                        int p = NumberOfByte * SamplesPerByte + ElementIterator;
                        fskX[0].Add(new XY<double>(Element.X, Element.Y * sn[0][p].Y));
                        fskX[1].Add(new XY<double>(Element.X, Element.Y * sn[1][p].Y));
                        if (ElementIterator == 0)
                            fskP.Add(new XY<double>(Element.X, fskX[0][p].Y - fskX[1][p].Y));
                        else
                            fskP.Add(new XY<double>(Element.X, fskX[0][p].Y - fskX[1][p].Y + fskP[p - 1].Y));

                        if (fskP[p].Y >= WartoscProgu)
                        {
                            fskM.Add(new XY<double>(Element.X, 0.0));
                            zero++;
                        }
                        else
                        {
                            fskM.Add(new XY<double>(Element.X, 1.0));
                            jeden++;
                        }

                        ElementIterator++;
                        if (ElementIterator == SamplesPerByte)
                        {
                            for (int i = SamplesPerByte * NumberOfByte; i < SamplesPerByte * NumberOfByte + SamplesPerByte; i++)
                                if (jeden >= zero) fskM[i].Y = 1.0; else fskM[i].Y = 0.0;

                            ElementIterator = 0;
                            NumberOfByte++;

                            zero = 0;
                            jeden = 0;
                        }
                    }
                    break;
            }
                
        }
    }
    
    public class Lab5
    {
        public Kluczowanie k;
        public Lab5(string Wiadomosc, int SamplesPerByte, double TimePerByte, double DefaultAmplitude, double DefaultFrequency) => k = new Kluczowanie(Form1.StringToByteArray(Wiadomosc), SamplesPerByte, TimePerByte, DefaultAmplitude, DefaultFrequency);
    }

    public partial class Form1 : Form
    {
        public static Lab5 lab5 = new Lab5(Wiadomosc: "a", SamplesPerByte: 100, TimePerByte: 1.0, DefaultAmplitude: 1.0, DefaultFrequency: 1.0);

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

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(1900, 800);
        }

        /*Wyświetlanie*/
        private void za_ask_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(lab5.k.ask), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "za.png", 1900, 800, OxyColors.White);
        }

        private void x_ask_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(lab5.k.askX), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "zaX.png", 1900, 800, OxyColors.White);
        }

        private void p_ask_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(lab5.k.askP), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "zaP.png", 1900, 800, OxyColors.White);
        }

        private void m_ask_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(lab5.k.askM), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "zaM.png", 1900, 800, OxyColors.White);
        }

        private void zf_fsk_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(lab5.k.fsk), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "zf.png", 1900, 800, OxyColors.White);
        }

        private void x1_fsk_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(lab5.k.fskX[0]), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "zfX1.png", 1900, 800, OxyColors.White);
        }

        private void x2_fsk_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(lab5.k.fskX[1]), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "zfX2.png", 1900, 800, OxyColors.White);
        }

        private void p_fsk_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(lab5.k.fskP), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "zfP.png", 1900, 800, OxyColors.White);
        }

        private void m_fsk_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(lab5.k.fskM), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "zfM.png", 1900, 800, OxyColors.White);
        }

        private void zp_psk_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(lab5.k.psk), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "zp.png", 1900, 800, OxyColors.White);
        }

        private void x_psk_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(lab5.k.pskX), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "zpX.png", 1900, 800, OxyColors.White);
        }

        private void p_psk_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(lab5.k.pskP), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "zpP.png", 1900, 800, OxyColors.White);
        }

        private void m_psk_Click(object sender, EventArgs e)
        {
            Wykres(ToFunctionSeries(lab5.k.pskM), "time", "value");
            if (export) PngExporter.Export(this.pv.ActualModel, "zpM.png", 1900, 800, OxyColors.White);
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

        public static byte[,] StringToByteArray(string s)
        {
            byte[,] b = new byte[s.Length, sizeof(char) * 8];
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < sizeof(char) * 8; j++)
                    b[i, j] = (byte)((s[i] >> j) & 0x01);
            return b;
        }
    }
}
