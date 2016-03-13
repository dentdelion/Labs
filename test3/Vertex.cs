using System;
using Gtk;
using System.Collections.Generic;
using Cairo;
namespace test3
{
	public class Vertex
	{
		public List<Edge> adjEdges { get; set; } //смежные с вершиной ребра
		public static int nextInt = 1;
		public int number;
		private bool visited;
		public bool isVisited() {
			return visited;
		}
		public void setVisited() {
			visited = true;
		}
		public void setUnvisited() {
			visited = false;
		}
		public PointD position;
		public Vertex() {
			number = nextInt++;
			visited = false;
			adjEdges = new List<Edge> ();
		}
		public Vertex(int num) {
			number = num;
			visited = false;
			adjEdges = new List<Edge> ();
		}

		public Vertex(PointD pos) {
			number = nextInt++;
			visited = false;
			position = pos;
			adjEdges = new List<Edge> ();
		}

		public Edge findMinAdjEdge() {
			if (adjEdges.Count == 0)
				return null;
			return Edge.findMinInList (adjEdges);
		}
	}
}

