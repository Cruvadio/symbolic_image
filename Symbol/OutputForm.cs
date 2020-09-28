using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Symbol
{
    public partial class OutputForm : Form
    {

        private Symbol _symbol;
        private Actions _action;
        public OutputForm()
        {
            InitializeComponent();
        }

        public OutputForm(Symbol symbol, Actions action)
        {
            InitializeComponent();
            _symbol = symbol;
            _action = action;
        }

        private void OutputForm_Shown(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            switch (_action)
            {
                case Actions.BuildGraph:
                    {
                        BuildGraph();
                        break;
                    }
                case Actions.TopologySort:
                    {
                        TopologySort();
                        break;
                    }
            }

            var endTime = DateTime.Now - time;

            lblTime.Text = endTime.TotalMilliseconds.ToString() + " милисекунд.";
        }


        private void BuildGraph()
        {
            _symbol.MakeGraph();

            progressBar1.Value += 100;

            textBoxOutput.Text = "Построение символического образа прошло успешно. Формат представления: номер веришины: образы этой вершины через запятую.\r\n";

            for (int i = 0; i < _symbol.Graph.Length; ++i)
            {
                textBoxOutput.Text += i.ToString() + ": ";
                for (int j = 0; j < _symbol.Graph[i].Count; ++j)
                {
                    textBoxOutput.Text += _symbol.Graph[i][j].ToString() + ", ";
                }
                textBoxOutput.Text += "\r\n";
            }
        }


        private void TopologySort ()
        {
            _symbol.MakeGraph();

            progressBar1.Value += 50;

            List<int> ans = _symbol.TopologySort();

            progressBar1.Value += 50;

            textBoxOutput.Text = "Топологическая сортировка прошла успешно. Вершины показаны в порядке возрастания времени выхода из вершины: \r\n";

            for (int i = 0; i < ans.Count; ++i)
            {
                textBoxOutput.Text += ans[i].ToString() + "\r\n";
            }
        }
    }
}
