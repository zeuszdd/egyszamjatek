using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Convert;

namespace egyszamjatek
{
    class Jatekos
    {
        public string Nev { get; private set; }
        public List<int> Tippek { get; private set; }
        public Jatekos(string egysor)
        {
            string[] adatok = egysor.Split(' ');
            Tippek = new List<int>();
            foreach (var item in adatok.Take(adatok.Length-1))
            {
                Tippek.Add(int.Parse(item));
            }
            Nev = adatok[adatok.Length-1];
        }
        /*
        public Jatekos(string[] matrix)
        {
            Nev = matrix[matrix.Length - 1];
            Tippek = new List<int>();
            foreach (var item in matrix.Take(matrix.Length - 1))
            {
                Tippek.Add(int.Parse(item));
            }
        }
        */
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
