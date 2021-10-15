using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Lab_Cifrados.Models;
using Libreria_ED2;

namespace Lab_Cifrados.Controllers
{
    [Route("api")]
    [ApiController]
    public class CifradoresController : Controller
    {

        public static IWebHostEnvironment rutasDeSubida;
        public CifradoresController(IWebHostEnvironment _rutas)
        {
            rutasDeSubida = _rutas;
        }

        [HttpPost]
        [Route("cipher/{method}")]
        public async Task<IActionResult> Cifrar([FromForm] SubirArchivo objetoArchivo, string method, [FromForm]string Key)
        {
            if (objetoArchivo.File.Length > 0)
            {

                if (!Directory.Exists(rutasDeSubida.WebRootPath + "\\Archivos\\"))
                {
                    Directory.CreateDirectory(rutasDeSubida.WebRootPath + "\\Archivos\\");
                }
                using (FileStream stream = System.IO.File.Create(rutasDeSubida.WebRootPath + "\\Archivos\\" + objetoArchivo.File.FileName))
                {
                    objetoArchivo.File.CopyTo(stream);
                    stream.Flush();
                }

                string nombreArchivo = objetoArchivo.File.FileName;
                string[] arregloNombre = nombreArchivo.Split('.');
                nombreArchivo = arregloNombre[0];

                if (method=="César"||method == "césar"||method == "Cesar"||method == "cesar")
                {
                    CifradorCesar cesar = new CifradorCesar(1024);
                    cesar.Cifrar(rutasDeSubida.WebRootPath + "\\Archivos\\" + objetoArchivo.File.FileName, rutasDeSubida.WebRootPath + "\\Archivos\\", Key, nombreArchivo);

                    var bytesArchivo = System.IO.File.ReadAllBytesAsync(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombreArchivo + ".csr");
                    var bytes = System.IO.File.ReadAllBytes(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombreArchivo + ".csr");
                    var objetoStream = new MemoryStream(bytes);
                    return File(objetoStream, "application/octet-stream", nombreArchivo + ".csr");
                }
                else if (method=="ZigZag"|| method == "zigzag")
                {
                    CifradorZigzag zigzag = new CifradorZigzag();
                    zigzag.Cifrar(rutasDeSubida.WebRootPath + "\\Archivos\\" + objetoArchivo.File.FileName, rutasDeSubida.WebRootPath + "\\Archivos\\", Convert.ToInt32(Key), nombreArchivo);

                    var bytesArchivo = System.IO.File.ReadAllBytesAsync(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombreArchivo + ".zz");
                    var bytes = System.IO.File.ReadAllBytes(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombreArchivo + ".zz");
                    var objetoStream = new MemoryStream(bytes);
                    return File(objetoStream, "application/octet-stream", nombreArchivo + ".zz");
                }
                else
                {
                    return StatusCode(500);

                }

            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("sdes/cipher/{nombre}")]
        public async Task<IActionResult> CifrarSDES([FromForm] SubirArchivo objetoArchivo, string nombre, [FromForm] string Key)
        {
            int llave10bitsvalidar = Convert.ToInt32(Key);
            if (objetoArchivo.File.Length > 0 && llave10bitsvalidar<1024)
            {

                if (!Directory.Exists(rutasDeSubida.WebRootPath + "\\Archivos\\"))
                {
                    Directory.CreateDirectory(rutasDeSubida.WebRootPath + "\\Archivos\\");
                }
                using (FileStream stream = System.IO.File.Create(rutasDeSubida.WebRootPath + "\\Archivos\\" + objetoArchivo.File.FileName))
                {
                    objetoArchivo.File.CopyTo(stream);
                    stream.Flush();
                }

                string nombreArchivo = objetoArchivo.File.FileName;
                string[] arregloNombre = nombreArchivo.Split('.');
                nombreArchivo = arregloNombre[0];

                CifradorSDES sdes = new CifradorSDES(1024);
                sdes.Cifrar(rutasDeSubida.WebRootPath + "\\Archivos\\" + objetoArchivo.File.FileName, rutasDeSubida.WebRootPath + "\\Archivos\\", rutasDeSubida.WebRootPath + "\\Archivos\\" + "Permutaciones.txt", nombre, Convert.ToInt32(Key));

                var bytesArchivo = System.IO.File.ReadAllBytesAsync(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombre + ".sdes");
                var bytes = System.IO.File.ReadAllBytes(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombre + ".sdes");
                var objetoStream = new MemoryStream(bytes);
                return File(objetoStream, "application/octet-stream", nombre + ".sdes");

            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("decipher")]
        public async Task<IActionResult> Decifrar([FromForm] SubirArchivo objetoArchivo,[FromForm] string Key)
        {
            if (objetoArchivo.File.Length > 0)
            {

                if (!Directory.Exists(rutasDeSubida.WebRootPath + "\\Archivos\\"))
                {
                    Directory.CreateDirectory(rutasDeSubida.WebRootPath + "\\Archivos\\");
                }
                using (FileStream stream = System.IO.File.Create(rutasDeSubida.WebRootPath + "\\Archivos\\" + objetoArchivo.File.FileName))
                {
                    objetoArchivo.File.CopyTo(stream);
                    stream.Flush();
                }

                string nombreArchivo = objetoArchivo.File.FileName;
                string[] arregloNombre = nombreArchivo.Split('.');
                nombreArchivo = arregloNombre[0];
                string extension = arregloNombre[1];

                if (extension=="csr")
                {
                    CifradorCesar cesar = new CifradorCesar(1024);
                    cesar.Decifrar(rutasDeSubida.WebRootPath + "\\Archivos\\" + objetoArchivo.File.FileName, rutasDeSubida.WebRootPath + "\\Archivos\\", Key, nombreArchivo);

                    var bytesArchivo = System.IO.File.ReadAllBytesAsync(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombreArchivo + ".txt");
                    var bytes = System.IO.File.ReadAllBytes(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombreArchivo + ".txt");
                    var objetoStream = new MemoryStream(bytes);
                    return File(objetoStream, "application/octet-stream", nombreArchivo + ".txt");
                }
                else if(extension=="zz")
                {
                    CifradorZigzag zigzag = new CifradorZigzag();
                    zigzag.Descifrar(rutasDeSubida.WebRootPath + "\\Archivos\\" + objetoArchivo.File.FileName, rutasDeSubida.WebRootPath + "\\Archivos\\", Convert.ToInt32(Key), nombreArchivo);

                    var bytesArchivo = System.IO.File.ReadAllBytesAsync(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombreArchivo + ".txt");
                    var bytes = System.IO.File.ReadAllBytes(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombreArchivo + ".txt");
                    var objetoStream = new MemoryStream(bytes);
                    return File(objetoStream, "application/octet-stream", nombreArchivo + ".txt");

                }
                else
                {
                    return StatusCode(500);
                }
                
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("sdes/decipher")]
        public async Task<IActionResult> DecifrarSDES([FromForm] SubirArchivo objetoArchivo, [FromForm] string Key)
        {
            int llave10bitsvalidar = Convert.ToInt32(Key);

            if (objetoArchivo.File.Length > 0 && llave10bitsvalidar<1024)
            {
                
                if (!Directory.Exists(rutasDeSubida.WebRootPath + "\\Archivos\\"))
                {
                    Directory.CreateDirectory(rutasDeSubida.WebRootPath + "\\Archivos\\");
                }
                using (FileStream stream = System.IO.File.Create(rutasDeSubida.WebRootPath + "\\Archivos\\" + objetoArchivo.File.FileName))
                {
                    objetoArchivo.File.CopyTo(stream);
                    stream.Flush();
                }

                string nombreArchivo = objetoArchivo.File.FileName;
                string[] arregloNombre = nombreArchivo.Split('.');
                nombreArchivo = arregloNombre[0];
                string extension = arregloNombre[1];

                CifradorSDES sdes = new CifradorSDES(2048);
                string nombreOriginal = sdes.Decifrar(rutasDeSubida.WebRootPath + "\\Archivos\\" + objetoArchivo.File.FileName, rutasDeSubida.WebRootPath + "\\Archivos\\", rutasDeSubida.WebRootPath + "\\Archivos\\" + "Permutaciones.txt", Convert.ToInt32(Key));

                var bytesArchivo = System.IO.File.ReadAllBytesAsync(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombreOriginal);
                var bytes = System.IO.File.ReadAllBytes(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombreOriginal);
                var objetoStream = new MemoryStream(bytes);
                return File(objetoStream, "application/octet-stream", nombreOriginal);

            }
            else
            {
                return StatusCode(500);
            }
        }


    }
}
