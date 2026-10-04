using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using Apoyo;
using System.Runtime.InteropServices;
using System.IO;

namespace Practica1
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Ayuda ayuda = new Ayuda();

            Byte[] array = new Byte[64];
            
            //Generamos un número aleatorio
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

            //escribimos byte a byte el número en un array
            crypto.GetBytes(array);

            //cerramos porque ya no lo necesitamos
            crypto.Dispose();

            //creamos un array donde guardamos el número ya cifrado
            Byte[] arrayCifrado = ProtectedData.Protect(array, null, DataProtectionScope.CurrentUser);

            //lo sacamos por pantalla
            ayuda.WriteHex(arrayCifrado, arrayCifrado.Length);

            //creamos el fichero donde vamos a almacenar el número cifrado
            FileInfo fichero = new FileInfo("C:\\Users\\pablo\\Documents\\cifrado.bin");

            //Guradamos el número en el fichero
            ayuda.GuardaBufer(fichero.FullName, arrayCifrado);

            //Creamos un array para almacenar el array cifrado que vamos a leer del fichero
            Byte[] leido = new byte[ayuda.BytesFichero(fichero.FullName)];

            //Leemos el número cifrado del fichero y lo metemos en el array "leido"
            ayuda.CargaBufer(fichero.FullName, leido);


            //Descriframos el array y lo guardamos en un buffer
            Byte[] arrayDescifrado = ProtectedData.Unprotect(leido, null, DataProtectionScope.CurrentUser);

            ProtectedMemory.Protect(arrayDescifrado, MemoryProtectionScope.SameLogon);

            Console.Write("\n");
            ayuda.WriteHex(arrayDescifrado, arrayDescifrado.Length);

            ProtectedMemory.Unprotect(arrayDescifrado, MemoryProtectionScope.SameLogon);

            Console.Write("\n");

            //Imprimimos por pantalla el buffer descifrado
            ayuda.WriteHex(arrayDescifrado, arrayDescifrado.Length);

        }
    }
}
