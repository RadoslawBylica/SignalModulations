using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FFTWSharp;

namespace PTD
{
    public class Widmo
    {
        public double[] x;
        public double[] y;
        public double[] yLog;

        public Widmo(int n)
        {
            this.x = new double[n / 2 - 1];
            this.y = new double[n / 2 - 1];
            this.yLog = new double[n / 2 - 1];
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

        public static Widmo NewWidmo(double[] wykres, double fs = 250)
        {
            double[] fft = FFT(wykres, true);
            Widmo widmo = new Widmo(wykres.Length);
            double max = double.NaN;
            for (int i = 0, f = 0; i < widmo.y.Length; i++, f += 2)
            {
                widmo.x[i] = i * fs / wykres.Length;
                widmo.y[i] = Math.Sqrt(Math.Pow(fft[f], 2) + Math.Pow(fft[f + 1], 2));
                widmo.y[i] *= (2.0 / wykres.Length);
                widmo.yLog[i] = 10.0 * Math.Log10(widmo.y[i]);
                if (double.IsNaN(max)) max = widmo.yLog[i];
                else if (widmo.yLog[i] > max) max = widmo.yLog[i];
            }

            for (int i = 0; i < widmo.y.Length; i++)
            {
                if (widmo.yLog[i] > max / 100000) widmo.yLog[i] = 0;
            }
            return widmo;
        }
    }
}
