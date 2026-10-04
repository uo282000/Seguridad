using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Practica_3._7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Contraseña: ");
            string Contra = Console.ReadLine();
            byte[] Sal = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 };

            Rfc2898DeriveBytes Generador = new Rfc2898DeriveBytes(Contra, Sal, 1000);

            byte[] Clave = Generador.GetBytes(16);

            Generador.Reset();

            Console.Write("Clave generada: ");
            foreach (byte i in Clave)
            {
                Console.Write(i);
            }

            byte[] VI = Generador.GetBytes(16);
            Console.Write("\nVI: ");
            foreach (byte i in VI)
            {
                Console.Write(i);
            }

            Rfc2898DeriveBytes Generador2 = new Rfc2898DeriveBytes(Contra, Sal, 1000);

            byte[] array = Generador2.GetBytes(16);
            Console.Write("\nGenerador2: ");
            foreach (byte i in array)
            {
                Console.Write(i);
            }

        }
    }
}
