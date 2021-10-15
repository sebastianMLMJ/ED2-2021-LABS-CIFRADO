using System;
using Libreria_ED2;
using System.IO;
namespace Pruebas_Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            CifradorSDES tester = new CifradorSDES(1024);
            //int sem = 211;
            //byte prueba = Convert.ToByte(sem);
            //BinaryWriter bw = new BinaryWriter(new FileStream("C:\\ABF\\ejemplosdes.txt",FileMode.OpenOrCreate));
            //bw.Write(prueba);
            //bw.Close();
            
            tester.Cifrar("C:\\ABF\\hard-test.txt", "C:\\ABF\\", "C:\\ABF\\Permutaciones.txt", "cifradosdes",364);
            tester.Decifrar("C:\\ABF\\cifradosdes.sdes", "C:\\ABF\\Decifrados\\", "C:\\ABF\\Permutaciones.txt", 364);
            

        }
    }
}
