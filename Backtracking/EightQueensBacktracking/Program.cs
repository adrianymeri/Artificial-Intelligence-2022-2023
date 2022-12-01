using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueensBacktracking
{
    class Program
    {
        static int N = 8;
        static int[,] shahu = new int[N, N];
        static void Main(string[] args)
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    shahu[i, j] = 0;

            if(VendosMbretereshen(0))
            {
                for(int i=0;i<N;i++)
                {
                    for (int j = 0; j < N; j++)
                        Console.Write(shahu[i, j] + " ");

                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }

        static bool VendosMbretereshen(int q)
        {
            if (q == N)
                return true;
            for (int i = 0; i < N; i++)
            {
                //nese vendoset ne nje rresht, atehere q avancon per 1
                if(KontrolloVendosjen(i,q)==true)
                {
                    shahu[i, q] = 1;
                    if (VendosMbretereshen(q + 1))
                        return true;

                    shahu[i, q] = 0;
                }                
            }

            return false;
        }

        static bool KontrolloVendosjen(int i, int q)
        {
            //kontrollo ne rresht
            for (int k = 0; k < q; k++)
                if (shahu[i, k] == 1)
                    return false;

            //kontrollo diagonalen lart
            for(int rr=i-1,k=q-1;rr>=0 && k>=0;rr--,k--)
            {
                if (shahu[rr, k] == 1)
                    return false;
            }
            //int rr = i - 1;
            //int k = q - 1;
            //while(rr>=0 && k>=0)
            //{

            //    rr--;
            //    k--;
            //}

            //kontrollo diagonalen poshte
            for (int rr = i + 1, k = q - 1; rr < N && k >= 0; rr++, k--)
            {
                if (shahu[rr, k] == 1)
                    return false;
            }

            return true;
        }
    }

    
}
