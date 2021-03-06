﻿using System;
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
            _symbol.MakeGraph();
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
                case Actions.FindComponents:
                    {
                        FindStrongConnectedComponents();
                        break;
                    }
            }

            var endTime = DateTime.Now - time;

            lblTime.Text = endTime.TotalMilliseconds.ToString() + " милисекунд.";
        }


        private void BuildGraph()
        {
            progressBar1.Value += 100;

            textBoxOutput.Text = "Построение символического образа прошло успешно. Формат представления: номер веришины: образы этой вершины через запятую.\r\n";

            for (int i = 0; i < _symbol.Graph.Capacity; ++i)
            {
                textBoxOutput.Text += i.ToString() + ": ";
                if (_symbol.Graph[i] != null)
                for (int j = 0; j < _symbol.Graph[i].Count; ++j)
                {
                    textBoxOutput.Text += _symbol.Graph[i][j].ToString() + ", ";
                }
                textBoxOutput.Text += "\r\n";
            }
        }


        private void TopologySort ()
        {


            progressBar1.Value += 50;

            List<List<int>> components = _symbol.FindStrongConnectedComponents(true);

            progressBar1.Value += 50;

            textBoxOutput.Text = "Топологическая сортировка прошла успешно. Внизу показан граф конденсации \r\n";
            textBoxOutput.Text += "Количество вершин и компонент: " + components.Count.ToString() + "\r\n";

            int n = 1;
            for (int i = 0; i < components.Count; ++i)
            {
                if (components[i].Count == 1) textBoxOutput.Text += components[i][0].ToString() + "\r\n";
                else
                {
                    textBoxOutput.Text += "H" + n++;
                    textBoxOutput.Text += "\r\n";
                }
            }
        }


        private void FindStrongConnectedComponents ()
        {
            progressBar1.Value += 50;

            List<List<int>> components = _symbol.FindStrongConnectedComponents();

            progressBar1.Value += 50;

            textBoxOutput.Text = "Компоненты сильной связности найдены. Всего компонент: " + components.Count.ToString() + "\r\n";

            for (int i = 0; i < components.Count; i++)
            {
                textBoxOutput.Text += "Компонента № " + (i + 1).ToString() + "\r\n";
                foreach(int v in components[i])
                {
                    textBoxOutput.Text += v.ToString() + ", ";
                }

                textBoxOutput.Text += "\r\n";
            }

        }
    }
}
