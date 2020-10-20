using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
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

			//Mutex[] mut = new Mutex[_numberOfCells];
			if (xFunction == null || yFunction == null)
				return;

			//for(int i = 0; i < _numberOfCells; i++)
			Parallel.For<Edge>(0, _numberOfCells,()=> { return (new Edge()); }, (i, state, edge) =>
			{
				int row = (int)i / _cols;
				int col = (int)i % _cols;
				double x1 = _xMin + col * _delta;
				double y1 = _yMax - row * _delta;
				//edge = new Edge();
				edge.Num.Add((int)i);
				//mut[i] = new Mutex();
				edge.Verticies.Add(null);

				for (int k = 1; k <= _cast; k++)
				{
					double x = x1 + (double)k * _delta / (double)_cast;
					double y = y1 - (double)k * _delta / (double)_cast;

					int cell = ReturnCell(x, y);

					if (cell == -1) continue;
					if (cell >= _numberOfCells) continue;


					if (edge.Verticies.Count !=0 && edge.Verticies[edge.Verticies.Count - 1] == null)
						edge.Verticies[edge.Verticies.Count - 1] = new List<int>();


					if (!edge.Verticies[edge.Verticies.Count - 1].Contains(cell))
					{
						edge.Verticies[edge.Verticies.Count - 1].Add(cell);
					}
				}
				return edge;
			}, (edge) => 
			{ 
				for(int i = 0; i < edge.Verticies.Count; i++)
                {
					if (edge.Verticies[i] != null)
                    {
						_graph[edge.Num[i]] = edge.Verticies[i];
                    }
                }
			});

        }


		private SymbolGraph TransposeGraph()
        {
			SymbolGraph tGraph = new SymbolGraph(_numberOfCells);

			for (int i = 0; i < _numberOfCells; ++i)
			{
				if (Graph[i] != null)
				for(int j = 0; j < Graph[i].Count; ++j)
				{

					int g = Graph[i][j];

					if (!tGraph.Contains(g))
					{
							tGraph[g] = new List<int>();
					}

					tGraph[g].Add(i);
				};
			};

			return tGraph;
        }

		public List<int> TopologySort()
        {
			bool[] used = new bool[_numberOfCells];
			List<int> order = new List<int>(_numberOfCells);


			Action<int> depthFirstSearch = null;

			depthFirstSearch = start =>
			{

				Stack<int> stack = new Stack<int>();

				stack.Push(start);

				while (stack.Count != 0)
                {
					int v = stack.Pop();
					used[v] = true;
					if (Graph[v] != null)
					{
						for (int i = 0; i < Graph[v].Count; ++i)
						{
							int g = Graph[v][i];
							if (!used[g])
							{
								stack.Push(Graph[v][i]);
								used[Graph[v][i]] = true;
							}
						}
					}
					order.Add(v);					
                }

				//used[v] = true;
				//for (int i = 0; i < Graph[v].Count; ++i)
				//{ 
				//	int to = Graph[v][i];
				//	if (!used[to])
				//		depthFirstSearch(to);
				//}
				//ans.Add(v);
			};


			Action topologicalSort = null;
			topologicalSort = () =>
			{
				Parallel.For(0, _numberOfCells, (i, state) => { used[i] = false; });
					

				order.Clear();

				for(int i = 0; i < _numberOfCells; ++i)
				{
					if (!used[i])
						depthFirstSearch(i);
				};
					
				order.Reverse();
			};


			topologicalSort();

			return order;
		}


		public List<List<int>> FindStrongConnectedComponents ()
        {
			List<int> order = TopologySort();
			bool[] used = new bool[_numberOfCells];

			SymbolGraph tGraph = TransposeGraph();
			List<List<int>> components = new List<List<int>>();

			Func<int, List<int>> depthFirstSearch = null;

			depthFirstSearch = start =>
			{
				List<int> component = new List<int>();
				Stack<int> stack = new Stack<int>();

				stack.Push(start);

				while (stack.Count != 0)
				{
					int v = stack.Pop();
					used[v] = true;
					component.Add(v);
					if (tGraph[v] == null) continue;
					for (int i = 0; i < tGraph[v].Count; ++i)
					{
						int g = tGraph[v][i];
						if (!used[g])
						{
							stack.Push(g);
							used[g] = true;
						}
					}

				}

				return component;
			};
			Parallel.For(0, _numberOfCells, (i, state) => { used[i] = false; });

			for (int i = 0; i < _numberOfCells; ++i)
			{
				int v = order[i];

				if (tGraph.Contains(v) && !used[v])
				{
					List<int> component = depthFirstSearch(v);

					if (component.Count > 1)
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

		private bool ContainsCell (int cell, List<List<int>> components)
        {
			foreach (var component in components)
				foreach (var c in component)
					if (c == cell) return true;

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
					//Parallel.ForEach(component, (c) =>
					foreach(var c in component)
				   {
					   int[] cells = NewCoords(c, oldCols);

						for (int m = 0; m < 4; m++)
					   {
						   double x, y;
						   ReturnInterval(cells[m], out x, out y);

						   for (int l = 0; l < _cast; l++)
						   //Parallel.For(0, _cast, (l, state) =>
						  {
							  double xn = x + (double)l * _delta / (double)_cast;
							  double yn = y - (double)l * _delta / (double)_cast;


							  int cell = ReturnCell(xn, yn);

							  if (cell == -1) continue;
							  if (cell >= _numberOfCells) continue;

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
					_graph = graph;

					

				};
				components = FindStrongConnectedComponents();
				oldCols = _cols;

			}

			return components;
        }
	}
}
