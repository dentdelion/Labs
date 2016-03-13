using System;
using System.Collections.Generic;
namespace test3
{
	public class Edge
	{
		
		public double length { get; set; }
		public List<Vertex> v { get; set; }
		public Edge() {
			v = new List<Vertex> (2);
			length = 0;
			//visited = false;
		}

//		public bool isVisited() {
//			return visited;
//		}
//		public void setVisited() {
//			visited = true;
//		}
//		public void setUnvisited() {
//			visited = false;
//		}

		public Edge(Vertex v1, Vertex v2) {
			length = AGM.getLengthBetween(v1, v2);
			v = new List<Vertex> (2);
			v.Add(v1);
			//visited = false;
			v.Add(v2);
		}
		public static Edge findMinInList(List<Edge> edges) {
			if (edges.Count == 0) {
				Console.WriteLine ("List of edges is empty!");
				return null;
			}
			if (edges.Count == 1)
				return edges [0];
			Edge min = new Edge();
			double minD = 32000;
			foreach (Edge e in edges) {
				if (e.length < minD) {
					minD = e.length;
					min = e;
				}
			}
			return min;
		}
	}
}

