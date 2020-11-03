using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Symbol
{
	public class Symbol
	{
		private SymbolGraph _graph;

		private int _cols;
		private int _rows;
		private int _width;
		private int _height;
		private double _delta;


		private double _xMin;
		private double _xMax;
		private double _yMin;
		private double _yMax;


		private int _cast_square;


		private int _cast;

		private double _a;
		private double _b;

		private int _numberOfCells;

		public delegate double mappingFunction(double x, double y, double a, double b);

		public mappingFunction xFunction;
		public mappingFunction yFunction;
		public SymbolGraph Graph
        {
			get
            {
				return _graph;
            }
        }

		public int Scale
        {
            get
            {
				return _width / _cols;
            }
        }


		public int Cols
        {
			get
            {
				return _cols;
            }
        }

		public int Rows
        {
            get
            {
				return _rows;
            }
        }

		public Symbol(
			double xMin,
			double xMax,
			double yMin,
			double yMax,
			int cast,
			double delta,
			double a = 0,
			double b = 0,
			int width = 640,
			int height = 480
			)
		{
			_xMax = xMax;
			_xMin = xMin;
			_yMax = yMax;
			_yMin = yMin;
			_cast = cast;
			_delta = delta;
			_a = a;
			_b = b;
			_width = width;
			_height = height;

			_rows = (int)((double)(yMax - yMin) / delta);
			_cols = (int)((double)(xMax - xMin) / delta);

			_numberOfCells = _rows * _cols;
			_graph = new SymbolGraph(_numberOfCells);


			_cast_square = (int)Math.Ceiling(Math.Sqrt((double)_cast));
		}

		private int ReturnCell(double x, double y)
        {

			double xn = xFunction(x, y, _a, _b);
			double yn = yFunction(x, y, _a, _b);


			if (xn <= _xMin || xn >= _xMax || yn <= _yMin || yn >= _yMax)
				return -1;

			return (int)Math.Abs(((double)(yn - _yMax)/_delta)) * _cols + (int)Math.Abs( ((double)(xn - _xMin)/_delta));
        }


		class Edge
		{
			private List<int> num;
			private List<List<int>> verticies;

			public List<int> Num
            {
                get
                {
					return num; 
                }
                set
                {
					num = value;
                }
            }

			public List<List<int>> Verticies
            {
                get
                {
					return verticies;
                }

                set
                {
					verticies = value;
                }
            }

			public Edge()
            {
				num = new List<int>();
				verticies = new List<List<int>>();
            }
		}

		public void MakeGraph ()
        {

			if (xFunction == null || yFunction == null)
				return;

			//for(int i = 0; i < _numberOfCells; i++)
			Parallel.For(0, _numberOfCells, (i, state) =>
			{
				int row = (int)i / _cols;
				int col = (int)i % _cols;
				double x1 = _xMin + col * _delta;
				double y1 = _yMax - row * _delta;



				for (int k = 0; k <= _cast_square - 1; k++)
				{
					for (int t = 0; t <= _cast_square - 1; t++)
					{
						double x = x1 + (double)k * _delta / (double)_cast_square;
						double y = y1 - (double)t * _delta / (double)_cast_square;

						int cell = ReturnCell(x, y);

						if (cell == -1) continue;
						if (cell >= _numberOfCells) continue;

						if (!_graph.Contains(i))
							_graph[i] = new List<int>();

						if (!_graph[i].Contains(cell))
						{
							_graph[i].Add(cell);
						}
					}
				}
			});

        }


		private SymbolGraph TransposeGraph()
        {
			SymbolGraph tGraph = new SymbolGraph(_numberOfCells);

			foreach(KeyValuePair<int, List<int>> i in Graph.keyValuePairs)
            {
				foreach (var v in i.Value)
                {
					if (!tGraph.Contains(v))
                    {
						tGraph[v] = new List<int>();
					}
					if (!tGraph[v].Contains(i.Key))
						tGraph[v].Add(i.Key);
                }
            }

			return tGraph;
        }





		public List<int> TopologySort()
        {
			int[] color = new int[_numberOfCells];
			List<int> order = new List<int>();


			Action<int> depthFirstSearch = null;

			depthFirstSearch = (v) =>
			{
				color[v] = 1;
				if (Graph[v] != null)
					foreach (var g in Graph[v])
                    {
						if (color[g] != 1)
							depthFirstSearch(g);
                    }
				order.Add(v);
			};

			Action topologicalSort = null;
			topologicalSort = () =>
			{
				Parallel.For(0, _numberOfCells, (i, state) => { color[i] = 0; });

				Stack<int> dfs = new Stack<int>();

				order.Clear();

				foreach (KeyValuePair<int, List<int>> i in Graph.keyValuePairs)
				{
                    //if (!used[i.Key])
                    //    depthFirstSearch(i.Key);


                    if (color[i.Key] == 0)
                    {
                        dfs.Push(i.Key);
                    }

                    while (dfs.Count != 0)
                    {
                        var v = dfs.Pop();
						if (color[v] == 1)
						{
							color[v] = 2;
							order.Add(v);
							continue;
						}
						else if (color[v] == 2) continue;

                        color[v] = 1;
                        dfs.Push(v);

                        if (Graph[v] != null)
                            for (int j = Graph[v].Count - 1; j >= 0; j--)
                            {
                                int g = Graph[v][j];
                                if (color[g] == 0)
                                {
                                    dfs.Push(g);
                                }
                            }
                    }

                };
			};



			topologicalSort();
			order.Reverse();
			return order;
		}


		public List<List<int>> FindStrongConnectedComponents (bool isTopologySort = false)
        {
			List<int> order = TopologySort();
            bool[] used = new bool[_numberOfCells];
            //var used = new List<int>();

            SymbolGraph tGraph = TransposeGraph();
			List<List<int>> components = new List<List<int>>();

			Func<int, List<int>> depthFirstSearch = null;




			depthFirstSearch = start =>
			{
				var component = new List<int>();

				Stack<int> stack = new Stack<int>();

				stack.Push(start);

				while(stack.Count != 0)
                {
					int v = stack.Pop();
					used[v] = true;
					component.Add(v);

					if (tGraph[v] != null)
						for (int i = 0; i < tGraph[v].Count; i++)
                        {
							int g = tGraph[v][i];
							if (!used[g])
                            {
								stack.Push(g);
                            }
                        }
                }
				return component;
			};
			
			//Parallel.For(0, _numberOfCells, (i, state) => { used[i] = false; });

			foreach (var v in order)
			{
				if (!used[v] && tGraph.Contains(v))
				{
					List<int> component = depthFirstSearch(v);

					if (component.Count > 1 || isTopologySort)
					{
						components.Add(component);
					}
				}
			}

			return components;
		}




		public int ReturnCol(int cell)
        {
			return cell % _cols;
        }

		public int ReturnRow (int cell)
        {
			return cell / _cols;
        }


		private int[] NewCoords(int cell, int cols)
		{
			int row = cell / cols;
			int col = cell % cols;

			int[] cells = new int[4];

			cells[0] = 2 * row * _cols + 2 * col;
			cells[1] = 2 * row * _cols + 2 * col + 1;
			cells[2] = (2 * row + 1)* _cols + 2 * col;
			cells[3] = (2 * row + 1)* _cols + 2 * col + 1;

			return cells;
		}

		private void ReturnInterval (int cell, out double x, out double y)
        {
			int row = ReturnRow(cell);
			int col = ReturnCol(cell);

			x = _xMin + (double)col * _delta;
			y = _yMax - (double)row * _delta;
        }


		List<int> FindMaxComponent(List<List<int>> components)
        {
			List<int> maxComponent = components[0];
			foreach (var c in components)
            {
				if (c.Count > maxComponent.Count)
					maxComponent = c;
            }

			return maxComponent;
        }

		private int OldCoordSystem(int cell, int oldCols)
		{
			int row = ReturnRow(cell);
			int col = ReturnCol(cell);

			return (row / 2) * oldCols + (col / 2);
		}


		private bool ContainsCell (int cell, List<List<int>> components)
        {
			foreach (var component in components)
				if (component.Contains(cell)) return true;

			return false;
        }

		public List<List<int>> MakeNewGraph(int n, List<List<int>> components)
        {

			int oldCols = _cols;


			//List<int> component = FindMaxComponent(components);
			for (int i = 0; i < n; i++)
            {
				_numberOfCells *= 4;
				_cols *= 2;
				_delta *= 0.5;
				_rows *= 2;
				SymbolGraph graph = new SymbolGraph(_numberOfCells);

				foreach (var component in components)
				{
					Parallel.ForEach(component, (c) =>
					//foreach(var c in component)
					{
					   int[] cells = NewCoords(c, oldCols);

					   for (int m = 0; m < 4; m++)
					   {
						   double x, y;
						   ReturnInterval(cells[m], out x, out y);

						   for (int l = 0; l < _cast_square - 1; l++)
						   //Parallel.For(0, _cast, (l, state) =>
						   {
								for (int d = 0; d < _cast_square - 1; d++)
								{
									double xn = x + (double)l * _delta / (double)_cast_square;
									double yn = y - (double)d * _delta / (double)_cast_square;


									int cell = ReturnCell(xn, yn);

									if (cell == -1) continue;
									if (cell >= _numberOfCells) continue;

									int oldCell = OldCoordSystem(cell, oldCols);

									if (!Graph.Contains(oldCell)) continue;

									if (!graph.Contains(cells[m]))
									{
										graph[cells[m]] = new List<int>();
									}

									if (graph[cells[m]] != null && !graph[cells[m]].Contains(cell))
									{
										graph[cells[m]].Add(cell);
									}
								}

						   }


					   }


				   });
					_graph = graph;

					

				};
				components = FindStrongConnectedComponents();
				oldCols = _cols;

			}

			return components;
        }
	}
}
