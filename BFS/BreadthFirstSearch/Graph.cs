using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadthFirstSearch
{
    class Graph
    {
        public int NoNodes { get; set; }
        public List<int>[] Connections;

        public Graph(int _noNodes)
        {
            NoNodes = _noNodes;
            Connections = new List<int>[NoNodes];

            for (int i = 0; i < NoNodes; i++)
                Connections[i] = new List<int>();
        }

        public void AddConnection(int sourceNode, int connectedNode)
        {
            Connections[sourceNode].Add(connectedNode);
        }
    }
}
