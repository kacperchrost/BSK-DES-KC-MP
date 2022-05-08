using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{

    public static class DESunio
    {
        //Dane algorytmu DES
        private static readonly int[] PC_1 = {57, 49, 41, 33, 25, 17, 9,
                                                1, 58, 50, 42, 34, 26, 18,
                                                10, 2, 59, 51, 43, 35, 27,
                                                19, 11, 3, 60, 52, 44, 36,
                                                63, 55, 47, 39, 31, 23, 15,
                                                7, 62, 54, 46, 38, 30, 22,
                                                14, 6, 61, 53, 45, 37, 29,
                                                21, 13, 5, 28, 20, 12, 4};
        private static readonly int[] PC_2 = {14, 17, 11, 24, 1, 5,
                                                3, 28, 15, 6, 21, 10,
                                                23, 19, 12, 4, 26, 8,
                                                16, 7, 27, 20, 13, 2,
                                                41, 52, 31, 37, 47, 55,
                                                30, 40, 51, 45, 33, 48,
                                                44, 49, 39, 56, 34, 53,
                                                46, 42, 50, 36, 29, 32};

        private static readonly int[] IP = {58, 50, 42, 34, 26, 18, 10, 2,
                                            60, 52, 44, 36, 28, 20, 12, 4,
                                            62, 54, 46, 38, 30, 22, 14, 6,
                                            64, 56, 48, 40, 32, 24, 16, 8,
                                            57, 49, 41, 33, 25, 17,  9, 1,
                                            59, 51, 43, 35, 27, 19, 11, 3,
                                            61, 53, 45, 37, 29, 21, 13, 5,
                                            63, 55, 47, 39, 31, 23, 15, 7},
                                    IP_1 = {40, 8, 48, 16, 56, 24, 64, 32,
                                            39, 7, 47, 15, 55, 23, 63, 31,
                                            38, 6, 46, 14, 54, 22, 62, 30,
                                            37, 5, 45, 13, 53, 21, 61, 29,
                                            36, 4, 44, 12, 52, 20, 60, 28,
                                            35, 3, 43, 11, 51, 19, 59, 27,
                                            34, 2, 42, 10, 50, 18, 58, 26,
                                            33, 1, 41, 9, 49, 17, 57, 25 };


        private static readonly int[] leftShift = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        private static readonly int[] Exp_Perm = {32, 1, 2, 3, 4, 5,
                                                        4, 5, 6, 7, 8, 9,
                                                        8, 9, 10, 11, 12, 13,
                                                        12, 13, 14, 15, 16, 17,
                                                        16, 17, 18, 19, 20, 21,
                                                        20, 21, 22, 23, 24, 25,
                                                        24, 25, 26, 27, 28, 29,
                                                        28, 29, 30, 31, 32, 1};
        private static readonly int[,] S1 = {{14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                                            { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
                                            { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                                            { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 }},
                                        S2 = {{15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
                                            { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
                                            { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
                                            { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 } },
                                        S3 = {{10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
                                            { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
                                            { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
                                            { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12} },
                                        S4 = {{7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
                                            { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
                                            { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
                                            { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14} },
                                        S5 = { {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
                                            { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
                                            { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
                                            { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3} },
                                        S6 = {{12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
                                            { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
                                            { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
                                            { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 } },
                                        S7 = {{4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
                                            { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
                                            { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
                                            { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0 ,15, 14, 2, 3, 12 } },
                                        S8 = {{13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
                                            { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
                                            { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
                                            { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 } };

        private static readonly int[] P = {16, 7, 20, 21,
                                            29, 12, 28, 17,
                                            1, 15, 23, 26,
                                            5, 18, 31, 10,
                                            2, 8, 24, 14,
                                            32, 27, 3, 9,
                                            19, 13, 30, 6,
                                            22, 11, 4, 25};



        //Konwertowanie.
        //string => int array
        private static int[] ToArray(string s)
        {
            int[] ints = s.ToCharArray().Where(x => int.TryParse(x.ToString(), out int myInt)).Select(x => int.Parse(x.ToString())).ToArray();
            return ints;
        }

        //Konwertowanie.
        //int array => string 
        private static string FromArrayToString(int[] array)
        {
            return string.Join("", array);
        }

        //Konwertowanie i wypełnienie do 4 bitów.
        //hex string => binary string
        private static string HexToBin4Bit(string hexadecimal)
        {
            string binary = "";
            foreach (char c in hexadecimal)
            {
                binary += Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0');
            }
            return binary;
        }

        //Konwertowanie.
        //binary string => hex string
        private static string BinToHex(string binary)
        {
            return string.Join("", Enumerable.Range(0, binary.Length / 4).Select(i => Convert.ToByte(binary.Substring(i * 4, 4), 2).ToString("X")));
        }

        //Konwertowanie.
        //binary string => decimal integer
        private static int BinToDec(string binary)
        {
            return Convert.ToInt32(binary, 2);
        }

        //Konwertowanie 4 bitów.
        //decimal value => binary text
        private static string DecToBin4Bit(int decimalValue)
        {
            return Convert.ToString(decimalValue, 2).PadLeft(4, '0');
        }

        //Konwertowanie 4 bitów.
        //int xor => 32bit string array 
        private static int[] ConvertXoredTo32Bits(int[] xored)
        {
            int packageIndex = 0, valueDecimal;
            string valueBinary, sString = "";

            //Konwertowanie 48bitów na 32bity
            for (int j = 0; j < xored.Length; j += 6)
            {
                //Pobieranie indeksu wiersza i kolumny w formacie binarnym z pakietu 6 bitów
                string rowIndexBin = xored[j].ToString() + xored[j + 5].ToString(),
                    columnIndexBin = FromArrayToString(xored.Skip(j + 1).Take(4).ToArray());

                //Konwertowanie na dziesiętny
                int rowIndex = BinToDec(rowIndexBin),
                    columnIndex = BinToDec(columnIndexBin);

                //Wybór odpowiedniej wartości z odpowiedniej tablicy
                valueDecimal = GetValueFromSNArray(packageIndex, rowIndex, columnIndex);

                //Konwertowanie wartości na binarną
                valueBinary = DecToBin4Bit(valueDecimal);
                sString += valueBinary;
                packageIndex++;
                valueBinary = "";
            }
            return ToArray(sString);
        }

        //Przesuwanie bitów w lewo jeden lub dwa razy w zależności od tablicy 'leftShift'.
        private static int[] ShiftToLeft(int[] toShift, int shiftCount)
        {
            int[] shifted = new int[toShift.Length];
            for (int j = 0; j < shiftCount; j++)
            {
                if (j == 1)
                {
                    shifted.CopyTo(toShift, 0);
                }

                for (int i = 0; i < toShift.Length; i++)
                {
                    shifted[i] = i == toShift.Length - 1 ? toShift.First() : toShift[i + 1];
                }
            }
            return shifted;
        }

        //Zwracanie permutowanej tablicy o określonym rozmiarze na podstawie tablicy 'permutationRule'.
        private static int[] Permute(int[] toPermute, int[] permutationRule, int outputSize)
        {
            int[] output = new int[outputSize];
            for (int i = 0; i < outputSize; i++)
            {
                output[i] = toPermute[permutationRule[i] - 1];
            }
            return output;
        }

        //Zwracanie wartości dziesiętnej w podanym wierszu i kolumnie z tablicy 'S1'-'S8' na podstawie 'packageIndex'.
        private static int GetValueFromSNArray(int packageIndex, int rowIndex, int columnIndex)
        {
            int valueDecimal = 0;
            switch (packageIndex)
            {
                case 0:
                    valueDecimal = S1[rowIndex, columnIndex];
                    break;
                case 1:
                    valueDecimal = S2[rowIndex, columnIndex];
                    break;
                case 2:
                    valueDecimal = S3[rowIndex, columnIndex];
                    break;
                case 3:
                    valueDecimal = S4[rowIndex, columnIndex];
                    break;
                case 4:
                    valueDecimal = S5[rowIndex, columnIndex];
                    break;
                case 5:
                    valueDecimal = S6[rowIndex, columnIndex];
                    break;
                case 6:
                    valueDecimal = S7[rowIndex, columnIndex];
                    break;
                case 7:
                    valueDecimal = S8[rowIndex, columnIndex];
                    break;
            }
            return valueDecimal;
        }

        //Rozdzielenie tablicy wejściowej na równe parzyste tablice wyjściowe.
        private static void SplitArray(int[] sourceArray, out int[] firstArray, out int[] secondArray)
        {
            int halfOfSize = sourceArray.Length / 2;
            firstArray = new int[halfOfSize];
            secondArray = new int[halfOfSize];
            Array.Copy(sourceArray, 0, firstArray, 0, halfOfSize);
            Array.Copy(sourceArray, halfOfSize, secondArray, 0, halfOfSize);
        }

        //Generowanie permutowanych kluczy.
        private static List<int[]> GeneratePermutatedKeys(int[] binaryKey)
        {
            int[] key56B = Permute(binaryKey, PC_1, 56); // 56bitowy 'key' po permutacji ('K+')
            SplitArray(key56B, out int[] C, out int[] D); // Rozdziela 56-bitowy klucz na dwie 28-bitowe tablice

            List<int[]> joinedPermutatedKeys = new List<int[]>(); //Lista połączonych 'C' i 'D' po permutacji PC_2


            //Przesuwanie 'C' i 'D', łączenie ich i permutowanie
            for (int i = 0; i < 16; i++)
            {
                C = ShiftToLeft(C, leftShift[i]);
                D = ShiftToLeft(D, leftShift[i]);

                int[] CD = C.Concat(D).ToArray();
                int[] CD48B = Permute(CD, PC_2, 48);  //48bit

                joinedPermutatedKeys.Add(CD48B);
            }

            return joinedPermutatedKeys;
        }


        /// <summary>
        /// KODOWANIE
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <param name="binHex"></param>
        /// <returns></returns>
        public static string Encoding(string message, string key, bool binHex)
        {
            //Konwertowanie ciągu znaków szesnastkowych na binarną tablicę int lub ciąg binarny na binarną tablicę int
            int[] binaryMsg;
            //bin - true, hex - false
            if (binHex) binaryMsg = ToArray(message);
            else binaryMsg = ToArray(HexToBin4Bit(message));
            int[] binaryKey = ToArray(HexToBin4Bit(key));

            //Generowanie kluczy
            List<int[]> joinedPermutatedKeys = GeneratePermutatedKeys(binaryKey);

            //Podzielenie 'message' na 64bitowe części
            List<int[]> messageParts = new List<int[]>();
            for (int i = 0; i < binaryMsg.Length; i += 64)
            {
                int[] part = new int[(binaryMsg.Length - i) < 64 ? binaryMsg.Length - i : 64];
                Array.Copy(binaryMsg, i, part, 0, (binaryMsg.Length - i) < 64 ? binaryMsg.Length - i : 64);
                messageParts.Add(part);
            }
            if (binaryMsg.Length % 64 != 0)
            {
                int[] last = messageParts.Last();
                last = last.Append(1).ToArray();
                while (last.Length != 64)
                {
                    last = last.Append(0).ToArray();
                }
                messageParts.RemoveAt(messageParts.Count - 1);
                messageParts.Add(last);
            }

            StringBuilder sb = new StringBuilder();


            //Kodowanie
            foreach (int[] binaryMessage in messageParts)
            {
                int[] messageIP = Permute(binaryMessage, IP, 64);  //64bitowy 'message' po permutacji

                //Podzielenie 'message' na dwie 32bitowe tablice
                SplitArray(messageIP, out int[] Lprev, out int[] Rprev);

                int[] E = new int[48]; //'E(Rn-1)'

                //Przetworzenie 'message'
                for (int i = 0; i < 16; i++)
                {
                    int[] R = new int[32], xored = new int[48]; //'Kn'+'E(Rn-1)'
                    int[] L = Rprev;

                    //Permutacja 'E(Rn-1)'
                    E = Permute(L, Exp_Perm, 48);

                    //Xorowanie 'E(Rn-1)' z 'Kn'
                    for (int j = 0; j < E.Length; j++)
                    {
                        xored[j] = E[j] ^ joinedPermutatedKeys.ElementAt(i)[j];
                    }

                    //Xorowane po konwersji na podstawie tabel 'S'
                    int[] S = ConvertXoredTo32Bits(xored);
                    int[] f = Permute(S, P, 32);

                    for (int j = 0; j < R.Length; j++)
                    {
                        R[j] = Lprev[j] ^ f[j];
                    }

                    Lprev = L;
                    Rprev = R;
                }

                int[] finalRL = Permute(Rprev.Concat(Lprev).ToArray(), IP_1, 64);
                string hexEncriptedM = BinToHex(FromArrayToString(finalRL));
                sb.Append(hexEncriptedM);
            }

            return sb.ToString();
        }


        /// <summary>
        /// DEKODOWANIE
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <param name="binHex"></param>
        /// <returns></returns>
        public static string Decoding(string message, string key, bool binHex)
        {
            //Konwertowanie ciągu znaków szesnastkowych na binarną tablicę int lub ciąg binarny na binarną tablicę int
            int[] binaryMsg;

            //bin - true, hex - false
            if (binHex) binaryMsg = ToArray(message);
            else binaryMsg = ToArray(HexToBin4Bit(message));
            int[] binaryKey = ToArray(HexToBin4Bit(key));

            //Generowanie kluczy
            List<int[]> joinedPermutatedKeys = GeneratePermutatedKeys(binaryKey);

            //Podzielenie 'message' na 64bitowe części
            List<int[]> messageParts = new List<int[]>();
            for (int i = 0; i < binaryMsg.Length; i += 64)
            {
                int[] part = new int[64];
                Array.Copy(binaryMsg, i, part, 0, (binaryMsg.Length - i) < 64 ? binaryMsg.Length - i : 64);
                messageParts.Add(part);
            }

            StringBuilder sb = new StringBuilder();


            //Dekodowanie
            foreach (int[] binaryMessage in messageParts)
            {
                int[] messageIP = Permute(binaryMessage, IP, 64);  //64bitowy 'message' po permutacji

                //Podzielenie 'message' na dwie 32bitowe tablice
                SplitArray(messageIP, out int[] Lprev, out int[] Rprev);

                int[] E = new int[48]; //'E(Rn-1)'

                //Przetwarzanie 'message'
                for (int i = 15; i >= 0; i--)
                {
                    int[] R = new int[32], xored = new int[48]; //'Kn+E(Rn-1)'
                    int[] L = Rprev;

                    //Permutacja 'E(Rn-1)'
                    E = Permute(L, Exp_Perm, 48);

                    //Xorowanie 'E(Rn-1)' z 'Kn'
                    for (int j = 0; j < E.Length; j++)
                    {
                        xored[j] = E[j] ^ joinedPermutatedKeys.ElementAt(i)[j];
                    }

                    //Xorowane po konwersji na podstawie tabel 'S'
                    int[] S = ConvertXoredTo32Bits(xored);
                    int[] f = Permute(S, P, 32);

                    for (int j = 0; j < R.Length; j++)
                    {
                        R[j] = Lprev[j] ^ f[j];
                    }

                    Lprev = L;
                    Rprev = R;
                }

                int[] finalRL = Permute(Rprev.Concat(Lprev).ToArray(), IP_1, 64);
                string hexDecriptedM = BinToHex(FromArrayToString(finalRL));

                //Sprawdzenie czy zaszyfrowany blok ma dopełnienie i następnie usuwanie go
                if (hexDecriptedM.LastIndexOf("80") > -1)
                {
                    int indexOf80 = hexDecriptedM.LastIndexOf("80");
                    hexDecriptedM = hexDecriptedM.Remove(indexOf80, hexDecriptedM.Length - indexOf80);
                }
                sb.Append(hexDecriptedM);
            }

            return sb.ToString();
        }
    }
}
