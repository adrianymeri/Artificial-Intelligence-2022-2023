using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightPuzzleDFS
{
    class Node
    {
        public int[,] Representation = new int[3, 3];
        public Node Parent;
        public int Level;
        public List<Node> Children;

        public Node(int[,] _representation)
        {
            for (int i = 0; i < _representation.GetLength(0); i++)
                for (int j = 0; j < _representation.GetLength(0); j++)
                    Representation[i, j] = _representation[i, j];

            Children = new List<Node>();
            Level = 0;
        }
    }
}
