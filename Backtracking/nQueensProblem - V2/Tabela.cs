using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nQueensProblem
{
    class nQueens
    {
        readonly int n;//numri i mbretereshave
        char[,] matrica;//Tabela
        int index = -1;//Perdoret si indeksues te pozita e koordinatave te mbretereshes se fundit te vendosur
        //ne listen e kordinatave
        int nrZgjidhjeve = 0;
        List<int[]> listaKordinatave;//Perdoret si collection i kordinatave te mbretereshave te vendosura ne tabele
        int rreshti = 0, shtylla = 0;//Do te perdoren per alokimin e mbretereshave ne qeli,
        //pra operimi dhe navigimi behet permes ketyre dy variablave
        bool perfundoKerkimin = false;
        public bool shiqoQdoHap = false;

        public nQueens(int n)
        {
            this.n = n;
            matrica = new char[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrica[i, j] = '0';
                }
            }
            listaKordinatave = new List<int[]>();
        }

        public string Vizato()
        {
            string strMatrica = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    strMatrica += matrica[i, j];
                }
                strMatrica += "\n";
            }
            return strMatrica;
        }

        public void vendoseMbretereshen(int _rreshti, int _shtylla)
        {
            for (int i = 0; i < n; i++)
            {
                //Vendosim me x qelit ku mund te sulmoj mbreteresha
                matrica[_rreshti, i] = 'x';//Shtylla
                matrica[i, _shtylla] = 'x';//Rreshti
                if ((_rreshti + i) < n && (_shtylla + i) < n)
                    matrica[_rreshti + i, _shtylla + i] = 'x';//Diagonalja djathtas posht
                if ((_rreshti - i) >= 0 && (_shtylla - i) >= 0)
                    matrica[_rreshti - i, _shtylla - i] = 'x';//Diagonalja majtas lart
                if ((_rreshti - i) >= 0 && (_shtylla + i) < n)
                    matrica[_rreshti - i, _shtylla + i] = 'x';//Diagonalja djathtas lart
                if ((_rreshti + i) < n && (_shtylla - i) >= 0)
                    matrica[_rreshti + i, _shtylla - i] = 'x';
               
            }
            matrica[_rreshti, _shtylla] = 'Q';//Pasi te vendosen qelite ku mund te sulmoj
            //mbreteresha, vendoset me Q mbreteresha
            
        }

        public void Pastro(int _rreshti, int _shtylla)
        {//Fshije mbretereshen e fundit te vendosur

            //Ne fillim hiqen pozitat ku mund te sulmoj kjo mbretereshe
            for (int i = 0; i < n; i++)
            {
                matrica[_rreshti, i] = '0';//Shtylla
                matrica[i, _shtylla] = '0';//Rreshti
                if ((_rreshti + i) < n && (_shtylla + i) < n)
                    matrica[_rreshti + i, _shtylla + i] = '0';//Diagonalja djathtas posht
                if ((_rreshti - i) >= 0 && (_shtylla - i) >= 0)
                    matrica[_rreshti - i, _shtylla - i] = '0';//Diagonalja majtas lart
                if ((_rreshti - i) >= 0 && (_shtylla + i) < n)
                    matrica[_rreshti - i, _shtylla + i] = '0';//Diagonalja djathtas lart
                if ((_rreshti + i) < n && (_shtylla - i) >= 0)
                    matrica[_rreshti + i, _shtylla - i] = '0';
                
            }

            matrica[_rreshti, _shtylla] = '0';                
            listaKordinatave.RemoveAt(index);//Fshihen edhe kordinatat

            index--;//Indeksohet kordinata e fundit e mbetur
            if (listaKordinatave.Count > 0)//Nese kemi kordinata ateher duhet ti rivendosim
            {//mbretereshat sepse ka mundesi qe gjat fshirjes se mbretereshes se fundit (vendosjes 0
                //te qelive) te mbishkruhen me 0 edhe vendet ku mund te sulmojne mbretereshat e tjera
                for (int i = 0; i < listaKordinatave.Count; i++)
                {
                    vendoseMbretereshen
                        (listaKordinatave[i][0], listaKordinatave[i][1]);//[i][j]->array brenda array, jo matrice
                }
            }
            
        }

        public void BackTrace()
        {
            rreshti++;//Kalohet te qelia me radhe te kolona aktuale
            if(rreshti>=n || shtylla>=n)//Nese mberrine ne fund te nje rreshti ose kolone
            {//ateher duhet te shiqohet nese eshte fundi ose te hiqet mbreteresha me radhe, pasi qe
                //backtrace ne kete rast ka mberritur ne fund te node aktual dhe duhet te pastrohet kjo node
                //nga pema.

                if (index == -1) { perfundoKerkimin = true; return; }//Ndodh kur kalohen te gjitha rreshtat e kolones 0

                rreshti = listaKordinatave[index][0] + 1;
                shtylla--;
                Pastro(listaKordinatave[index][0], listaKordinatave[index][1]);
                if (rreshti == n) 
                { //Nese edhe te node prind kemi kaluar degen e fundit ateher ne menyre 
                    //rekursive thirrim prap backtrace
                    BackTrace(); 
                }
            }

        }

        public void Kalkulo()
        {//Vendoset mbreteresha e pare
            vendoseMbretereshen(rreshti, shtylla);
            listaKordinatave.Add(new int[] { rreshti, shtylla });//pasi te vendoset mbreteresha
            index++;//duhet te indeksohet kordinata e mbretereshes se re 
            rreshti = 0;//Kalon te shtylla me radhe duke u nisur nga rreshti i pare,
            shtylla++;//pra logjikisht krijohet nje node i ri
            
            while (!perfundoKerkimin)
            {

                if(shtylla==n)
                {//Nese kemi mberritur ne fund ateher e shtypim tabelen dhe thirrim backtrace
                    //per te gjetur zgjidhjet e tjera
                    nrZgjidhjeve++;
                    Console.WriteLine(Vizato());
                    BackTrace();
                    continue;
                }

                if (matrica[rreshti, shtylla] != 'x')
                {//Nese qelia valide ateher vendose mbretereshen
                    vendoseMbretereshen(rreshti, shtylla);
                    listaKordinatave.Add(new int[] { rreshti, shtylla });
                    index++;
                    rreshti = 0;
                    shtylla++;

                }
                else
                {//Perndryshe backtrace
                    BackTrace();
                } 
                if (shiqoQdoHap)
                {
                    MessageBox.Show(Vizato() + "\nPozita aktuale:\nrreshti=" + rreshti + " shtylla=" + shtylla + " index=" + index, "Tabela aktuale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            Console.WriteLine("Numri total i zgjidhjeve: " + nrZgjidhjeve);
            

        }


    }



}
