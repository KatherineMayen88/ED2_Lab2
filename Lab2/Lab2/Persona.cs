using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Persona : IComparable<Persona>
    {

        public string name { get; set; }
        public long DPI { get; set; }
        public string datebirth { get; set; }
        public string address { get; set; }
        public string[] companies { get; set; }
        public int CompareTo(Persona other)
        {
            if (other == null)
            {
                return 1;
            }

            return this.DPI.CompareTo(other.DPI);
        }

    }
}
