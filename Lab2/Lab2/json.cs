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
                    string comprimidos = COMPRESIONLZ78(compresion);

                    list_compresion.Add("DPI: " + dpi + "\nNombre: " + name + "\nCompañias cifradas:\n" + comprimidos);
                    
                    arbol.Add(person);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex);
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
                    Console.WriteLine("El error es " + ex);
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
                    Console.WriteLine("El error es " + ex);
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
                Console.WriteLine($"Árbol guardado en '{filePath}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el árbol en el archivo JSONL: " + ex.Message);
            }
        }


        public static string personaBuscada(long dpiABuscar)
        {
            nodo<Persona> nodoEncontrado = arbol.GetDPI(dpiABuscar);

            if (nodoEncontrado != null)
            {
                Persona personaEncontrada = nodoEncontrado.value;
                return ($"DPI: {personaEncontrada.DPI}, Nombre: {personaEncontrada.name}, Nacimiento: {personaEncontrada.datebirth}, Direccion: {personaEncontrada.address}");
            }
            else
            {
                return ($"No se encontró: {dpiABuscar}");
            }
        }



        public static string COMPRESIONLZ78(string compresion)
        {
            string text = "";
            string comparar = "";
            int index = 0;
            int valor_a_retornar = 0;
            
            text = compresion; 
            compresion = "<0 , " + text[0] + ">   "; 

            list_diccionario.Add(" ");
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
                        compresion += "<" + index + " , " + comparar[comparar.Length - 1] + ">   "; //<index, letra>

                    else
                        compresion += "<0 , " + comparar + ">   "; //<0,letra>

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
    
    

    }
}
