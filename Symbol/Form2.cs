using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SharpGL;

namespace Symbol
{
    public partial class Form2 : Form
    {
        Pen blackPen = new Pen(Color.Black, 1);

        OpenGL gl;



        bool isDrawed = false;
        Point yCoordUp;
        Point yCoordDown;
        Point xCoordLeft;
        Point xCoordRight;

        int width;
        int height;


        Symbol symbol;
        List<List<int>> strongComponents;


        public Form2(Symbol symbol, int width, int height, int n)
        {
            InitializeComponent();
            this.symbol = symbol;
            this.Width = width;
            this.Height = height;

            xCoordLeft = new Point(0, height/2);
            xCoordRight = new Point(width, height/2);
            yCoordDown = new Point(width/2, 0);
            yCoordUp = new Point(width/2, height);

            symbol.MakeGraph();

            strongComponents = symbol.FindStrongConnectedComponents();
            if (n > 0)
            {
                strongComponents = symbol.MakeNewGraph(n, strongComponents);
            }
                
            gl = this.openGLControl1.OpenGL;



        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.Clear(Color.White);
            //e.Graphics.DrawLine(blackPen, yCoordDown, yCoordUp);
            //e.Graphics.DrawLine(blackPen, xCoordRight, xCoordLeft);


            //DrawComponents(e);

        }


        void DrawSquare (int cell)
        {
            int row = symbol.ReturnRow(cell);
            int col = symbol.ReturnCol(cell);

            //e.Graphics.FillRectangle(brush, col * symbol.Scale, row * symbol.Scale, symbol.Scale + 1, symbol.Scale + 1);
            gl.Rect(2 * ((double)col /(double)symbol.Cols) - 1,
                -2 * ((double)row / (double) symbol.Rows) + 1,
                2 * ((double)(col + 1)/ (double) symbol.Cols) - 1,
                -2 * ((double)(row + 1) / (double) symbol.Rows) + 1);

            // gl.Rect(-1, 1, 1, -1);
        }



        void DrawGrid()
        {
            gl.Color(0f, 0f, 0f);
            gl.Begin(OpenGL.GL_LINES);

            gl.Vertex(0, 1);
            gl.Vertex(0, -1);
            gl.Vertex(1, 0);
            gl.Vertex(-1, 0);

            gl.End();
        }

        void DrawComponents()
        {
            Random random = new Random();
            for (int i = 0; i < strongComponents.Count; i++)
            {
                if (strongComponents[i].Count > 1)
                {
                    //SolidBrush brush = new SolidBrush(Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));

                    gl.Color(random.NextDouble(),random.NextDouble(), random.NextDouble()) ;
                    foreach (var v in strongComponents[i])
                    {
                        DrawSquare(v);
                    }
                }
            }
        }

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            if (!isDrawed)
            {
                gl.ClearColor(1f, 1f, 1f, 1f);
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                gl.LoadIdentity();


                gl.Translate(0, 0, -2.25f);



                DrawGrid();
                DrawComponents();

                isDrawed = true;
            }

        }


    }
}
