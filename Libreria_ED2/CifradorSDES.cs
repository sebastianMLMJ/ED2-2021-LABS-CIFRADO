using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Libreria_ED2
{
    public class CifradorSDES:CifradorInterfaz
    {
        int longitudBuffer;

        public CifradorSDES(int _longitudBuffer)
        {
            longitudBuffer = _longitudBuffer;
        }

        public void Cifrar(string dirLectura, string dirEscritura, string dirPermutaciones, string nombre, int llave)
        {
            //Convirtiendo llave a bits
            string llaveBinaria = Convert.ToString(llave, 2);
            llaveBinaria=llaveBinaria.PadLeft(10, '0');
            char[] llavearreglo = llaveBinaria.ToCharArray();

            //Configurando permutaciones
            StreamReader sr = new StreamReader(new FileStream(dirPermutaciones, FileMode.OpenOrCreate));
            string lineaLectura = sr.ReadLine();
            string permutacion;
            int[] p10 = new int [10];
            int[] p8 = new int[8];

            while (lineaLectura!=null)
            {
                string[] div1 = lineaLectura.Split(':');
                string[] div2 = div1[1].Split(',');
                switch (div1[0])
                {
                    case "p10":
                        for (int i = 0; i < p10.Length; i++)
                        {
                            p10[i] = Convert.ToInt32(div2[i]);
                        }
                        break;
                    case "p8":
                        for (int i = 0; i < p8.Length; i++)
                        {
                            p8[i] = Convert.ToInt32(div2[i]);
                        }
                        break;
                    default:
                        break;
                }
                lineaLectura=sr.ReadLine();

            }
            //entp10 = entrada-permutacion10, salp10 = salida permutacion10
            char[] entp10 = llavearreglo;
            char[] salp10;
            salp10 = Permutar(entp10, p10);

            //generando k1
            char[] leftsh1 = LeftShiftUno(salp10);
            char[] k1 = Permutar(leftsh1, p8);
            string llave1="";
            for (int i = 0; i < k1.Length; i++)
            {
                llave1 += k1[i].ToString();
            }
            
            

            //generando k2
            char[] leftsh2 = LeftShiftDos(leftsh1);
            char[] k2 = Permutar(leftsh2, p8);

            string llave2 = "";
            for (int i = 0; i < k1.Length; i++)
            {
                llave2 += k2[i].ToString();
            }

            Console.WriteLine(llave1);
            Console.WriteLine(llave2);



        }

        private char[] Permutar(char[] entrada, int[]permutacion)
        {
            char[] salida = new char[permutacion.Length];
            for (int i = 0; i < permutacion.Length; i++)
            {
                salida[i] = entrada[permutacion[i]];
            }
            return salida;
        }
        
        private char[] LeftShiftUno(char[] entrada)
        {
            int mitad = entrada.Length / 2;
            LinkedList<char> salida1 = new LinkedList<char>();
            LinkedList<char> salida2 = new LinkedList<char>();

            for (int i = 0; i < mitad; i++)
            {
                salida1.AddLast(entrada[i]); 
            }
           
            for (int i = mitad; i < entrada.Length; i++)
            {
                salida2.AddLast(entrada[i]);
            }

            char primerCaracter = salida1.First.Value;
            salida1.RemoveFirst();
            salida1.AddLast(primerCaracter);

            primerCaracter = salida2.First.Value;
            salida2.RemoveFirst();
            salida2.AddLast(primerCaracter);

            char[] union = new char[entrada.Length];
            int posicion = 0;

            foreach (var item in salida1)
            {
                union[posicion] = item;
                posicion++;
            }
            foreach (var item in salida2)
            {
                union[posicion] = item;
                posicion++;
            }

            return union;
        }

        private char[] LeftShiftDos(char[] entrada)
        {
            int mitad = entrada.Length / 2;
            LinkedList<char> salida1 = new LinkedList<char>();
            LinkedList<char> salida2 = new LinkedList<char>();

            for (int i = 0; i < mitad; i++)
            {
                salida1.AddLast(entrada[i]);
            }

            for (int i = mitad; i < entrada.Length; i++)
            {
                salida2.AddLast(entrada[i]);
            }

            char primerCaracter; 
            primerCaracter = salida1.First.Value;
            salida1.RemoveFirst();
            salida1.AddLast(primerCaracter);

            primerCaracter = salida1.First.Value;
            salida1.RemoveFirst();
            salida1.AddLast(primerCaracter);

            primerCaracter = salida2.First.Value;
            salida2.RemoveFirst();
            salida2.AddLast(primerCaracter);

            primerCaracter = salida2.First.Value;
            salida2.RemoveFirst();
            salida2.AddLast(primerCaracter);

            char[] union = new char[entrada.Length];
            int posicion = 0;

            foreach (var item in salida1)
            {
                union[posicion] = item;
                posicion++;
            }
            foreach (var item in salida2)
            {
                union[posicion] = item;
                posicion++;
            }

            return union;
        }
    }
}
