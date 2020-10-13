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


        Point yCoordUp = new Point(400, 800);
        Point yCoordDown = new Point(400, 0);
        Point xCoordLeft = new Point(0, 400);
        Point xCoordRight = new Point(800, 400);

        int width;
        int height;


        Symbol symbol;
        List<List<int>> strongComponents;


        public Form2(Symbol symbol, int width, int height)
        {
            InitializeComponent();
            this.symbol = symbol;
            this.Width = width;
            this.Height = height;

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


        void DrawSquare (int cell, PaintEventArgs e, SolidBrush brush)
        {
            int row = symbol.ReturnRow(cell);
            int col = symbol.ReturnCol(cell);

            e.Graphics.FillRectangle(brush, col * symbol.Scale, row * symbol.Scale, symbol.Scale + 1, symbol.Scale + 1);
        }


        void DrawComponents(PaintEventArgs e)
        {
            Random random = new Random();
            for (int i = 0; i < strongComponents.Count; i++)
            {
                if (strongComponents[i].Count > 5)
                {
                    SolidBrush brush = new SolidBrush(Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
                    foreach (var v in strongComponents[i])
                    {
                        DrawSquare(v, e, brush);
                    }
                }
            }
        }
    }
}
