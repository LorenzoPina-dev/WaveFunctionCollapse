using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveFunctionReduce
{
    public class Pezzo
    {
        public int id { get; set; }
        public int[] bordi { get; set; }
        public Image immagine { get; set; }
        public Pezzo(int[] bordi, string uri, RotateFlipType rotation)
        {
            immagine = Image.FromFile( uri);
            immagine.RotateFlip(rotation);
            this.bordi = bordi;
        }
        public Pezzo(int[] bordi, string uri)
        {
            immagine = Image.FromFile(uri);
            this.bordi = bordi;
        }
        public bool match(Pezzo p, int direzione)
        {
            return bordi[direzione] == p.bordi[(direzione + 2) % 4];
        }
        public string ToString()
        {
            string s = "";
            for (int i = 0; i < bordi.Length; i++)
                s += bordi[i]+";";
            return s;
        }
    }
}
