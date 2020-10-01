using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Symbol
{
	public class Symbol
	{
		private List<int>[] _graph;

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
		public List<int>[] Graph
        {
			get
            {
				return _graph;
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
			_graph = new List<int>[_numberOfCells];
		}

		double JuliaX(double xn, double yn)
        {
			return xn * xn - yn * yn + _a;
        }

		double JuliaY(double xn, double yn)
		{
			return 2 * xn * yn + _b;
		}

		private int ReturnCell(double x, double y)
        {

			double xn = xFunction(x, y, _a, _b);
			double yn = yFunction(x, y, _a, _b);


			if (xn <= _xMin || xn >= _xMax || yn <= _yMin || yn >= _yMax)
				return -1;

			return (int)Math.Abs(((double)(yn - _yMax)/_delta)) * _cols + (int)Math.Abs( ((double)(xn - _xMin)/_delta));
        }


		public void MakeGraph ()
        {
			if (xFunction == null || yFunction == null)
				return;

			for(int i = 0; i< _numberOfCells; i++)
			{
				int row = i / _cols;
				int col = i % _cols;
				double x1 = _xMin + col * _delta;
				double y1 = _yMax - row * _delta;
				_graph[i] = new List<int>();

				for (int k = 1; k <= _cast; k++)
				{
					double x = x1 + (double)k * _delta / (double)_cast;
					double y = y1 - (double)k * _delta / (double)_cast;

					int cell = ReturnCell(x, y);

					if (cell == -1) continue;
					if (cell >= _numberOfCells) continue;

					if (!_graph[i].Contains(cell))
					{
						_graph[i].Add(cell);
					}
				}
			};

        }


		private List<int>[] TransposeGraph()
        {
			List<int>[] tGraph = new List<int>[_numberOfCells];
			Parallel.For(0, _numberOfCells, (i, state) =>
			{
				tGraph[i] = new List<int>();
			});

			for (int i = 0; i < _numberOfCells; ++i)
			{
				for(int j = 0; j < Graph[i].Count; ++j)
				{
					int g = Graph[i][j];
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
					for (int i = 0; i < Graph[v].Count; ++i)
                    {
						int g = Graph[v][i];
						if (!used[g])
                        {
							stack.Push(Graph[v][i]);
							used[Graph[v][i]] = true;
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

			List<int>[] tGraph = TransposeGraph();
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
				if (!used[v])
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

	}
}
