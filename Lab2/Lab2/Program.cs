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
            bool op1eleccion = false;
            int op1 = 0;
            
            try
            {
                Console.WriteLine("-----------------------------------------------------------------------------\n");
                Console.WriteLine("----------------LABORATORIO 2: DATOS SENSIBLES DE TALENT HUB-----------------\n");
                Console.WriteLine("-----------------------------------------------------------------------------\n");
                Console.WriteLine("Presione el número 1 para cargar la información en el árbol mediante el archivo json predeterminado.");
                
                while (op1 != 1)
                {
                    op1 = Convert.ToInt32(Console.ReadLine());

                    if (op1 == 1)
                    {
                        json.leerArchivo();
                        op1eleccion = true;
                    }
                    else
                    {
                        Console.WriteLine("Por favor ingrese un número válido (1)");
                    }
                }

                if (op1eleccion == true)
                {
                    Console.WriteLine("----------------BIENVENIDO A LOS DATOS SENSIBLES DE TALENT HUB----------------");
                    Console.WriteLine("------------------------------------------------------------------------------");
                    Console.WriteLine("Ingrese el número de la acción a realizar: \n" +
                        "1. Buscar persona mediante su DPI \n" +
                        "2. Ver compresión de compañias de una persona mediante su DPI\n" +
                        "3. Ver descompresión de compañias de una persona mediante su DPI\n" +
                        "4. Salir del programa \n");
                    eleccion = Convert.ToInt32(Console.ReadLine());

                    while (eleccion != 0)
                    {
                        switch (eleccion)
                        {
                            case 1:
                                buscar();
                                Console.WriteLine("\n------------------------------------------------------------------------------");
                                Console.WriteLine("Ingrese el número de la acción a realizar: \n" +
                                    "1. Buscar persona mediante su DPI \n" +
                                    "2. Ver compresión de compañias de una persona mediante su DPI\n" +
                                    "3. Ver descompresión de compañias de una persona mediante su DPI\n" +
                                    "4. Salir del programa \n");
                                eleccion = Convert.ToInt32(Console.ReadLine());
                                break;
                            case 2:
                                ir_a_compresion();
                                Console.WriteLine("\n------------------------------------------------------------------------------");
                                Console.WriteLine("Ingrese el número de la acción a realizar: \n" +
                                    "1. Buscar persona mediante su DPI \n" +
                                    "2. Ver compresión de compañias de una persona mediante su DPI\n" +
                                    "3. Ver descompresión de compañias de una persona mediante su DPI\n" +
                                    "4. Salir del programa \n");
                                eleccion = Convert.ToInt32(Console.ReadLine());
                                break;
                            case 3:
                                pruebaDESC();
                                Console.WriteLine("\n------------------------------------------------------------------------------");
                                Console.WriteLine("Ingrese el número de la acción a realizar: \n" +
                                    "1. Buscar persona mediante su DPI \n" +
                                    "2. Ver compresión de compañias de una persona mediante su DPI\n" +
                                    "3. Ver descompresión de compañias de una persona mediante su DPI\n" +
                                    "4. Salir del programa \n");
                                eleccion = Convert.ToInt32(Console.ReadLine());
                                break;
                            case 4:
                                Console.WriteLine("\nGracias por utilizar nuestro programa de búsquedas y compresión.");
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Opción incorrecta");
                                break;
                        }
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
                string dpi_buscar = Console.ReadLine().Trim();
                dpi = Convert.ToInt64(dpi_buscar);

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

                Console.WriteLine(json.aplicacion_de_compresion(dpi_convertido));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public static void ir_a_descompresion()
        {
            try
            {
                Console.WriteLine("DPI a buscar: ");
                string dpi_buscar = Console.ReadLine().Trim();
                long dpi_convertido = Convert.ToInt64(dpi_buscar);

                Console.WriteLine(json.aplicacion_de_descompresion(dpi_convertido));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        public static void pruebaDESC()
        {
            try
            {
                long dpi = 0;

                Console.WriteLine("DPI a buscar: ");
                string dpi_buscar = Console.ReadLine().Trim();
                dpi = Convert.ToInt64(dpi_buscar);

                Console.WriteLine(json.pruebaDESCOMPRESION(dpi));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public static void pruebas()
        {
            ir_a_descompresion();
        }
    
    }

}
