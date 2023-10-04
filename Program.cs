using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converti_Dec_Bin_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inserisci un indirizzo IP decimale puntato (es. 10.10.10.10): ");
            string input = Console.ReadLine();

            if (ValidaInput(input))
            {
                int[] decimalePuntato = ParsaInput(input);

                bool[] risultatoBinario = Converti_DP_To_Binario(decimalePuntato);

                Console.WriteLine($"Risultato binario: {risultatoBinario}");
                //StampaArrayBool(risultatoBinario);
                
            }

            Console.ReadLine();
        }

        static bool ValidaInput(string input)
        {
            // Verifica se l'input ha il formato corretto (X.X.X.X)
            string[] parti = input.Split('.');
            if (parti.Length != 4)
            {
                return false;
            }

            // Verifica se ciascuna parte è un numero valido
            foreach (string parte in parti)
            {
                if (!int.TryParse(parte, out int numero) || numero < 0 || numero > 255)
                {
                    return false;
                }
            }

            return true;
        }

        static int[] ParsaInput(string input)
        {
            string[] parti = input.Split('.');
            int[] decimalePuntato = new int[4];

            for (int i = 0; i < 4; i++)
            {
                decimalePuntato[i] = int.Parse(parti[i]);
            }

            return decimalePuntato;
        }

        static bool[] Converti_DP_To_Binario(int[] decimalePuntato)
        {
            bool[] risultatoBinario = new bool[32];
            int indiceBinario = 0;

            foreach (int numero in decimalePuntato)
            {
                string binario = Convert.ToString(numero, 2).PadLeft(8, '0');

                for (int i = 0; i < 8; i++)
                {
                    risultatoBinario[indiceBinario] = binario[i] == '1';
                    indiceBinario++;
                }
            }

            return risultatoBinario;
        }
    }
}
