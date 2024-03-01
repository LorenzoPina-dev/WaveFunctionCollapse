using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WaveFunctionReduce
{
    class Pattern
    {
        public Pezzo[] pezzi { get; }
        private Node _alberoPossibilità;

        public Node AlberoPossibilità { get { return _alberoPossibilità; } }

        private static Pattern instance=null;
        
        public static Pattern GetInstance()
        {
            if (instance == null)
            {
                instance = new Pattern();
            }
            return instance;
        }
        private Pattern()
        {
            string directory = ".\\img";
            string[] imgFilePath = Directory.GetFiles(directory);
            pezzi = new Pezzo[imgFilePath.Length];
            for (int i = 0; i < imgFilePath.Length; i++)
                pezzi[i] = new Pezzo(getCodificaFromFilePath(imgFilePath[i]), imgFilePath[i]) { id = i };
        }

        private int[] getCodificaFromFilePath(string filePath)
        {
            string fileName = filePath.Substring(filePath.LastIndexOf('\\')+1, 4);
            int[] codifica = new int[fileName.Length];
            for (int i = 0; i < fileName.Length; i++)
                codifica[i] = fileName[i] - '0';
            return codifica;
        }
        //metodo usato nella generazione dell'albero per ottenere tutte le codifiche aventi un determinato pattern
        public Pezzo[] GetPezziConPattern(Pezzo[] possibilita,int pos,string pattern)
        {
            if (pos == 4)
                return possibilita;
            if (pattern[0] == '?')
                return GetPezziConPattern(possibilita,pos + 1, pattern.Substring(1));
            int valorePattern = -1;
            if (!int.TryParse(pattern[0] + "",out valorePattern))
                throw new Exception("pattern non valido");
            List<Pezzo> ris = new List<Pezzo>();
            foreach (Pezzo p in GetPezziConPattern(possibilita,pos + 1, pattern.Substring(1)))
                if (p.bordi[pos] == valorePattern)
                    ris.Add(p);
            return ris.ToArray();
        }
        public void GeneraAlberoCombinazioni(int nFigli)
        {
            //genera l'albero ricorsivamente il costruttore
            _alberoPossibilità = new Node(0, nFigli, pezzi);
        }

        public Pezzo[] NavigaAlbero(string pattern)
        {
            Node n = AlberoPossibilità;
            for(int i=0;i<pattern.Length;i++)
            {
                int figlio = 0;
                if (pattern[i] == '?')//se non selezionato il lato è sempre l'ultimo figlio
                    figlio = n.Figli.Length-1;
                else
                    figlio = pattern[i]-'0';//se no accesso diretto al figlio giusto
                n = n.Figli[figlio];
            }
            return n.possibilità;
        }
    }
}
