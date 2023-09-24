using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class nodo<T> where T : IComparable<T>
    {
        public nodo<T> izquierda { get; set; }

        public nodo<T> derecha { get; set; }

        public T value { get; set; }

        public int altura { get; set; }
    }
}
