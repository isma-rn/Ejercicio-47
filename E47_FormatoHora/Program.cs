using System;
using System.Text;
using System.Text.RegularExpressions;

namespace E47_FormatoHora
{
    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            string opcion, formato24;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Ingrese hora en formato 24hrs (Ejemplo: 15:36, 02:25, 23:09)");
                formato24 = Console.ReadLine();
                
                if (ValidarFormato24H(formato24.Replace(" ", "")))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("{0}", ConvertirFormato24_a_12hrs(formato24) );
                }                    
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Error, no se ingresó la hora correctamente");
                }

                do
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("n : nuevo, s : salir");
                    opcion = Console.ReadLine();

                    if (opcion.Equals("s"))
                    {
                        salir = true;
                        break;
                    }
                    else if (!opcion.Equals("n"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No se reconoce opción...");
                    }
                    else
                        break;
                } while (true);
            } while (!salir);
        }
        public static bool ValidarFormato24H(string numero)
        {
            Regex patron = new Regex(@"^(?:[01][0-9]|2[0-3]):[0-5][0-9]$");
            return patron.IsMatch(numero);
        }
        public static string ConvertirFormato24_a_12hrs(string hora)
        {
            bool mayorA12 = false;
            ObtenerHoras(hora, out byte hr);
            ObtenerMinutos(hora, out byte m);            
            if (hr > 12)
            {
                hr -= 12;
                mayorA12 = true;
            }
            return ConstruirFormato12hrs(hr, m, mayorA12);
        }
        private static string ConstruirFormato12hrs(byte hr, byte m, bool mayorA12)
        {
            StringBuilder result = new StringBuilder();            
            result.Append( hr < 10 ? "0" + hr : hr.ToString() );
            result.Append(":");
            result.Append( m < 10 ? "0" + m : m.ToString() );
            result.Append(mayorA12 ? " PM" : " AM");
            return result.ToString();
        }
        private static void ObtenerHoras(string hora, out byte hr)
        {
            Byte.TryParse( hora.Substring(0, hora.IndexOf(":")), out byte hrA );
            hr = hrA;
        }
        private static void ObtenerMinutos(string hora, out byte m)
        {
            Byte.TryParse(hora.Substring(hora.IndexOf(":") + 1, 2), out byte mA);
            m = mA;
        }
    }
}