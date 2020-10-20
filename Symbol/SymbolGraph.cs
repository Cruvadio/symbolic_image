using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Symbol
{
    public class SymbolGraph
    {

        private ConcurrentDictionary<int, List<int>> graph;
        private int numberOfVerticies;


        public ConcurrentDictionary<int, List<int>> keyValuePairs
        {
            get
            {
                return graph;
            }
        }
        public int Count
        {
            get
            {
                return graph.Count;
            }
        }

        public int Capacity
        {
            get
            {
                return numberOfVerticies;
            }

            set
            {
                numberOfVerticies = value;
            }
        }

        public SymbolGraph(int number)
        {
            numberOfVerticies = number;
            graph = new ConcurrentDictionary<int, List<int>>();
        }

        public SymbolGraph()
        {
            graph = new ConcurrentDictionary<int, List<int>>();
        }
        public List<int> this[int i]
        {
            get
            {
                if (i >= numberOfVerticies || i < 0) throw new ArgumentOutOfRangeException();
                if (graph.ContainsKey(i)) return graph[i];
                return null;
            }

            set
            {
                if (i >= numberOfVerticies || i < 0) throw new ArgumentOutOfRangeException();
                graph[i] = value;
            }
        }

        public bool Contains (int i)
        {
            return graph.ContainsKey(i);
        }
    }

}
