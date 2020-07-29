using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadingSignal
{
    class GoldCode
    {
        public GoldCode()
        {
            Signal = new List<int>();
            SamplesPerValue = 100;
        }
        
        List<int> Signal;
        public int SamplesPerValue;

        public double[] GetSignal
        {
            get
            {
                List<int> ExtendetSignal = new List<int>();
                foreach (var Value in Signal)
                    for (int Samples = 0; Samples < SamplesPerValue; Samples++)
                        ExtendetSignal.Add(Value);

                double[] ret = new double[ExtendetSignal.Count];
                for (int Iterator = 0; Iterator < ExtendetSignal.Count; Iterator++)
                        ret[Iterator] = ExtendetSignal[Iterator];
                return ret;
            }
        }

        public int[] GetMyMessage(string Code)
        {
            int[] CodeInt = new int[Code.Length];
            for (int Iterator = 0; Iterator < Code.Length; Iterator++)
                CodeInt[Iterator] = (int)Code[Iterator] - 48;

            List<int> Message = new List<int>();
            int TemporaryValue = 0;
            for(int Iterator = 0, CodeIterator = 0; Iterator < Signal.Count; Iterator++, CodeIterator++)
            {
                if (CodeInt[CodeIterator] == 0)
                    TemporaryValue += Signal[Iterator];
                else
                    TemporaryValue += Signal[Iterator] * -1;

                if (CodeIterator == Code.Length - 1)
                {
                    Message.Add(TemporaryValue / Code.Length);
                    CodeIterator = -1;
                    TemporaryValue = 0;
                }
            }

            int[] MyMessage = new int[Message.Count];
            for (int Iterator = 0; Iterator < Message.Count; Iterator++)
                if(Message[Iterator] == -1 )
                    MyMessage[Iterator] = 1;
                else
                    MyMessage[Iterator] = 0;

            return MyMessage;
        }

        public void Add(string Message, string Code)
        {
            int[] MessageInt = new int[Message.Length];
            for(int Iterator = 0; Iterator < Message.Length; Iterator++)
                MessageInt[Iterator] = (int)Message[Iterator] - 48;

            int[] CodeInt = new int[Code.Length];
            for (int Iterator = 0; Iterator < Code.Length; Iterator++)
                CodeInt[Iterator] = (int)Code[Iterator] - 48;


            List<int> BitStream = new List<int>();
            foreach (var Bit in MessageInt)
                for(int Iterator = 0; Iterator < CodeInt.Length; Iterator++)
                    BitStream.Add(Bit^CodeInt[Iterator]);

            for (int Iterator = 0; Iterator < BitStream.Count; Iterator++)
                if (BitStream[Iterator] == 0)
                    BitStream[Iterator] = 1;
                else
                    BitStream[Iterator] = -1;

            if (BitStream.Count > Signal.Count)
                for (int Iterator = Signal.Count; Iterator < BitStream.Count; Iterator++)
                    Signal.Add(0);

            for (int Iterator = 0; Iterator < BitStream.Count; Iterator++)
                Signal[Iterator] += BitStream[Iterator];
        }

        public static int[] GenerateGoldCode(int[] InitState)
        {
            List<int> GoldCode = new List<int>();
            List<int> State1 = new List<int>(InitState);
            List<int> State2 = new List<int>(InitState);
            List<int> Output1 = new List<int>();
            List<int> Output2 = new List<int>();
            for (int i = 0; i < Math.Pow(2, InitState.Length)-1; i++)
            {
                int XorValue1 = State1[1] ^ State1[4];
                int XorValue2 = State2[1] ^ State2[3] ^ State2[4];

                Output1.Add(State1[4]);
                Output2.Add(State2[4]);

                State1.Insert(0, XorValue1);
                State2.Insert(0, XorValue2);
            }

            int O1Zera = 0;
            int O2Zera = 0;

            for(int i = 0; i < Output1.Count; i++)
            {
                if (Output1[i] == 0) O1Zera++;
                if (Output2[i] == 0) O2Zera++;
                GoldCode.Add(Output1[i] ^ Output2[i]);
            }

            string G="";
            foreach(var i in GoldCode)
                G += i.ToString();

            string S = "";
            foreach (var i in InitState)
                S += i.ToString();

            string O1 = "";
            foreach (var i in Output1)
                O1 += i.ToString();

            string O2 = "";
            foreach (var i in Output2)
                O2 += i.ToString();

            MessageBox.Show(
                "Wygenerowany kod Golda: " + G + ".\n" +
                "Dla startowej sekwencji: " + S + ".\n" +
                "Kod Golda składa się z dwóch m sekwencji o rozmiarze " + GoldCode.Count + ".\n" +
                "Sekwencja dla 1+x^2+x^5: " + O1 + ".\n" +
                "Zera: " + O1Zera + " Jedynki: " + (Output1.Count - O1Zera) + ".\n" +
                "Sekwencja dla 1+x^2+x^4+x^5: " + O2 + ".\n" +
                "Zera: " + O2Zera + " Jedynki: " + (Output2.Count - O2Zera) + ".\n");
            return GoldCode.ToArray();
        }

        public static object GenerateMatrixH (int Size)
        {
            MLApp.MLApp matlab = new MLApp.MLApp();

            matlab.Execute(@"cd C:\Users\Nekoszi");

            object result = null;

            matlab.Feval("GenerateMatrixH", 1, out result, Size);

            matlab.Quit();

            object[] res = result as object[];
            return res[0];
        }
    }
}
