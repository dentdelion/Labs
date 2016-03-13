using System;
using Gtk;
using Cairo;
using System.Collections.Generic;

namespace test3
{
	public class myTree
	{
		/*
		 * Класс остовного дерева. Состоит из вершин и матрицы смежности вершин дерева. 
		 * Два алгоритма - Крускалла и Прима. В качестве аргументов для алгоритмов передается граф.
		 * Для построения дерева графа на контексте по тому или иному алгоритму необходимо: 
		 * создать объект дерева, (конструктор принимает список вершин графа, так как в остовном дереве находятся все вершины), 
		 * вызвать метод необходимого алгоритма,
		 * передать в него граф
		 * и вызвать метод doDraw, который в качестве аргумента принимает контекст. 
		 */
		List<Edge> edges;
		List <Vertex> vertexes;
		int [,] adjmatrix { get; set; }


		public myTree (List <Vertex> verts)
		{
			edges = new List<Edge> ();
			this.vertexes = new List<Vertex> (verts);
			adjmatrix = new int[vertexes.Count, vertexes.Count];
		}

		public myTree () {
			vertexes = new List<Vertex> ();
			edges = new List<Edge> ();
			adjmatrix = new int[vertexes.Count, vertexes.Count];
		}
		//Method to find the cut of graph between the set of vertexes which are already in a tree;
		//and the set of vertexes which are not in a tree. Further we use this method in Prim algorithm,
		//@see primAlgorithm
		//@param g - graph with all the v-xes, tree - tree with 1 set of vertexes
		//TODO: cut method for two sets of vertexes
		public static List<Edge> cut (Graph g, myTree tree) {
			List <Vertex> belongToTree = new List<Vertex> (tree.vertexes);
			List <Vertex> notBelongToTree = new List<Vertex> ();
			List <Edge> res = new List<Edge> ();
			//divide vertexes to two sets
			foreach (Vertex v in g.vertexes) {
				if (!belongToTree.Contains (v)) {
					notBelongToTree.Add (v);
				}
			}
			foreach (Edge e in g.edges) {
				if ((belongToTree.Contains (e.v [0]) && notBelongToTree.Contains (e.v [1]))
				    || (belongToTree.Contains (e.v [1]) && notBelongToTree.Contains (e.v [0]))) {
					res.Add (e);
				}
			}
			return res;
		}
		public void primAlgorithm(Graph graph, Context g) {
			g.SetSourceRGB (255, 0, 0);
			myTree tree = new myTree ();
			//List<Edge> temp = new List<Edge> ();
			tree.vertexes.Add (graph.vertexes [0]);
			while (tree.vertexes.Count != graph.vertexes.Count) {
				Edge e1 = Edge.findMinInList (cut(graph, tree));
				tree.edges.Add (e1);
				Console.WriteLine ("Added: " + e1.v [0].number + ", " + e1.v [1].number);
				if (tree.vertexes.Contains (e1.v [1]))
					tree.vertexes.Add (e1.v [0]);
				else
					tree.vertexes.Add (e1.v [1]);
			}
			foreach (Edge ed in tree.edges) {
				drawEdge (ed, g);
			}
			//System.Timers.Timer tm = new System.Timers.Timer (2500);
//			tm.AutoReset = true;
//			tm.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) => {
//				drawEdge(graph.edges[1], g);
//			};
//			tm.Enabled = true;
		}

		public void cruscalAlgorithm(Graph graph, Context g) {
			myTree tree = new myTree ();
			List<Edge> unadded = new List<Edge> (graph.edges);
			Edge e1 = Edge.findMinInList (unadded);
			tree.edges.Add (e1);
			tree.vertexes.Add (e1.v [0]);
			tree.vertexes.Add (e1.v [1]);
			unadded.Remove (e1);
			while (tree.vertexes.Count != (graph.vertexes.Count - 1) && unadded.Count != 0) {
				Edge e2 = Edge.findMinInList (unadded);
				Console.WriteLine ("Min: " + e2.v [0].number + ", " + e2.v [1].number);
				if (!(tree.vertexes.Contains (e2.v [0]) && tree.vertexes.Contains (e2.v [1]))) {
					tree.edges.Add (e2);
					Console.WriteLine ("Added: " + e2.v [0].number + ", " + e2.v [1].number);
					unadded.Remove (e2);
					if (!tree.vertexes.Contains (e2.v [0])) {
						tree.vertexes.Add (e2.v [0]);
						Console.WriteLine ("Vertex added: " + e2.v [0].number);
					} 
					if (!tree.vertexes.Contains (e2.v [1])) {
						tree.vertexes.Add (e2.v [1]);
						Console.WriteLine ("Vertex added: " + e2.v [1].number);
					}
				}
			}
			g.SetSourceRGB (255, 0, 0);
			foreach (Edge ed in tree.edges) {
				drawEdge (ed, g);
			}
		}
		
		//The only graphics method in the class. Draws a line which presents an edge of a graph on a context. 
		public void drawEdge(Edge e, Context g) {
			try {
				g.MoveTo (e.v [0].position);
				g.LineTo (e.v [1].position);
				g.Stroke ();
			} catch (ArgumentOutOfRangeException e1) {
			}
		}
	}
}

