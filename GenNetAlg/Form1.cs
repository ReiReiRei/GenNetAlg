using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenNetAlg
{
    public partial class Form1 : Form
    {

        int networkSize = 10;
        int populationSize = 30;
        int firstPoint = 0;
        int lastPoint = 3;
        int numberOfIterations = 50000;
        RoutingTable Tb;
        Population Po;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            


          

            for (int i = 0; i < numberOfIterations; i++)
            {
                int cur_size = Po.size * 2;
                for (int j = 0; j < cur_size; j++)
                {
                    Po.Chromosomes.Add(Chromosome.Cross(Po.Random, Po.Random).Mutate());
                }

                

                //foreach (var cr in Po.Chromosomes)
                //{
                //    if (cr.isNew == false)
                //    {
                //        Po.Chromosomes.Remove(cr);
                //    }

                //}

                foreach (var cr in Po.Chromosomes)
                {
                cr.isNew = false;
                }





                Po.Chromosomes.Sort(Chromosome.Compr);
                if (populationSize > Po.size)
                {
                    Po.Chromosomes = Po.Chromosomes.GetRange(0, Po.size);
                }
                else
                {
                    Po.Chromosomes = Po.Chromosomes.GetRange(0, populationSize - 1);
                }
                Po.Terminate();
                Populate.Text = Po.ToString();

             




            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           Tb = new RoutingTable(networkSize);
           Po = new Population(populationSize, networkSize, firstPoint, lastPoint, Tb);

           Populate.Text = Po.ToString();
           Route.Text = Tb.ToString();
        }
    }
}
