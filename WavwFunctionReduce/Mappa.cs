using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveFunctionReduce
{
    class Mappa
    {
        int riga;
        int colonna;
       public bool bordiVuoti { get; set; }
        public CellaMappa[][] mappa { get; }
        public Mappa(int riga,int colonna)
        {
            this.riga = riga;
            this.colonna = colonna;
            mappa = new CellaMappa[riga][];
            for (int i = 0; i < riga; i++)
            {
                mappa[i] = new CellaMappa[colonna];
                for (int j = 0; j < colonna; j++)
                    mappa[i][j] = new CellaMappa() { x = i, y = j } ;
            }
        }
        public void AggiornaMappa()
        {
            Random r = new Random();
            StablePriorityQueue<CellaMappa>  lista = new StablePriorityQueue<CellaMappa>(riga*colonna);
            Pattern p = Pattern.GetInstance();
            CellaMappa start;
            if (bordiVuoti)
                start = mappa[r.Next(riga-2)+1][r.Next(colonna-2)+1];
            else
                start = mappa[r.Next(riga)][r.Next(colonna)];
            lista.Enqueue(start, p.NavigaAlbero(start.patternVicini).Length);
            while(lista.Count!=0)
            {
                CellaMappa c = lista.Dequeue();
                Pezzo[] possibili = p.NavigaAlbero(c.patternVicini);
                if (c.ridotto)
                    continue;
                c.tail = possibili[r.Next(possibili.Length)];
                if (c.x > 0)
                {
                    CellaMappa top = mappa[c.x - 1][c.y];
                    top.patternVicini = getPatternVicino(c.x - 1, c.y);
                    lista.Enqueue(top, p.NavigaAlbero(top.patternVicini).Length);
                }
                if (c.y < colonna-1)
                {
                    CellaMappa right = mappa[c.x][c.y + 1];
                    right.patternVicini = getPatternVicino(c.x, c.y + 1);
                    lista.Enqueue(right, p.NavigaAlbero(right.patternVicini).Length);
                }
                if (c.x < riga-1)
                {
                    CellaMappa bottom = mappa[c.x + 1][c.y];
                    bottom.patternVicini = getPatternVicino(c.x + 1, c.y);
                    lista.Enqueue(bottom, p.NavigaAlbero(bottom.patternVicini).Length);
                }
                if (c.y > 0)
                {
                    CellaMappa left = mappa[c.x][c.y - 1];
                    left.patternVicini = getPatternVicino(c.x, c.y - 1);
                    lista.Enqueue(left, p.NavigaAlbero(left.patternVicini).Length);
                }
            }
        }
        public string getPatternVicino(int i,int j)
        {
            //avere i bordi vuoti vuol dire che i bordi devono avere la codifica senza collegamento
            string s = "";
            if (i > 0)
                s += mappa[i - 1][j].ridotto ? mappa[i - 1][j].tail.bordi[2] + "" : "?";//codifica sopra
            else
                s += bordiVuoti ? "0":"?";
            if (j < colonna - 1)
                s += mappa[i][j + 1].ridotto ? mappa[i][j + 1].tail.bordi[3] + "" : "?";//codifica destra
            else
                s += bordiVuoti ? "0" : "?";
            if (i<riga-1)
                s += mappa[i + 1][j].ridotto ? mappa[i + 1][j].tail.bordi[0] + "":"?";//codifica sotto
            else
                s += bordiVuoti ? "0" : "?";
            if (j>0)
                s += mappa[i][j - 1].ridotto ? mappa[i][j - 1].tail.bordi[1] + "":"?";//codifica sinistra
            else
                s += bordiVuoti ? "0" : "?";
            return s;
        }
    }
}
