using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    /// <summary>
    /// 
    /// </summary>
    class LFSR_Line
    {
        public List<int> Line; //pojedyncza linia
        public int xor { get; set; } //wynik xor

        public LFSR_Line()
        {
            Line = new List<int>();
            xor = -1;
        }

        public int XOR_Method(int x, int y)
        {
            if (x == 0 && y == 0) return 0;
            if (x == 1 && y == 0) return 1;
            if (x == 0 && y == 1) return 1;
            if (x == 1 && y == 1) return 0;
            return -1;
        }
    }

    class LFSR
    {
        private int Repeat;
        private string Taps;
        List<int> LFSR_Result; //Wynikowa lista LFSR!!!
        List<int> Randoms = new List<int>();

        public LFSR(string Repeat, string Taps)
        {
            this.Repeat = int.Parse(Repeat);
            this.Taps = Taps;
        }
        public static int[] ToArray(string s)
        {
            int[] ints = s.ToCharArray().Where(x => int.TryParse(x.ToString(), out int myInt)).Select(selector: x => int.Parse(x.ToString())).ToArray();
            return ints;
        }

        public List<int> Generate()
        {
            //DANE TWORZONE PRZEZ PROGRAM
            LFSR_Result = new List<int>(); //Wynikowa lista LFSR!!!
            var rand = new Random(); //Losowanie liczb startowych
            int numcol = -1; //ilosc kolumn, tworzone odczytując M
            List<int> Order = new List<int>(); //tablica inicjatyw, , tworzone odczytując M
            LFSR_Line NewLineObject = new LFSR_Line(); //Obiekt pojedynczej linii


            //ODCZYTYWANIE KLUCZA
            //Console.WriteLine("Ordering...");
            for (int i = 0; i < Taps.Length; i++)
            {
                //Szukamy znaku x
                if (Taps[i] == 'x' || Taps[i] == 'X')
                {
                    if (((int)Taps[i + 2] >= 48 && (int)Taps[i + 2] <= 57))
                    {
                        Order.Add((int)Taps[i + 2] - 48); //zapisujemy liczbę potęgi przy x'ie
                    }
                }
            }

            //Znalezienie liczby kolumn
            foreach (int o in Order)
            { if (numcol < o) numcol = o; }

            //WYGENEROWANIE PIERWSZEJ LINII
            //Console.WriteLine("Preparing...");
            for (int i = 0; i < numcol; i++)
            {
                int r = rand.Next(2);
                NewLineObject.Line.Add(r);
                Randoms.Add(r);
            }

            //Główna Pętla
            //Console.WriteLine("LOOPing...");
            for (int c = 0; c < Repeat; c++)
            {
                //Console.WriteLine("XORing...");
                int x = NewLineObject.Line[Order[0] - 1];
                int y = NewLineObject.Line[Order[1] - 1];
                for (int i = 1; i < Order.Count; i++)
                {
                    y = NewLineObject.Line[Order[i] - 1];
                    x = NewLineObject.XOR_Method(x, y);
                }
                NewLineObject.xor = x;
                LFSR_Result.Add(NewLineObject.Line[numcol - 1]); //dodajemy nowy wynik równy najmłodszemu bitowi

                //Console.WriteLine("Creating new NewLineObject ...");
                for (int i = numcol - 1; i > 0; i--) //Patrzymy na linię od końca
                    NewLineObject.Line[i] = NewLineObject.Line[i - 1]; //Zmieniamy obecny obiekt NewLineObject
                NewLineObject.Line[0] = NewLineObject.xor; //ustawiamy najstarszy bit na xor
            }

            return LFSR_Result;
        }

        public List<int> GetRandoms()
        {
            return Randoms;
        }


    }
}
