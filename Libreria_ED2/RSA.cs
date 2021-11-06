using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Libreria_ED2
{
    class RSA
    {

        const int longitud = 1000;
        public static int e;

        public int MCD(int a, int b)
        {
            int restante;
            do
            {
                restante = b;
                b = a % b;
                a = restante;
            }
            while (b != 0);
            return restante;
        }
        public void GenerarLlaves(int ValorP, int ValorQ)
        {
            var p = ValorP;
            var q = ValorQ;
            var n = p * q;
            var QN = (p - 1) * (q - 1);

            int CountA = 0;
            int CountB = 0;

            for (var x = 2; x < QN; x++)
            {
                CountA = MCD(x, n);
                CountB = MCD(x, QN);
                if ((CountA == 1) && (CountB == 1))
                {
                    e = x;
                    break;
                }
            }

            var Temp = 0;
            int d = 2; // Encontrar el valor de D
            do
            {
                d++;
                Temp = (d * e) % QN;
            }
            while (Temp != 1);
            var RutaOrigen = Environment.CurrentDirectory + "\\temp";

            using (var Ws = new FileStream(RutaOrigen + "/" + "private.Key", FileMode.OpenOrCreate))//Escribiendo llave privada
            {
                using (var Writer = new StreamWriter(Ws))
                {
                    Writer.Write(n.ToString() + "," + d.ToString());
                }
                Ws.Close();
            }

            using (var Ws2 = new FileStream(RutaOrigen + "/" + "public.Key", FileMode.OpenOrCreate))//Escribiendo llave privada
            {
                using (var Writer2 = new StreamWriter(Ws2))
                {
                    Writer2.Write(n.ToString() + "," + e.ToString());
                }
                Ws2.Close();
            }
        }
    }
}
