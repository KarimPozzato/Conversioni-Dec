using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Bin_Dec_3
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("Inserisci un indirizzo IP decimale puntato (es. 10.10.10.10): ");
            string input = Console.ReadLine();

            if (ValidaInput(input, out int[] decimalePuntato))
            {
                bool[] risultatoBinario = Convet_DP_To_Binario(decimalePuntato);

                Console.WriteLine("Risultato in binario:");
                StampaArrayBool(risultatoBinario);

                int risultatoIntero = Convert_DP_To_Intero(decimalePuntato);
                Console.WriteLine($"Da intero a decimale puntato: {risultatoIntero}");

                int risultatoInteroDaBinario = Convert_Binario_To_Intero(risultatoBinario);
                Console.WriteLine($"Da intero a binario: {risultatoInteroDaBinario}");

                int[] risultatoDecimalePuntatoDaBinario = Convert_Binario_To_DP(risultatoBinario);
                Console.WriteLine($"Da decimale puntato a binario: {risultatoDecimalePuntatoDaBinario}");
            }
            else
            {
                Console.WriteLine("Input non valido.");
                Console.WriteLine("Inserire un indirizzo IP decimale puntato valido.");
            }

            Console.ReadLine();
        }

        static bool ValidaInput(string input, out int[] decimalePuntato)
        {
            string[] parti = input.Split('.');
            decimalePuntato = new int[4];

            if (parti.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < 4; i++)
            {
                if (!int.TryParse(parti[i], out decimalePuntato[i]) || decimalePuntato[i] < 0 || decimalePuntato[i] > 255)
                {
                    return false;
                }
            }

            return true;
        }

        static bool[] Convet_DP_To_Binario(int[] decimalePuntato)
        {
            bool[] risultatoBinario = new bool[32];
            int indiceBinario = 0;

            foreach (int numero in decimalePuntato)
            {
                for (int j = 7; j >= 0; j--)
                {
                    risultatoBinario[indiceBinario] = (numero & (1 << j)) != 0;
                    indiceBinario++;
                }
            }

            return risultatoBinario;
        }

        static int Convert_DP_To_Intero(int[] decimalePuntato)
        {
            int risultatoIntero = 0;

            foreach (int numero in decimalePuntato)
            {
                risultatoIntero = (risultatoIntero << 8) | numero;
            }

            return risultatoIntero;
        }

        static int Convert_Binario_To_Intero(bool[] binario)
        {
            int risultatoIntero = 0;

            foreach (bool bit in binario)
            {
                risultatoIntero = (risultatoIntero << 1) | (bit ? 1 : 0);
            }

            return risultatoIntero;
        }

        static int[] Convert_Binario_To_DP(bool[] binario)
        {
            int[] risultatoDecimalePuntato = new int[4];

            for (int i = 0; i < 4; i++)
            {
                int valore = 0;

                for (int j = 0; j < 8; j++)
                {
                    valore = (valore << 1) | (binario[i * 8 + j] ? 1 : 0);
                }

                risultatoDecimalePuntato[i] = valore;
            }

            return risultatoDecimalePuntato;
        }

        static void StampaArrayBool(bool[] array)
        {
            foreach (bool valore in array)
            {
                Console.Write(valore ? "1" : "0");
            }
            Console.WriteLine();
        }
    }    
}
