using System;
using System.Collections.Generic;

namespace test3
{
	public class Dijkstra
	{
		static Graph graph;
		public const double M = 32000;
		static private List<Vertex> minWay;//Минимальный путь, найденный после выполнения алгоритма. 
		static private List<double> marks; //Марки для каждой из вершин. Считаются как минимальное из всех путей к вершине.
		//private List<Vertex> neighbours;
		public Dijkstra (Graph g)
		{
			//neighbours = new List<Vertex> ();
			minWay = new List<Vertex> ();
			graph = g;
			marks = new List<double> ();
			marks.Add (0);
			for (int i = 1; i < graph.vertexes.Count; i++) {
				marks.Add (M);
			}
		
		}
		public static int IndexOfMin(IList<double> self)
		{
			if (self == null) {
				throw new ArgumentNullException("self");
			}
			if (self.Count == 0) {
				throw new ArgumentException("List is empty.", "self");
			}
			double min = self[0];
			int minIndex = 0;
			for (int i = 1; i < self.Count; ++i) {
				if (self[i] < min) {
					min = self[i];
					minIndex = i;
				}
			}
			return minIndex;
		}
		private void findMinWay() {
			minWay.Add (graph.vertexes [0]);
			List<double> distances = new List<double> ();
			distances.Add (0);
			for (int i = 1; i < graph.vertexes.Count; i++) {
				graph.vertexes [i].setUnvisited ();
				distances.Add (M);
				minWay.Add (graph.vertexes [i]); //test
			}
			for (int i = 0; i < minWay.Count; i++) {
				Console.WriteLine (minWay [i].position.X + ", " + minWay [i].position.Y);
			}
//			while (checkHasUnvisited(graph.vertexes)) {
//				Vertex v1 = graph.vertexes[IndexOfMin (distances)];
//				v1.setVisited();
//				List<Vertex> neighbours = new List<Vertex>(findUnvisitedNeighboursFor(v1));
//				for (int i = 0; i < neighbours.Count; i++) {
//					if (distances[neighbours[i].number - 1] 
//						> (distances[v1.number -1] + getLengthBetween(v1, neighbours[i]))) {
//						distances[neighbours[i].number - 1] = distances[v1.number -1] + getLengthBetween(v1, neighbours[i]);
//						minWay.Add(v1);
//					}
//				}
//			}
		}
		public bool checkHasUnvisited(List<Vertex> self) {
			bool a = false; 
			for (int i = 0; i < self.Count; i++) {
				if (!self [i].isVisited ()) {
					a = true;
					break;
				}				
			}
			return a;
		}
		public List<Vertex> getMinWay() {
			findMinWay ();
			return minWay;
		}
		private List<Vertex> findUnvisitedNeighboursFor(Vertex v) {
			var Neighbours = new List<Vertex> ();
			for (int i = 0; i < graph.vertexes.Count; i++) {
						if (graph.adjmatrix [v.number - 1, i] == 1 && !graph.vertexes[i].isVisited())
					Neighbours.Add (graph.vertexes [i]);
			}
			return Neighbours;
		}

	}
}

