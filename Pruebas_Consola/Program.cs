using System;
using Libreria_ED2;
namespace Pruebas_Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            CifradorSDES tester = new CifradorSDES(1024);

            tester.Cifrar("C:\\ABF\\cuento.txt", "C:\\ABF", "C:\\ABF\\Permutaciones.txt", "ejemplo.txt",364);
            

        }
    }
}
