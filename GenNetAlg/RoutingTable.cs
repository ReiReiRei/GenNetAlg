using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenNetAlg
{
    class RoutingTable
    {
        public int[,] Table
        {
            get;
            set;
        }
        private int[] weight_set = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 100001 };


        public RoutingTable(int size)
        {
            Table = new int[size, size];
            Random rnd = new Random();

            for (int i = 0; i < Table.GetLength(0); i++)
            {
                for (int j = 0; j < Table.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        Table[i, j] = weight_set[rnd.Next(0, weight_set.Length-1)];
                    }
                    else
                    {
                        Table[i, j] = 0;
                    }
                }
            }

        }

        private RoutingTable()
        { }

        public override string ToString()
        {
            string s = "";
            for(int i=0;i<Table.GetLength(0);i++)
            {  
                for(int j=0;j<Table.GetLength(1);j++)
                {
                    s = s + Table[i, j].ToString() + " ";
                }
                s = s + "\r\n";
            }
            return s;
            

        }



    }
}
