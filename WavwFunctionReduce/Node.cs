using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveFunctionReduce
{
    class Node
    {
        public Pezzo[] possibilità { get; set; }
        private Node[] figli;
        public Node[] Figli { get { return figli; } }
        public Node(int profondita,int nFigli, Pezzo[] possibilita)
        {
            if (profondita <= 3)
            {
                figli = new Node[nFigli + 1];
                figli[nFigli] = new Node(profondita + 1, nFigli, possibilita);
            }
            if(profondita==4)
                this.possibilità = possibilita;
            if (figli != null)
            {
                string pattern = "";
                for (int i = profondita; i < 3; i++)
                    pattern += "?";
                for (int i = 0; i < nFigli; i++)
                    figli[i] = new Node(profondita + 1, nFigli, Pattern.GetInstance().GetPezziConPattern(possibilita, profondita, i + pattern));
            }
        }

        public string ToString()
        {
            string s = "";
            if (figli == null)
            {
                string po = "";
                foreach(Pezzo p in possibilità)
                    po += p.ToString() + ";";
                s += po + " ";
            }
            else
                foreach (Node n in figli)
                    s += n.ToString();

            return s;
        }
    }
}
