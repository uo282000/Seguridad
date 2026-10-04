using Apoyo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Practica3._5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ayuda ayuda = new Ayuda();
            int TamClave = 16;
            byte[] Clave = new byte[TamClave];

            for (int i = 0; i < Clave.Length; i++)
            {
                Clave[i] = (byte)(i % 256);
            }
            byte[] VI = new byte[TamClave];
            for (int i = 0; i < VI.Length; i++)
            {
                VI[i] = (byte)((i + 160) % 256);
            }

            string[] TextoPlano =
            {
                "a", "b", "c", "d", "e", "f", "g", "h", "i",
                "j", "k", "l", "m", "n", "o", "p", "q", "r",
                "s", "t", "u", "v", "w", "x", "y", "z"
            };

            //Pruebas parte 4:cambiar el tipo de servicio criptograficao
            AesCryptoServiceProvider proveedor = new AesCryptoServiceProvider();
            //AesManaged proveedor = new AesManaged();
            Console.WriteLine("Configuracion por defecto");
            Console.WriteLine("BlockSize: " + proveedor.BlockSize);
            Console.WriteLine("KeySize: " + proveedor.KeySize);
            Console.WriteLine("Padding: " + proveedor.Padding);
            Console.WriteLine("Mode: " + proveedor.Mode);

            proveedor.KeySize = TamClave * 8;
            proveedor.Padding = PaddingMode.PKCS7;
            proveedor.Mode = CipherMode.CBC;

            Console.WriteLine("\nConfiguracion Asignada");
            Console.WriteLine("KeySize: " + proveedor.KeySize);
            Console.WriteLine("Padding: " + proveedor.Padding);
            Console.WriteLine("Mode: " + proveedor.Mode);

            proveedor.GenerateKey();

            Console.WriteLine("\nClave asignada: ");
            //De esta manera imprimimos toda su longitud
            ayuda.WriteHex(proveedor.Key, proveedor.Key.Length);

            proveedor.Key = Clave;

            Console.WriteLine("\nClave tamaño elegido generada: ");
            ayuda.WriteHex(proveedor.Key, proveedor.Key.Length);

            proveedor.GenerateIV();
            Console.WriteLine("\nVector de inicialización: ");
            //De esta manera imprimimos toda su longitud
            ayuda.WriteHex(proveedor.IV, proveedor.IV.Length);

            proveedor.IV = VI;

            Console.WriteLine("\nVector de inicialización: ");
            ayuda.WriteHex(proveedor.IV, proveedor.IV.Length);




            //Proceso de cifrado de un array de bytes 

            FileStream fichero = new FileStream("zz_TextoCifrado.bin",
                FileMode.Create, FileAccess.Write, FileShare.None);

            ICryptoTransform cifrador = proveedor.CreateEncryptor();

            CryptoStream cryptoStream = new CryptoStream(
                fichero, cifrador, CryptoStreamMode.Write);
            
            StreamWriter escritor = new StreamWriter(cryptoStream);

            foreach (string letra in TextoPlano)
            {
                escritor.WriteLine(letra);
            }

            //cerramos las herramientas
            escritor.Close();
            cifrador.Dispose();

            //Proceso de descifrado de un array de bytes 

            FileStream ficheroCifrado = new FileStream("zz_TextoCifrado.bin",
                FileMode.Open, FileAccess.Read, FileShare.None);

            ICryptoTransform descifrador = proveedor.CreateDecryptor();

            CryptoStream cryptoStream2 = new CryptoStream(
                ficheroCifrado, descifrador, CryptoStreamMode.Read);

            StreamReader lector = new StreamReader(cryptoStream2);

            string cadenaDescifrada = lector.ReadToEnd();

            //cerramos las herramientas
            lector.Close();
            descifrador.Dispose();

            Console.WriteLine("\nTexto Descifrado");
            Console.WriteLine(cadenaDescifrada);
        }
    }
}
