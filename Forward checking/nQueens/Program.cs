using System;

namespace EightQueensForwardChecking
{
  public class Program
  {
    static int N = 8;
    static int[,] shahu = new int[N, N];
    public static void Main(string[] args)
    {
      for (int i = 0; i < N; i++)
        for (int j = 0; j < N; j++)
          shahu[i, j] = 0;

      if(VendosMbretereshen(0))
      {
        for(int i=0;i<N;i++)
        {
          for (int j = 0; j < N; j++)
            Console.Write((shahu[i, j] <= 0 ? 0 : 1) + " ");

          Console.WriteLine();
        }
      }
    }

    static bool VendosMbretereshen(int q)
    {
      if (q == N)
        return true;

      for (int i = 0; i < N; i++)
      {
        var oldValue = (int[,])shahu.Clone();
        
        if(!KontrolloVendosjen(i,q))
          continue;

        shahu[i, q] = 1;

        //nese vendoset ne nje rresht, atehere q avancon per 1
        if (VendosMbretereshen(q + 1))
          return true;

        shahu = oldValue;
      }

      return false;
    }

    static bool KontrolloVendosjen(int i, int q)
    {
      if (shahu[i, q] < 0)
        return false;
      
      // Largo -1 nga rreshti
      for (int k = q + 1; k < N; k++)
        shahu[i, k] -= 1;
        
      // Largo -1 nga diagonalja larte
      for(int rr = i - 1, k = q + 1; rr >= 0 && k < N; rr--, k++)
        shahu[rr, k] -= 1;

      // Largo -1 nga diagonalja poshte
      for (int rr = i + 1, k = q + 1; rr < N && k < N; rr++, k++)
        shahu[rr, k] -= 1;

      return true;
    }
  }
}