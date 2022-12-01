using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadthFirstSearch
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

            Queue<int> openList = new Queue<int>();
            List<int> closedList = new List<int>();

            openList.Enqueue(0);

            while (openList.Count > 0)
            {
                int tempNode = openList.Dequeue();
                Console.WriteLine("Nyja: " + tempNode);
                closedList.Add(tempNode);

                if(graph.Connections[tempNode].Count>0)
                {
                    for (int i = 0; i < graph.Connections[tempNode].Count; i++)
                    {
                        if (openList.Contains(graph.Connections[tempNode][i]) == false
                            && closedList.Contains(graph.Connections[tempNode][i]) == false)
                            openList.Enqueue(graph.Connections[tempNode][i]);
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
