using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveFunctionReduce
{
    public class CellaMappa: StablePriorityQueueNode
    {
        public string patternVicini { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public Pezzo tail { get; set; }
        public bool ridotto { get { return tail != null; } }

        public CellaMappa()
        {
            patternVicini = "????";
        }
        public CellaMappa(string pattern)
        {
            patternVicini = pattern;
        }
    }
}
