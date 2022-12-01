using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthFirstSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(10);
            graph.AddConnection(0, 1);
            graph.AddConnection(0, 2);
            graph.AddConnection(1, 3);
            graph.AddConnection(2, 4);
            graph.AddConnection(2, 5);
            graph.AddConnection(3, 6);
            graph.AddConnection(4, 7);
            graph.AddConnection(7, 8);
            graph.AddConnection(7, 9);

            Stack<int> OpenList = new Stack<int>();
            List<int> ClosedList = new List<int>();

            OpenList.Push(0);

            while(OpenList.Count>0)
            {
                int tempNodeValue = OpenList.Pop();
                Console.WriteLine("Node: " + tempNodeValue);

                ClosedList.Add(tempNodeValue);

                if(graph.Connections[tempNodeValue].Count>0)
                {
                    for(int i = graph.Connections[tempNodeValue].Count-1;i>=0;i--)
                    {
                        int tempCandidateNode = graph.Connections[tempNodeValue][i];
                        if(OpenList.Contains(tempCandidateNode)==false && 
                            ClosedList.Contains(tempCandidateNode)==false)
                        {
                            OpenList.Push(tempCandidateNode);
                        }
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
