﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SharpGL;

namespace Symbol
{
    public partial class Form2 : Form
    {

        OpenGL gl;
        bool isDrawed = false;

        Actions selectedAction;

        Symbol symbol;
        List<List<int>> strongComponents;
        double[] p;


        public Form2(Symbol symbol, int width, int height, int n, Actions action)
        {
            InitializeComponent();
            this.symbol = symbol;
            this.Width = width;
            this.Height = height;

            selectedAction = action;
            symbol.MakeGraph();


            if (action == Actions.SetLocalization)
            {
                strongComponents = symbol.FindStrongConnectedComponents();
                if (n > 0)
                {
                    strongComponents = symbol.MakeNewGraph(n, strongComponents);
                }
            }
            else if (action == Actions.DrawAttractor)
            {
                strongComponents = symbol.FindStrongConnectedComponents(true);
            }
            else if (action == Actions.BalanceMethod)
            {
                strongComponents = symbol.FindStrongConnectedComponents(false);

                p = symbol.BalanceMethod(strongComponents, n);
            }
            gl = this.openGLControl1.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);

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

        void DrawAttractor ()
        {
            for (int i = 0; i < strongComponents.Count; i++)
            {
                if (strongComponents[i].Count > 1)
                {
                    gl.Color(0f, 1f, 0f);
                    foreach(var v in strongComponents[i])
                    {
                        DrawSquare(v);
                    }
                }
                else
                {
                    gl.Color(1f, 0f, 0f);
                    DrawSquare(strongComponents[i][0]);
                }
            }
        }

        void DrawCyllinder (double x, double y, double height, double s)
        {

            //gl.Color(0.1f, 0.8f, 0.1f);
            //gl.Begin(OpenGL.GL_POLYGON);

            //gl.Vertex(x, y, 0);
            //gl.Vertex(x, y, height);
            //gl.Vertex(x, y + s, height);
            //gl.Vertex(x, y + s, 0);

            //gl.End();

            //gl.Begin(OpenGL.GL_POLYGON);

            //gl.Vertex(x, y + s, 0);
            //gl.Vertex(x, y + s, height);
            //gl.Vertex(x + s, y + s, height);
            //gl.Vertex(x + s, y + s, 0);

            //gl.End();

            //gl.Begin(OpenGL.GL_POLYGON);

            //gl.Vertex(x + s, y + s, 0);
            //gl.Vertex(x + s, y + s, height);
            //gl.Vertex(x + s, y, height);
            //gl.Vertex(x + s, y, 0);

            //gl.End();


            //gl.Begin(OpenGL.GL_POLYGON);

            //gl.Vertex(x + s, y + s, 0);
            //gl.Vertex(x + s, y + s, height);
            //gl.Vertex(x, y, height);
            //gl.Vertex(x, y, 0);

            //gl.End();

            gl.Color(0.8f, 0.1f, 0.1f);
            gl.Begin(OpenGL.GL_POLYGON);

            gl.Vertex(x, y, 0);
            gl.Vertex(x, y + s, 0);
            gl.Vertex(x + s, y + s, 0);
            gl.Vertex(x + s, y, 0);

            gl.End();

            gl.Color(0.1f, 0.1f, 0.8f);
            gl.Begin(OpenGL.GL_POLYGON);

            gl.Vertex(x, y, height);
            gl.Vertex(x, y + s, height);
            gl.Vertex(x + s, y + s, height);
            gl.Vertex(x + s, y, height);

            gl.End();
        }

        void DrawMeasures ()
        {
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Rotate(-45.0f, 1.0f, 0.0f, 0.0f);
            gl.Rotate(45.0f, 0.0f, 0.0f, 1.0f);
            //gl.Translate(0.0f, 0.0f, -1.5f);
            foreach (var v in strongComponents[0])
            {
                int row = symbol.ReturnRow(v);
                int col = symbol.ReturnCol(v);

                double x = 2 * ((double)col / (double)symbol.Cols) - 1;
                double y = -2 * ((double)row / (double)symbol.Rows) + 1;
                
                DrawCyllinder(x, y, p[v], 0.005);
            }
        }

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            if (!isDrawed)
            {
                gl.ClearColor(1f, 1f, 1f, 1f);
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                gl.LoadIdentity();


                //gl.Translate(0, 0, -2.25f);



                DrawGrid();
                if (selectedAction == Actions.SetLocalization)
                    DrawComponents();
                else if (selectedAction == Actions.DrawAttractor)
                    DrawAttractor();
                else if (selectedAction == Actions.BalanceMethod)
                    DrawMeasures();

                isDrawed = true;
            }

        }


    }
}
