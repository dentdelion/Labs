using System;
using System.Collections.Generic;
using Cairo;
using Gdk;
namespace test3
{
	public class Graph
	{
		public const double M = 32000;
		public List <Vertex> vertexes;
		public List <Edge> edges;
		public int[,] adjmatrix;
		public double[,] fulladjmatrix;
		private static Vertex end = new Vertex (-1);
		public Queue <Vertex> queue;

		public List<Vertex> BFS() {
			List <Vertex> vertsArranged = new List<Vertex> ();
			queue = new Queue<Vertex> ();
			vertexes [0].setVisited ();
			vertsArranged.Add (vertexes [0]);
			queue.Enqueue (vertexes [0]);
			Vertex v2;
			while (queue.Count != 0) {
				Vertex v1 = queue.Dequeue ();
				while ((v2 = getAdjUnvisitedVertex (v1)) != end) {
					v2.setVisited ();
					vertsArranged.Add (v2);
					queue.Enqueue (v2);
				}		
			}
			foreach (Vertex vert in vertexes) {
				vert.setUnvisited ();
			}
			foreach (Vertex vert in vertsArranged) {
				Console.WriteLine (vert.position.X.ToString () + ", " + vert.position.Y.ToString ()); 
			}
			return vertsArranged;
		}

		public List<Vertex> DFS() {
			List<Vertex> vertsArranges = new List<Vertex> ();
			Stack<Vertex> stack = new Stack<Vertex> ();
			stack.Push (vertexes [0]);
			vertexes [0].setVisited ();
			vertsArranges.Add (vertexes [0]);
			Vertex v1, v2;
			while (stack.Count != 0) {
				v1 = stack.Peek ();
				v2 = getAdjUnvisitedVertex (v1);
				if (v2 != end) {
					stack.Push (v2);
					v2.setVisited ();
					vertsArranges.Add (v2);
				} else
					v1 = stack.Pop ();

			}
			foreach (Vertex vert in vertexes) {
				vert.setUnvisited ();
			}
			foreach (Vertex vert in vertsArranges) {
				Console.WriteLine (vert.number + " vertex - (" + vert.position.X.ToString () + ", " + vert.position.Y.ToString () + ")");
			}
			return vertsArranges;
		}

		Vertex getAdjVertex(Vertex v) {
			for (int j = 0; j < vertexes.Count; j++) {
				if (adjmatrix [v.number - 1, j] == 1)
					return vertexes [j];
			}
			return end;
		}
			
//		public static Vertex getClosestVertex(Graph graph, List<Vertex> verts, ref int iInAdjMatrix, ref int jInAdjMatrix) {
//			Vertex closest = new Vertex ();
//			double min = M;
//			foreach (Vertex vert in verts) {
//				for (int i = 0; i < graph.vertexes.Count; i++) {
//					if (graph.fulladjmatrix [vert.number - 1, i] != M && graph.fulladjmatrix [vert.number - 1, i] < min && !graph.vertexes[i].isVisited()) {
//						min = graph.fulladjmatrix [vert.number - 1, i];
//						closest = graph.vertexes [i];
//						iInAdjMatrix = i;
//						jInAdjMatrix = vert.number - 1;
//					}
//				}
//			}
//			return closest;
//		}
		public static Vertex getClosestVertex(Vertex v, Graph g) {
			Vertex closest = new Vertex ();
			double min = M;
			for (int i = 0; i < g.vertexes.Count; i++) {
				if (g.fulladjmatrix [v.number - 1, i] != M && g.fulladjmatrix [v.number - 1, i] < min && !g.vertexes[i].isVisited()) {
					min = g.fulladjmatrix [v.number - 1, i];
					closest = g.vertexes [i];
				}
			}
			return closest;		
		}

		Vertex getAdjUnvisitedVertex(Vertex v) {
			for (int j = 0; j < vertexes.Count; j++) {
				if (adjmatrix [v.number - 1, j] == 1 && vertexes[j].isVisited () == false)
					return vertexes [j];
			}
			return end;
		}
		public Graph (List<Vertex> verts) {
			fulladjmatrix = new double[verts.Count, verts.Count];
			vertexes = new List<Vertex> (verts);
			edges = new List<Edge>();
			adjmatrix = new int[verts.Count, verts.Count];
			Random rnd = new Random ();
			//Random filling and console output of adjacency matrix.
			for (int i = 0; i < verts.Count; i++) {
				for (int j = 0; j < verts.Count; j++) {
					if (i == j) {
						adjmatrix [i, j] = 0;
					} else if (i < j) {
						adjmatrix [i, j] = rnd.Next () % 2;
						adjmatrix [j, i] = adjmatrix [i, j];
					}
					Console.Write (adjmatrix [i, j].ToString () + " ");
				}
				Console.WriteLine ("");
			}

			//Console output of vertexes coordinates on the Drawing Area.
			for (int i = 0; i < vertexes.Count; i++) {
				Console.WriteLine (vertexes [i].position.X.ToString () + ", " + vertexes [i].position.Y.ToString ());
			}
			//Filling and colsole output of full adjacency matrix with edge lengths.
			Console.WriteLine ("Full adjacency matrix: \n");
			fillFullAdjMatrix (vertexes, adjmatrix);
			//Filling edge list based on vertexes list and randomly filled adj matrix.
			for (int i = 0; i < verts.Count; i++) {
				for (int j = 0; j < verts.Count; j++) {
					if (adjmatrix[i, j] == 1 && i < j) {
						edges.Add(new Edge(vertexes[i], vertexes[j]));
					}
				}
			}

			foreach (Vertex v in vertexes) {
				v.adjEdges = findAdjEdges (v);
				//Console.WriteLine (v.number + " ");
				//foreach (Edge e in v.adjEdges) 
				//	Console.Write (e.v[0].number + "-" + e.v[1].number + "\t");
			}
			foreach (Edge e in edges) {
				Console.WriteLine (e.v [0].number + "\t" +  e.v [1].number + "\t" +  e.length);
			}
		}

		void fillFullAdjMatrix(List <Vertex> vertexes, int[,] adjmatrix) {
			for (int i = 0; i < vertexes.Count; i++) {
				for (int j = 0; j < vertexes.Count; j++) {
					if (adjmatrix [i, j] == 1) {
						fulladjmatrix [i, j] = Math.Sqrt ((vertexes [i].position.X - vertexes [j].position.X) * (vertexes [i].position.X - vertexes [j].position.X)
							+ (vertexes [i].position.Y - vertexes [j].position.Y) * (vertexes [i].position.Y - vertexes [j].position.Y));
						Console.Write ((int)fulladjmatrix [i, j] + "\t");
					} else {
						fulladjmatrix [i, j] = M;
						Console.Write ("0\t");
					}
				}
				Console.WriteLine ("");
			}
		}
		//Метод для нахождения смежных ребер для каждой вершины
		public List<Edge> findAdjEdges(Vertex v) {
			List<Edge> adjEdges = new List<Edge> ();
			foreach (Edge e in edges) {
				if (e.v [0].Equals (v) || e.v [1].Equals (v))
					adjEdges.Add (e);
			}

			return adjEdges;
		}
		//Methos to find an edge with min length in a draph
		public Edge getMinimumEdge(){
			return Edge.findMinInList (edges);
		}


	}
}

