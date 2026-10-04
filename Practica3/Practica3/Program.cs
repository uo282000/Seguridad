using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Apoyo;

namespace Practica3
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
                Clave[i] = (byte)(i%256);
            }
            byte[] VI = new byte[TamClave];
            for (int i = 0; i < VI.Length; i++)
            {
                VI[i] = (byte)((i + 160) % 256);
            }
            byte[] TextoPlano =
            {
                0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
                0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
                0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
                0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
                0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
                0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F
            };

            AesCryptoServiceProvider proveedor = new AesCryptoServiceProvider();
            Console.WriteLine("Configuracion por defecto");
            Console.WriteLine("BlockSize: " + proveedor.BlockSize);
            Console.WriteLine("KeySize: "+proveedor.KeySize);
            Console.WriteLine("Padding: "+proveedor.Padding);
            Console.WriteLine("Mode: "+proveedor.Mode);

            proveedor.KeySize = 128;
            proveedor.Padding = PaddingMode.ISO10126;
            proveedor.Mode = CipherMode.ECB;

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

            cryptoStream.Write(TextoPlano, 0, TextoPlano.Length);

            cryptoStream.Flush();
            
            cryptoStream.Close();

            cifrador.Dispose();

            fichero.Close();

            //Proceso de descifrado de un array de bytes 

            byte[] TextoDescifrado = new byte[new FileInfo("zz_TextoCifrado.bin").Length];

            FileStream ficheroCifrado = new FileStream("zz_TextoCifrado.bin",
                FileMode.Open, FileAccess.Read, FileShare.None);

            ICryptoTransform descifrador = proveedor.CreateDecryptor();

            CryptoStream cryptoStream2 = new CryptoStream(
                ficheroCifrado, descifrador, CryptoStreamMode.Read);

            cryptoStream2.Read(TextoDescifrado, 0, TextoDescifrado.Length);

            cryptoStream2.Flush();
            cryptoStream2.Close();
            descifrador.Dispose();
            ficheroCifrado.Close();

            Console.WriteLine("\nTexto Descifrado");
            ayuda.WriteHex(TextoDescifrado, TextoDescifrado.Length);
        }
    }
}
