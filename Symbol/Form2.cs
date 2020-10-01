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
    public partial class Form2 : Form
    {
        Pen blackPen = new Pen(Color.Black, 1);

        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush redBrush = new SolidBrush(Color.Red);

        Point yCoordUp = new Point(400, 800);
        Point yCoordDown = new Point(400, 0);
        Point xCoordLeft = new Point(0, 400);
        Point xCoordRight = new Point(800, 400);


        Symbol symbol;
        List<List<int>> strongComponents;


        public Form2(Symbol symbol)
        {
            InitializeComponent();
            this.symbol = symbol;

            symbol.MakeGraph();
            strongComponents = symbol.FindStrongConnectedComponents();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawLine(blackPen, yCoordDown, yCoordUp);
            e.Graphics.DrawLine(blackPen, xCoordRight, xCoordLeft);


            DrawComponents(e);

        }


        void DrawSquare (int cell, PaintEventArgs e)
        {
            int row = symbol.ReturnRow(cell);
            int col = symbol.ReturnCol(cell);

            e.Graphics.FillRectangle(redBrush, col, row, 1, 1);
        }


        void DrawComponents(PaintEventArgs e)
        {
            for (int i = 0; i < strongComponents.Count; i++)
            {
                if (strongComponents[i].Count > 5)
                {
                    foreach (var v in strongComponents[i])
                    {
                        DrawSquare(v, e);
                    }
                }
            }
        }
    }
}
