using System;
using Libreria_ED2;
namespace Pruebas_Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            CifradorCesar testercesar = new CifradorCesar(1024);
            testercesar.Cifrar("C:\\ABF\\Tarea.txt", "C:\\ABF\\", "dinosaurio", "ejemplocesar");
            testercesar.Decifrar("C:\\ABF\\ejemplocesar.csr", "C:\\ABF\\", "dinosaurio", "ejemplocesar");
        }
    }
}
