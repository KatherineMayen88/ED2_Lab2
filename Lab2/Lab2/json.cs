using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using static System.Collections.Specialized.BitVector32;
using Newtonsoft.Json;


namespace Lab2
{
    public class json
    {
        public static ArbolAVL<Persona> arbol = new ArbolAVL<Persona>();

        public static List<string> list_compresion = new List<string>();
        public static List<string> list_descompresion = new List<string>();
        public static List<string> list_diccionario = new List<string>();


        public static void leerArchivo()
        {
            string filePath = "C:/Users/kathe/source/repos/Lab2/input (1).csv";
            string filePathJsonL = "C:/Users/kathe/source/repos/Lab2/convertidos.txt";
            //"C:\Users\kathe\source\repos\Lab2\convertidos.txt"
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length == 2)
                    {
                        string action = parts[0].Trim();
                        string dataJson = parts[1].Trim();

                        Persona person = Newtonsoft.Json.JsonConvert.DeserializeObject<Persona>(dataJson);
                        commandReader(action, person, arbol);
                    }
                }
                GuardarArbolEnJsonl(arbol, filePathJsonL);
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR AL CARGAR EL ARCHIVO.");
            }
        }

        public static void commandReader(string action, Persona person, ArbolAVL<Persona> arbol)
        {
            if (action == "INSERT")
            {
                try
                {
                    string name = person.name;
                    string dpi = Convert.ToString(person.DPI);
                    string compresion = string.Join(" ", person.companies);
                    string comprimidos = compresionLZ78(compresion);

                    list_compresion.Add("\n----------DATOS DE LA PERSONA----------\n" + "Nombre: " + name + "\nDPI: " + dpi + "\nCompañias cifradas:\n<" + comprimidos);
                    list_descompresion.Add("\n----------DATOS DE LA PERSONA----------\n" + "Nombre: " + name + "\nDPI: " + dpi + "\nCompañias descifradas:\n" + descompresionLZ78(comprimidos));

                    arbol.Add(person);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR EN INSERT: " + ex);
                    throw;
                }


            }
            else if (action == "DELETE")
            {
                try
                {
                    arbol.Remove(person);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR EN DELETE " + ex);
                    throw;
                }

            }
            else if (action == "PATCH")
            {
                try
                {
                    arbol.Update(person, person.DPI);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR EN PATCH " + ex);
                    throw;
                }
            }
        }

        public static void GuardarArbolEnJsonl(ArbolAVL<Persona> arbol, string filePath)
        {
            try
            {
                List<string> jsonLines = new List<string>();

                List<Persona> elementos = arbol.ObtenerListaOrdenada(); // Cambia esto según el nombre de tu método

                foreach (var persona in elementos)
                {
                    string jsonData = JsonConvert.SerializeObject(persona);
                    jsonLines.Add($"{jsonData}");
                }

                File.WriteAllLines(filePath, jsonLines);
                Console.WriteLine("ÁRBOL GUARDADO, ruta: " + filePath + "\n\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR al guardar el árbol en el archivo JSON: " + ex.Message);
            }
        }

        public static string personaBuscada(long dpiABuscar)
        {
            nodo<Persona> nodoEncontrado = arbol.GetDPI(dpiABuscar);

            if (nodoEncontrado != null)
            {
                Persona personaEncontrada = nodoEncontrado.value;
                //string companies = string.Join(" ", personaEncontrada.companies);
                return ($"\n----------DATOS DE LA PERSONA----------\nNombre: {personaEncontrada.name} \nDPI: {personaEncontrada.DPI} \nFecha de nacimiento: {personaEncontrada.datebirth} \nDireccion: {personaEncontrada.address}");
            }
            else
            {
                return ($"No se encontró ninguna persona con el DPI {dpiABuscar}");
            }
        }


        public static string aplicacion_de_compresion(long dpiBuscar)
        {
            string buscarDPI = Convert.ToString(dpiBuscar);
            string encontrado = list_compresion.Find(s => s.Contains(buscarDPI));
            return encontrado;
        }

        public static string compresionLZ78(string compresion)
        {
            string texto = "";
            string comparar_textos = "";
            int index = 0;
            int vretornar = 0;

            texto = compresion;
            compresion = "0 " + texto[0] + ">   \n<";

            list_diccionario.Add(""); //el primer elemento es null
            list_diccionario.Add(texto[0] + ">   <");

            for (int indexText = 1; indexText < texto.Length; indexText++)
            {
                comparar_textos += texto[indexText];

                if (list_diccionario.IndexOf(comparar_textos) != -1)
                {
                    index = list_diccionario.IndexOf(comparar_textos);

                    vretornar = 1; //si se repite la letra, se crea una condicion para que entre en el if la siguiente letra

                    if ((indexText + 1) == texto.Length)
                    {
                        compresion += index + " eof\n"; //end of line
                    }
                }
                else
                {
                    //LETRAS REPETIDAS
                    if (vretornar == 1)
                    {
                        //entra al if y coloca el index de la letra repetida y agrega la letra actual.
                        compresion += index + " " + comparar_textos[comparar_textos.Length - 1] + ">   \n<";
                    }
                    else
                    {
                        //si no se a repetido la letra antes, se coloca 0, letra
                        compresion += "0 " + comparar_textos + ">   \n<";
                    }

                    list_diccionario.Add(comparar_textos); //se agrega la letra al diccionario
                    comparar_textos = ""; //se reinicia el comparador

                    vretornar = 0; //regresa a 0 para no colver a entrar al if de letras repetidas
                }
            }
            return compresion;
        }


        public static string aplicacion_de_descompresion(long dpiBuscar)
        {
            string buscarDPI = Convert.ToString(dpiBuscar);
            string encontrado = list_descompresion.Find(s => s.Contains(buscarDPI));
            return encontrado;
        }

        public static string descompresionLZ78(string comprimido)
        {
            string texto = "";
            string next = "";
            int puntero = 0;

            texto = comprimido;
            string[] arregloComprimido = comprimido.Split();

            comprimido = "";

            for (int i = 0; i< texto.Length; i += 2)
            {
                if (arregloComprimido[i].Length == 0)
                {
                    break;
                }
                
                puntero = int.Parse(arregloComprimido[i]); //obtiene el puntero
                next = arregloComprimido[i + 1]; //obtiene el caracter

                if (next == "")
                {
                    next = "_";
                }

                if (next != "eof")
                {
                    comprimido += list_diccionario[puntero] + next;
                }
                else
                {
                    comprimido += list_diccionario[puntero];
                }

                puntero = 0;
                next = "";
            }

            puntero = 0;
            next = "";
            list_diccionario.Clear();
            return comprimido;
        }


        public static string pruebaDESCOMPRESION(long dpiABuscar)
        {
            nodo<Persona> nodoEncontrado = arbol.GetDPI(dpiABuscar);

            if (nodoEncontrado != null)
            {
                Persona personaEncontrada = nodoEncontrado.value;

                string companies = string.Join("     \n", personaEncontrada.companies);
                return ($"\n----------DATOS DE LA PERSONA----------\nNombre: {personaEncontrada.name} \nDPI: {personaEncontrada.DPI} \nFecha de nacimiento: {personaEncontrada.datebirth} \nDireccion: {personaEncontrada.address} \nCompañias:[\n {companies}");
            }
            else
            {
                return ($"No se encontró ninguna persona con el DPI {dpiABuscar}");
            }
        }




        /*POR SI SE ARRUINA
         
        public static string compresionLZ78(string compresion)
        {
            string texto = "";
            string comparar_textos = "";
            int index = 0;
            int vretornar = 0;

            texto = compresion;
            compresion = "0 " + texto[0] + "\n";

            list_diccionario.Add(""); //el primer elemento es null
            list_diccionario.Add(texto[0] + "");

            for (int indexText = 1; indexText < texto.Length; indexText++)
            {
                comparar_textos += texto[indexText];

                if (list_diccionario.IndexOf(comparar_textos) != -1)
                {
                    index = list_diccionario.IndexOf(comparar_textos);

                    vretornar = 1; //si se repite la letra, se crea una condicion para que entre en el if la siguiente letra

                    if ((indexText+1) == texto.Length)
                    {
                        compresion += index + " eof\n"; //end of line
                    }
                }
                else
                {
                    //LETRAS REPETIDAS
                    if (vretornar == 1)
                    {
                        //entra al if y coloca el index de la letra repetida y agrega la letra actual.
                        compresion += index + " " + comparar_textos[comparar_textos.Length - 1] + "\n";
                    }
                    else
                    {
                        //si no se a repetido la letra antes, se coloca 0, letra
                        compresion += "0 " + comparar_textos + "\n";
                    }

                    list_diccionario.Add(comparar_textos); //se agrega la letra al diccionario
                    comparar_textos = ""; //se reinicia el comparador

                    vretornar = 0; //regresa a 0 para no colver a entrar al if de letras repetidas
                }
            }
            return compresion;
        }
         
         
         
         */

        /*
        public static string COMPRESIONLZ78(string compresion)
        {
            string text = "";
            string comparar = "";
            int index = 0;
            int valor_a_retornar = 0;
            
            text = compresion; 
            compresion = "0 " + text[0] + "\n"; 

            list_diccionario.Add("");
            list_diccionario.Add(text[0] + "");

            for (int itext = 1; itext < text.Length; itext++)
            {
                comparar += text[itext];

                if (list_diccionario.IndexOf(comparar) != -1)
                {
                    index = list_diccionario.IndexOf(comparar);

                    valor_a_retornar = 1; 
                    if (itext + 1 == text.Length) //endof
                        compresion += index + " null\n";
                }
                else //si se repiten las letras
                {
                    if (valor_a_retornar == 1)
                        compresion += index + " " + comparar[comparar.Length - 1] + "\n"; //<index, letra>

                    else
                        compresion += "0 " + comparar + "\n"; //<0,letra>

                    list_diccionario.Add(comparar);
                    comparar = "";

                    valor_a_retornar = 0;
                }
            }
            return compresion;
        }

        public static string aplicarCOMPRESION(long dpi_buscado)
        {
            string buscar = Convert.ToString(dpi_buscado);
            string respuesta = list_compresion.Find(s => s.Contains(buscar));
            return respuesta;
        }
    */


    }
}
