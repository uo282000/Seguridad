using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using Apoyo;
using System.Runtime.InteropServices;

namespace Practica1
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Ayuda ayuda = new Ayuda();

            Byte[] array = new Byte[64];

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

            crypto.GetBytes(array);

            ayuda.WriteHex(array, array.Length);

            crypto.Dispose();
        }
    }
}
