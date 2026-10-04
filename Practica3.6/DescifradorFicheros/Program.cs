using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DescifradorFicheros
{
    internal class Program
    {
        static void Main(string[] args)
        {
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

            AesCryptoServiceProvider proveedor = new AesCryptoServiceProvider();
            proveedor.Key = Clave;
            proveedor.IV = VI;

            FileStream input = new FileStream("Texto6Cifrado.bin",
                FileMode.Open, FileAccess.Read, FileShare.None);

            FileStream output = new FileStream("Texto6Descifrado.bin",
                FileMode.Create, FileAccess.Write, FileShare.None);

            ICryptoTransform descifrador = proveedor.CreateDecryptor();

            CryptoStream cryptoStream = new CryptoStream(
                input, descifrador, CryptoStreamMode.Read);

            byte[] buffer = new byte[4096];
            int leidos;

            while ((leidos = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, leidos);
            }

            cryptoStream.Close();
            descifrador.Dispose();
            output.Close();
        }
    }
}
