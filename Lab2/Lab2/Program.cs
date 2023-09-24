using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            json json = new json();
            int eleccion;
            try
            {
                Console.WriteLine("BIENVENIDO A BÚSQUEDAS COMPLEJAS DE TALENT HUB \n");
                Console.WriteLine("Ingrese el número de la acción a realizar: \n" +
                    "1. Cargar información en el árbol \n" +
                    "2. Buscar persona mediante su DPI \n" +
                    "3. Ver cifrado de compañias \n" +
                    "4. Salir del programa \n");
                eleccion = Convert.ToInt32(Console.ReadLine());

                while (eleccion != 0)
                {
                    switch (eleccion)
                    {
                        case 1:
                            json.leerArchivo();
                            Console.WriteLine("\nIngrese el número de la acción a realizar: \n" +
                                "1. Cargar información en el árbol \n" +
                                "2. Buscar persona mediante su DPI \n" +
                                "3. Ver cifrado de compañias \n" +
                                "4. Salir del programa \n");
                            eleccion = Convert.ToInt32(Console.ReadLine());
                            break;

                        case 2:
                            buscar();
                            Console.WriteLine("\nIngrese el número de la acción a realizar: \n" +
                                "1. Cargar información en el árbol \n" +
                                "2. Buscar persona mediante su DPI \n" +
                                "3. Ver cifrado de compañias \n" +
                                "4. Salir del programa \n");
                            eleccion = Convert.ToInt32(Console.ReadLine());
                            break;
                        case 3:
                            ir_a_compresion();
                            Console.WriteLine("\nIngrese el número de la acción a realizar: \n" +
                                "1. Cargar información en el árbol \n" +
                                "2. Buscar persona mediante su DPI \n" +
                                "3. Ver cifrado de compañias \n" +
                                "4. Salir del programa \n");
                            eleccion = Convert.ToInt32(Console.ReadLine());
                            break;
                        case 4:
                            Console.WriteLine("\nGracias por utilizar nuestro programa de busquedas.");
                            break;
                        default:
                            Console.WriteLine("Opción incorrecta");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            Console.ReadKey();
        }

        public static void buscar()
        {
            try
            {
                long dpi = 0;
                Console.WriteLine("Ingrese el número de DPI de la persona que desea buscar:");
                dpi = Convert.ToInt64(Console.ReadLine());

                Console.WriteLine(json.personaBuscada(dpi));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public static void ir_a_compresion()
        {
            try
            {
                Console.WriteLine("DPI a buscar: ");
                string dpi_buscar = Console.ReadLine().Trim();
                long dpi_convertido = Convert.ToInt64(dpi_buscar);

                Console.WriteLine(json.aplicarCOMPRESION(dpi_convertido));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }

}
