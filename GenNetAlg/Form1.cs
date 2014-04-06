using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GenNetAlg
{



    public partial class Form1 : Form
    {

        int networkSize = 10;
        int initialPopulationSize = 30;
        int firstPoint = 0;
        int lastPoint = 3;
        int numberOfIterations = 500;
        int currentIteration = 0;
        RoutingTable Tb;
        Population Po;

        public Form1()
        {
         
            InitializeComponent();
        }


        private void Step()
        {

               int cur_size = Po.size * 4;
                for (int j = 0; j < cur_size; j++)
                {
                    Po.Chromosomes.Add(Chromosome.Cross(Po.Random, Po.Random).Mutate());
                }


                foreach (var cr in Po.Chromosomes)
                {
                cr.isNew = false;
                }





                Po.Chromosomes.Sort(Chromosome.Compr);
                if (initialPopulationSize > Po.size)
                {
                    Po.Chromosomes = Po.Chromosomes.GetRange(0, Po.size);
                }
                else
                {
                    Po.Chromosomes = Po.Chromosomes.GetRange(0, initialPopulationSize - 1);
                }
                Po.Terminate();
                Populate.Text = Po.ToString();

           


        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            for (int i = 0; i < numberOfIterations; i++)
            {
                Step();
               
                chart1.Series["Max"].Points.AddXY(currentIteration, Po.Chromosomes[0].Weight);
                currentIteration++;
            }
        }

        public void refresh()
        {
            Tb = new RoutingTable(networkSize);
            Po = new Population(initialPopulationSize, networkSize, firstPoint, lastPoint, Tb);
            currentIteration = 0;



            dataGridView1.RowCount = Tb.Table.GetLength(0);
            dataGridView1.ColumnCount = Tb.Table.GetLength(1);
            for (int i = 0; i < Tb.Table.GetLength(0); i++)
            {
                for (int j = 0; j < Tb.Table.GetLength(1); j++)
                {
                    //пишем значения из массива в ячейки контролла
                    dataGridView1.Rows[i].Cells[j].Value = Tb.Table[i, j];

                }
            }



            Populate.Text = Po.ToString();
          
                chart1.Series["Max"].Points.Clear();
        

        }
        

        private void button2_Click(object sender, EventArgs e)
        {

            refresh();
           
        }

        private void RouteGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentIteration++;
            chart1.Series["Max"].Points.AddXY(currentIteration, Po.Chromosomes[0].Weight);
            Step();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text,out initialPopulationSize);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox2.Text, out numberOfIterations);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           int.TryParse(textBox3.Text, out networkSize);
           refresh();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox4.Text,out firstPoint);
            refresh();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
           
            int.TryParse(textBox5.Text, out lastPoint);
            refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Series maxSer = new Series("Max");
            chart1.Series.Add(maxSer);
            chart1.Series["Max"].ChartType = SeriesChartType.FastLine;
            chart1.ChartAreas[0].AxisX = new Axis { LabelStyle = new LabelStyle() { Font = new Font("Arial", 8) } };
            chart1.ChartAreas[0].AxisY = new Axis { LabelStyle = new LabelStyle() { Font = new Font("Arial", 8) } };
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].BackColor = Color.PaleTurquoise;



            textBox1.Text = initialPopulationSize.ToString();
            textBox2.Text = numberOfIterations.ToString();
            textBox3.Text = networkSize.ToString();
            textBox4.Text = firstPoint.ToString();
            textBox5.Text = lastPoint.ToString();

           
           







            

           

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Tb.Table[e.RowIndex, e.ColumnIndex] = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        }

       
       
    }
}
