using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightPuzzle_BFS
{
    class Program
    {
        static Node StartingNode = new Node(new int[,]
        {
            {1,2,3 },
            {4,8,6 },
            {0,7,5 }
        });
        static int[,] TargetState = new int[,]
        {
            {1,2,3 },
            {8,0,4 },
            {7,6,5 }
        };
        static void Main(string[] args)
        {
            Queue<Node> OpenList = new Queue<Node>();
            List<Node> ClosedList = new List<Node>();

            OpenList.Enqueue(StartingNode);

            Node tempNode = null;
            while(OpenList.Count>0)//perderisa ka nyje qe nuk jane vizitu
            {
                tempNode = OpenList.Dequeue();
                ClosedList.Add(tempNode);

                //kontrollo a kemi arrit tek target state
                if(CheckTarget(tempNode))
                {
                    Console.WriteLine("Zgjidhja u gjet ne nivelin " + tempNode.Level);
                    break;
                }

                //gjenerimi i children
                tempNode.Children = GenerateChildren(tempNode);

                for(int i=0;i<tempNode.Children.Count;i++)
                {
                    Node candidateNode = tempNode.Children[i];
                    if (Compare(candidateNode, OpenList.ToList()) == false && Compare(candidateNode, ClosedList) == false)
                        OpenList.Enqueue(candidateNode);
                }
            }

            //printojme zgjidhjen
            Print(tempNode);

            Console.ReadKey();
        }

        static bool CheckTarget(Node tempNode)
        {
            for (int i = 0; i < tempNode.Representation.GetLength(0); i++)
                for (int j = 0; j < tempNode.Representation.GetLength(0); j++)
                    if (tempNode.Representation[i, j] != TargetState[i, j])
                        return false;

            return true;
        }

        static List<Node> GenerateChildren(Node tempNode)
        {
            List<Node> Children = new List<Node>();
            int x = 0, y = 0;
            for(int i=0;i<tempNode.Representation.GetLength(0);i++)
                for(int j=0;j<tempNode.Representation.GetLength(0);j++)
                    if(tempNode.Representation[i,j] == 0)
                    {
                        x = i;
                        y = j;
                        break;
                    }
            //levizja majtas
            if(y>0)
            {
                Node childNode = new Node(tempNode.Representation);
                childNode.Level = tempNode.Level + 1;
                childNode.Parent = tempNode;
                childNode.Representation[x, y] = childNode.Representation[x, y - 1];
                childNode.Representation[x, y - 1] = 0;
                Children.Add(childNode);
            }

            //levizja djathtas
            if (y < 2)
            {
                Node childNode = new Node(tempNode.Representation);
                childNode.Level = tempNode.Level + 1;
                childNode.Parent = tempNode;
                childNode.Representation[x, y] = childNode.Representation[x, y + 1];
                childNode.Representation[x, y + 1] = 0;
                Children.Add(childNode);
            }

            //levizja lart
            if (x > 0)
            {
                Node childNode = new Node(tempNode.Representation);
                childNode.Level = tempNode.Level + 1;
                childNode.Parent = tempNode;
                childNode.Representation[x, y] = childNode.Representation[x - 1, y];
                childNode.Representation[x - 1, y] = 0;
                Children.Add(childNode);
            }

            //levizja poshte
            if (x < 2)
            {
                Node childNode = new Node(tempNode.Representation);
                childNode.Level = tempNode.Level + 1;
                childNode.Parent = tempNode;
                childNode.Representation[x, y] = childNode.Representation[x + 1, y];
                childNode.Representation[x + 1, y] = 0;
                Children.Add(childNode);
            }

            return Children;
        }

        static bool Compare(Node candidateNode, List<Node> currentList)
        {
            bool NodeExist = false;
            for(int k=0;k<currentList.Count;k++)
            {
                NodeExist = true;
                for (int i = 0; i < candidateNode.Representation.GetLength(0); i++)
                    for (int j = 0; j < candidateNode.Representation.GetLength(0); j++)
                        if (candidateNode.Representation[i, j] != currentList[k].Representation[i, j])
                            NodeExist = false;

                if (NodeExist)
                    return true;
            }
            //currentList = [N1,N2,N3]
            //candidateNode = N4
            return false;
        }

        static void Print(Node tempNode)
        {
            if (tempNode.Parent != null)
                Print(tempNode.Parent);

            for(int i=0;i<tempNode.Representation.GetLength(0);i++)
            {
                for (int j = 0; j < tempNode.Representation.GetLength(0); j++)
                    Console.Write(tempNode.Representation[i, j] + "  ");

                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
