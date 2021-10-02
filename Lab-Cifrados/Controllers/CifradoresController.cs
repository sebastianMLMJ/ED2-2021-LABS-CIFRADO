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
        public async Task<IActionResult> Comprimir([FromForm] SubirArchivo objetoArchivo, string method, [FromForm]string key)
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

                CifradorCesar cesar = new CifradorCesar(1024);
                cesar.Cifrar(rutasDeSubida.WebRootPath + "\\Archivos\\" + objetoArchivo.File.FileName, rutasDeSubida.WebRootPath + "\\Archivos\\",key,nombreArchivo);
      
                var bytesArchivo = System.IO.File.ReadAllBytesAsync(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombreArchivo + ".csr");
                var bytes = System.IO.File.ReadAllBytes(rutasDeSubida.WebRootPath + "\\Archivos\\" + nombreArchivo + ".csr");
                var objetoStream = new MemoryStream(bytes);
                return File(objetoStream, "application/octet-stream", nombreArchivo + ".csr");
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("decipher")]
        public async Task<IActionResult> descomprimir([FromForm] SubirArchivo objetoArchivo,[FromForm] string key)
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

                CifradorCesar cesar = new CifradorCesar(1024);
                cesar.Decifrar(rutasDeSubida.WebRootPath + "\\Archivos\\" + objetoArchivo.File.FileName, rutasDeSubida.WebRootPath + "\\Archivos\\", key, nombreArchivo);

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


    }
}
