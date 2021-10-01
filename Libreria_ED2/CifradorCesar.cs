using System;
using System.Collections.Generic;
using System.Text;

namespace Libreria_ED2
{
    public class CifradorCesar
    {
        int longitudBuffer;

        public CifradorCesar(int _longitudBuffer)
        {
            longitudBuffer = _longitudBuffer;
        }

        public void Cifrar(string dirLectura, string dirEscritura, string clave)
        {

            char[] letrasClave = clave.ToCharArray();
            Dictionary<char, byte> eliminarRepetidosClave = new Dictionary<char, byte>();
            Dictionary<byte, byte> abcModificado = new Dictionary<byte, byte>();
            LinkedList<byte> abcModPrevio = new LinkedList<byte>();
            byte iterador = 0;
            int cantLeida = 0;

            for (int i = 0; i < letrasClave.Length; i++)
            {
                if (eliminarRepetidosClave.ContainsKey(letrasClave[i]) == false)
                {
                    eliminarRepetidosClave.Add(letrasClave[i], Convert.ToByte(letrasClave[i]));
                }
            }

            for (int i = 0; i < 256; i++)
            {
                abcModPrevio.AddLast(Convert.ToByte(i));
            }

        }
    }
}
