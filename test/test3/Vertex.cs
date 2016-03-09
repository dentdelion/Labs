using System;
using Gtk;
using Cairo;
namespace test3
{
	public class Vertex
	{
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
		}
		public Vertex(int num) {
			number = num;
			visited = false;
		}
		public PointD getCenterWithVertex(Vertex v) {
			return new PointD ((this.position.X - v.position.X) / 2, (this.position.Y - v.position.Y) / 2);
		}
		public Vertex(PointD pos) {
			number = nextInt++;
			visited = false;
			position = pos;
		}
	}
}

