using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nQueensProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Jepni numrin e mbretereshave n: ");
            int n = int.Parse(Console.ReadLine());
            nQueens objTabela=new nQueens(n);
            Console.Write("Deshironi te shiqoni qdo hap? p-Po: ");
            string pergjigja = Console.ReadLine().ToString().ToUpper();
            Console.WriteLine("\nZgjidhjet:\n");
            if (pergjigja == "P") { objTabela.shiqoQdoHap = true; }

            objTabela.Kalkulo();
            Console.ReadKey();
        }
    }
}
