using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Symbol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            try
            {
                using (StreamReader sr = new StreamReader("cache"))
                {
                    txtFormXFormula.Text = ReadLineFromCache(sr);
                    txtFormYFormula.Text = ReadLineFromCache(sr);

                    txtFormXMax.Text = ReadLineFromCache(sr);
                    txtFormXMin.Text = ReadLineFromCache(sr);
                    txtFormYMax.Text = ReadLineFromCache(sr);
                    txtFormYMin.Text = ReadLineFromCache(sr);

                    txtFormAValue.Text = ReadLineFromCache(sr);
                    txtFormBValue.Text = ReadLineFromCache(sr);

                    txtFormDeltaValue.Text = ReadLineFromCache(sr);
                    textBoxCast.Text = ReadLineFromCache(sr);
                }
            }
            catch (Exception e)
            {
                using (StreamWriter sw = new StreamWriter("cache", false))
                {

                }
            }
        }

        private string ReadLineFromCache (StreamReader sr)
        {
            string line = sr.ReadLine();
            if (line == null) return "";
            return line;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void submit_Click(object sender, EventArgs e)
        {
            FormulaeParser parser = new FormulaeParser();

            var xFunc = parser.CreateFormula(txtFormXFormula.Text);
            var yFunc = parser.CreateFormula(txtFormYFormula.Text);
            var xMax = Double.Parse(txtFormXMax.Text);
            var xMin = Double.Parse(txtFormXMin.Text);
            var yMax = Double.Parse(txtFormYMax.Text);
            var yMin = Double.Parse(txtFormYMin.Text);

            var a = Double.Parse(txtFormAValue.Text);
            var b = Double.Parse(txtFormBValue.Text);

            var delta = Double.Parse(txtFormDeltaValue.Text);

            var cast = Int32.Parse(textBoxCast.Text);

            Symbol symbol = new Symbol(xMin, xMax, yMin, yMax, cast, delta, a, b);
            symbol.xFunction = xFunc;
            symbol.yFunction = yFunc;

            DateTime time = DateTime.Now;

            symbol.MakeGraph();

            var endTime = DateTime.Now - time;


            lblTime.Text = "Program worked " + endTime.TotalMilliseconds.ToString() + " miliseconds.";


            WriteToCache();

        }


        private void WriteToCache()
        {
            using (StreamWriter sw = new StreamWriter("cache", false))
            {
                WriteCache(sw, txtFormXFormula.Text);
                WriteCache(sw, txtFormYFormula.Text);
                WriteCache(sw, txtFormXMax.Text);
                WriteCache(sw, txtFormXMin.Text);
                WriteCache(sw, txtFormYMax.Text);
                WriteCache(sw, txtFormYMin.Text);
                WriteCache(sw, txtFormAValue.Text);
                WriteCache(sw, txtFormBValue.Text);
                WriteCache(sw, txtFormDeltaValue.Text);
                WriteCache(sw, textBoxCast.Text);
            }
        }

        private void WriteCache (StreamWriter sw, string str)
        {
            if (str != "")
                sw.WriteLine(str);
            else
                sw.WriteLine("");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string info = @"  Программа предназначена для построения символического образа. Для начала работы необходимо ввести данные:
    Отображение f(x,y) - формула для отображения x и y. Также необходимо ввести область, в которой нужно получить символический образ.
    Затем параметры a и b, если необходимо, и интервал, который определит точность и количество клеток, а также количество точек, выпускаемых из каждой клетки.
        Ввод формул:
      +  - сложение
      -  - вычитание
      *  - умножение
      /  - деление
      ^  - возведение в степень
     sin - синус
     cos - косинус
     exp - экспонента
     log - логарифм

    Доступные переменные: x, y, a, b
        ";
            const string caption = "Использование";
            var result = MessageBox.Show(info, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
