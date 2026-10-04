using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Para trabajar con ficheros
using System.IO;
using System.Runtime.InteropServices;


namespace Apoyo
{
    class Ayuda
    {
        public void WriteHex(byte[] Bufer, int NumBytes)
        {
            int NumCols = 16;

            // Console.WriteLine("NumBytes: " + NumBytes); // DEBUG
            int NumFils = (int)Math.Ceiling((double)NumBytes / (double)NumCols);
            // Console.WriteLine("NumFils: " + NumFils); // DEBUG

            int i = 0;

            for (int Fil = 0; Fil < NumFils; Fil++)
            {
                for (int Col = 0; Col < NumCols; Col++)
                {
                    Console.Write("{0,2:X2} ", Bufer[i]);
                    i++;
                    if (i >= NumBytes) break;
                }
                Console.WriteLine();
            }
        } // WriteHex()

        public long BytesFichero(string NombreFich)
        {
            FileInfo fichero = new FileInfo(NombreFich);
            return fichero.Length;

        } // BytesFichero()


        public void GuardaBufer(string NombreFich, byte[] Bufer)
        {
            FileInfo fichero = new FileInfo(NombreFich);

            using (FileStream escritor = fichero.OpenWrite())
            {
                escritor.Write(Bufer, 0, Bufer.Length);
            }
           
        } // GuardaBufer()
        

        public void CargaBufer(string NombreFich, byte[] Bufer)
        {
            FileInfo fichero = new FileInfo(NombreFich);

            using (FileStream lector = fichero.OpenRead())

            {
                lector.Read(Bufer, 0, Bufer.Length);
            }
        } // CargaBufer()

    } // class Ayuda
} // namespace Apoyo
