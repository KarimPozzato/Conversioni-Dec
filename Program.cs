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
            string input = Console.ReadLine(); //chiediamo in input il decimale puntato

            if (ValidaInput(input, out int[] decimalePuntato))
            {
                bool[] risultatoBinario = Convet_DP_To_Binario(decimalePuntato);

                Console.WriteLine("Risultato in binario:");
                StampaArrayBool(risultatoBinario);

                int risultatoIntero = Convert_DP_To_Intero(decimalePuntato);
                Console.WriteLine($"Da decimale puntato a intero: {risultatoIntero}");

                int risultatoInteroDaBinario = Convert_Binario_To_Intero(risultatoBinario);
                Console.WriteLine($"Da binario a intero: {risultatoInteroDaBinario}");

                int[] risultatoDecimalePuntatoDaBinario = Convert_Binario_To_DP(risultatoBinario);
                Console.WriteLine($"Da binario a decimale puntato: {FormattaIndirizzoIP(risultatoDecimalePuntatoDaBinario)}");
            }
            else
            {
                Console.WriteLine("Input non valido.");
                Console.WriteLine("Inserire un indirizzo IP decimale puntato valido.");
            }

            Console.ReadLine();
        }

        //Andiamo a convalidare l'input che ci viene fornito

        static bool ValidaInput(string input, out int[] decimalePuntato)
        {
            string[] parti = input.Split('.');
            decimalePuntato = new int[4];

            if (parti.Length != 4) //Controlliamo che la lunghezza del decimale puntato sia di 4
            {
                return false;
            }

            for (int i = 0; i < 4; i++) //controlliamo per ogni intervallo che sia dnetro il range 0 a 255
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
            // Crea un array di booleani di lunghezza 32 per rappresentare il risultato binario
            bool[] risultatoBinario = new bool[32];
            // Variabile per tenere traccia dell'indice corrente nell'array binario
            int indiceBinario = 0;

            // Itera attraverso ciascun numero nell'array decimalePuntato (quattro numeri in totale)
            foreach (int numero in decimalePuntato)
            {
                // Itera attraverso i bit di ciascun numero, dal bit più significativo (7) al meno significativo (0)
                for (int j = 7; j >= 0; j--)
                {
                    /*1 << j: Questo è uno shift bit a sinistra, che sposta il bit "1" di "j" posizioni a sinistra. Ad esempio, se j è 3, allora 1 << j diventerà 8 (binario: 1000)
                     * numero: Questo è il tuo operando di partenza, che rappresenta un numero intero in binario.
                     * numero & (1 << j): Questa è l'operazione di AND bit per bit tra "numero" e il risultato dello shift a sinistra. 
                     * Questo significa che verrà eseguito un AND tra ciascun bit di "numero" e il bit corrispondente nella rappresentazione binaria di 1 << j. 
                     * Se entrambi i bit sono 1 = true, il risultato sarà 1; altrimenti false, sarà 0.*/
                     
                    risultatoBinario[indiceBinario] = (numero & (1 << j)) != 0;
                    // Incrementa l'indice nell'array binario
                    indiceBinario++;
                }
            }

            // Restituisce l'array risultatoBinario rappresentante la forma binaria dell'indirizzo IP decimale puntato
            return risultatoBinario;
        }

        static void StampaArrayBool(bool[] array)
        {
            // Itera attraverso ogni valore booleano nell'array
            foreach (bool valore in array)
            {
                /*All'interno del ciclo, questa riga utilizza un operatore di controllo per decidere cosa stampare sulla console. 
                 * Se valore è true, verrà stampato "1"; se valore è false, verrà stampato "0". 
                 * In questo modo, si traducono i valori booleani in rappresentazioni binarie.*/
                Console.Write(valore ? "1" : "0");
            }
            Console.WriteLine();
        }

        static int Convert_DP_To_Intero(int[] decimalePuntato)
        {
            int risultatoIntero = 0;

            // Itera attraverso ciascun numero nell'array decimalePuntato (quattro numeri in totale)
            foreach (int numero in decimalePuntato)
            {
                // Sposta il risultatoIntero di 8 bit a sinistra (equivalente a moltiplicare per 256)
                risultatoIntero = (risultatoIntero << 8) | numero;
            }

            return risultatoIntero;
        }

        static int Convert_Binario_To_Intero(bool[] binario)
        {
            int risultatoIntero = 0;

            // Itera attraverso ciascun bit nell'array binario
            foreach (bool bit in binario)
            { 
                /* In questa riga, il valore corrente di risultatoIntero viene shiftato a sinistra di 1 bit (risultatoIntero << 1). 
                 * Questo è equivalente a moltiplicare il valore attuale per 2. 
                 * Poi, il bit corrente (bit) viene combinato con risultatoIntero tramite un'operazione di OR bit per bit (|). 
                 * Se bit è true (1), allora viene aggiunto 1 a risultatoIntero; altrimenti, viene aggiunto 0. 
                 * Questa operazione viene ripetuta per ogni bit nella sequenza binaria, accumulando i bit booleani nell'intero risultante.*/
                 
                risultatoIntero = (risultatoIntero << 1) | (bit ? 1 : 0);
            }

            return risultatoIntero;
        }

        static int[] Convert_Binario_To_DP(bool[] binario)
        {
            // Inizializza un array di interi per memorizzare il risultato del decimale puntato
            int[] risultatoDecimalePuntato = new int[4];

            for (int i = 0; i < 4; i++)
            {
                // Inizializza una variabile per memorizzare il valore dell'ottetto
                int valore = 0;

                // Itera attraverso i 8 bit di ciascun ottetto
                for (int j = 0; j < 8; j++)
                {
                    //  Il bit corrente dalla sequenza binaria (binario[i * 8 + j]) viene combinato con valore tramite un'operazione di OR bit per bit (|).
                    //  Se il bit è true (1), viene aggiunto 1 a valore; altrimenti, viene aggiunto 0.
                    //  Questo processo ripetuto per ciascuno dei 8 bit di ogni ottetto.
                    valore = (valore << 1) | (binario[i * 8 + j] ? 1 : 0);
                }

                // Assegna il valore dell'ottetto al risultato dell'indirizzo IP decimale puntato
                risultatoDecimalePuntato[i] = valore;
            }

            return risultatoDecimalePuntato;
        }

        static string FormattaIndirizzoIP(int[] decimalePuntato)
        {
            /* il metodo string.Join unisce gli elementi dell'array decimalePuntato in una singola stringa. 
             * Il separatore tra gli elementi è specificato come ".", il che significa che ogni elemento dell'array verrà separato da un punto nella stringa risultante.*/
            return string.Join(".", decimalePuntato);
        }
    }    
}
