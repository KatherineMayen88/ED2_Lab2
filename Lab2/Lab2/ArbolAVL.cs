using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class ArbolAVL<T> where T : IComparable<T>
    {
        public nodo<T> root = new nodo<T>();
        public nodo<T> num = new nodo<T>();

        public List<T> list_orden = new List<T>();
        public int n = 0;
        public int rotacion = 0;

        //AÑADIR VALOR NUEVO AL ARBOL
        public void Add(T value)
        {
            Insert(value);
        }

        public int ObtenerAltura(nodo<T> n)
        {
            if (n == null)
            {
                return -1;
            }
            else
            {
                return n.altura;
            }
        }

        //ELIMINAR NODO
        protected void Delete(nodo<T> nodo)
        {
            if (nodo.izquierda.value == null && nodo.derecha.value == null)
            {
                nodo.value = nodo.derecha.value;
            }
            else if (nodo.derecha.value == null)
            {
                nodo.value = nodo.izquierda.value;
                nodo.derecha = nodo.izquierda.derecha;

                nodo.izquierda = nodo.izquierda.izquierda;
            }
            else
            {
                if (nodo.izquierda.value != null)
                {
                    num = LADODERECHO(nodo.izquierda);
                }
                else
                {
                    num = LADODERECHO(nodo);
                }
                nodo.value = num.value;
            }
        }


        public T Remove(T borrado)
        {
            nodo<T> buscado = new nodo<T>();
            buscado = Get(root, borrado);
            if (buscado != null)
            {
                Delete(buscado);
            }
            return borrado;
        }


        private nodo<T> LADODERECHO(nodo<T> nodo)
        {
            if (nodo.derecha.value == null)
            {
                if (nodo.izquierda.value != null)
                {
                    return LADODERECHO(nodo.izquierda);
                }
                else
                {
                    nodo<T> temp = new nodo<T>();
                    temp.value = nodo.value;
                    nodo.value = nodo.derecha.value;
                    return temp;
                }
            }
            else
            {
                return LADODERECHO(nodo.derecha);
            }
        }


        protected nodo<T> Get(nodo<T> nodo, T getvalue)
        {
            if (getvalue.CompareTo(nodo.value) == 0)
            {
                return nodo;
            }
            else if (getvalue.CompareTo(nodo.value) == -1)
            {
                if (nodo.izquierda.value == null)
                {
                    return null;
                }
                else
                {
                    return Get(nodo.izquierda, getvalue);
                }
            }
            else
            {
                if (nodo.derecha.value == null)
                {
                    return null;
                }
                else
                {
                    return Get(nodo.derecha, getvalue);
                }
            }
        }


        public nodo<T> InsertarEnArbol(nodo<T> nodo, nodo<T> temporal)
        {
            try
            {
                nodo<T> nuevoNodo = temporal;

                if (nodo.value.CompareTo(temporal.value) == -1)
                {
                    if (temporal.izquierda.value == null)
                    {
                        temporal.izquierda = nodo;

                    }
                    else
                    {

                        temporal.izquierda = InsertarEnArbol(nodo, temporal.izquierda);

                        if ((ObtenerAltura(temporal.izquierda) - ObtenerAltura(temporal.derecha) == 2))
                        {
                            if (nodo.value.CompareTo(temporal.izquierda.value) == -1)
                            {
                                nuevoNodo = RotarIzquierda(temporal);
                            }
                            else
                            {
                                nuevoNodo = RotarDerechaIzquierda(temporal);
                            }
                        }

                    }
                }
                else if (nodo.value.CompareTo(temporal.value) == 1)
                {
                    if (temporal.derecha.value == null)
                    {
                        temporal.derecha = nodo;
                    }
                    else
                    {

                        temporal.derecha = InsertarEnArbol(nodo, temporal.derecha);
                        if ((ObtenerAltura(temporal.derecha) - ObtenerAltura(temporal.izquierda) == 2))
                        {
                            if (nodo.value.CompareTo(temporal.derecha.value) == 1)
                            {
                                nuevoNodo = RotarDerecha(temporal);
                            }
                            else
                            {
                                nuevoNodo = RotarDerechaDerecha(temporal);
                            }
                        }

                    }
                }

                if ((temporal.izquierda == null) && (temporal.derecha != null))
                {
                    temporal.altura = temporal.derecha.altura + 1;
                }
                else if ((temporal.izquierda != null) && (temporal.derecha == null))
                {
                    temporal.altura = temporal.izquierda.altura + 1;
                }
                else
                {
                    temporal.altura = Math.Max(ObtenerAltura(temporal.izquierda), ObtenerAltura(nodo.derecha)) + 1;
                }
                return nuevoNodo;
            }
            catch
            {
                throw;
            }
        }


        public nodo<T> CrearNodoAVL(T valuecreado)
        {
            nodo<T> nodo = new nodo<T>();
            nodo.value = valuecreado;
            nodo.altura = 0;
            nodo.izquierda = new nodo<T>();
            nodo.derecha = new nodo<T>();
            return nodo;
        }

        public void Insert(T valueinsertado)
        {
            try
            {
                nodo<T> nuevo = CrearNodoAVL(valueinsertado);

                if (root.value == null)
                {
                    root = nuevo;
                }
                else
                {
                    root = InsertarEnArbol(nuevo, root);
                }
            }
            catch
            {
                throw;
            }
        }


        protected nodo<T> Insert(nodo<T> nodo, T valueinsertado)
        {
            try
            {
                nodo<T> nuevo = CrearNodoAVL(valueinsertado);

                if (nodo == null)
                {
                    nodo = nuevo;
                }
                else
                {
                    nodo = InsertarEnArbol(nuevo, nodo);
                }
                return nodo;
            }
            catch
            {
                throw;
            }
        }


        public nodo<T> RotarIzquierda(nodo<T> nodo)
        {
            rotacion++;
            nodo<T> temp = nodo.izquierda;

            nodo.izquierda = temp.derecha;
            temp.derecha = nodo;
            nodo.altura = Math.Max(ObtenerAltura(nodo.izquierda), ObtenerAltura(nodo.derecha)) + 1;
            temp.altura = Math.Max(ObtenerAltura(temp.izquierda), ObtenerAltura(temp.derecha)) + 1;
            return temp;
        }

        public nodo<T> RotarDerecha(nodo<T> nodo)//rotacion derecha
        {
            rotacion++;
            nodo<T> temp = nodo.derecha;
            nodo.derecha = temp.izquierda;
            temp.izquierda = nodo;
            nodo.altura = Math.Max(ObtenerAltura(nodo.izquierda), ObtenerAltura(nodo.derecha)) + 1;
            temp.altura = Math.Max(ObtenerAltura(temp.izquierda), ObtenerAltura(temp.derecha)) + 1;
            return temp;
        }

        public nodo<T> RotarDerechaIzquierda(nodo<T> nodo)
        {
            rotacion++;
            nodo<T> temp = new nodo<T>();
            nodo.izquierda = RotarDerecha(nodo.izquierda);
            temp = RotarIzquierda(nodo);
            return temp;
        }

        public nodo<T> RotarDerechaDerecha(nodo<T> nodo)
        {
            rotacion++;
            nodo<T> temp = new nodo<T>();
            nodo.derecha = RotarIzquierda(nodo.derecha);
            temp = RotarDerecha(nodo);
            return temp;
        }

        private void InOrder(nodo<T> nodo)
        {
            if (nodo.value != null)
            {
                InOrder(nodo.izquierda);
                list_orden.Add(nodo.value);
                InOrder(nodo.derecha);
            }
        }


        public List<T> ObtenerListaOrdenada()
        {
            list_orden.Clear();
            InOrder(root);
            return list_orden;
        }



        public List<T> ObtenerDatosLista(Func<T, bool> Predicate)
        {
            List<T> prov = new List<T>();
            n = 0;
            ObtenerListaOrdenada();
            for (int i = 0; i < list_orden.Count(); i++)
            {
                if (Predicate(list_orden[i]))
                {
                    n = i;
                    prov.Add(list_orden[i]);
                }
            }
            return prov;
        }


        public List<T> ObtenerDatos2(Func<T, bool> Predicate, Func<T, bool> Predicate2)
        {
            List<T> prov = new List<T>();
            nodo<T> cmp = new nodo<T>();
            n = 0;
            ObtenerListaOrdenada();
            for (int i = 0; i < list_orden.Count(); i++)
            {
                if (Predicate2(list_orden[i]))
                {
                    if (Predicate(list_orden[i]))
                    {
                        n = i;
                        prov.Add(list_orden[i]);
                    }
                }
            }
            return prov;
        }


        private int ObtenerAlturaNodo(nodo<T> nodo)
        {
            if (nodo == null)
            {
                return -1;
            }
            else
            {
                int izquierda = ObtenerAlturaNodo(nodo.izquierda);
                int derecha = ObtenerAlturaNodo(nodo.derecha);
                return Math.Max(izquierda, derecha) + 1;
            }
        }

        public nodo<T> GetDPI(long dpi)
        {
            return GetDPI(root, dpi);
        }

        private nodo<T> GetDPI(nodo<T> nodo, long dpi)
        {
            if (nodo == null)
            {
                return null;
            }
            long nodoDPI = ((dynamic)nodo.value).DPI;

            if (dpi == nodoDPI)
            {
                return nodo;
            }
            else if (dpi < nodoDPI)
            {
                return GetDPI(nodo.izquierda, dpi);
            }
            else
            {
                return GetDPI(nodo.derecha, dpi);
            }
        }


        public bool Update(T persona, long dpi)
        {
            nodo<T> nodo = Get(root, persona);
            if (nodo != null)
            {
                Remove(persona);

                Insert(persona);

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
