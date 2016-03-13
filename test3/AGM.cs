using System;
using Gtk;
using Cairo;
namespace test3
{
	/*
	 * Static class for analitical geometry methods (getting length of a line between points;
	 * getting center; etc).
	 * 
	 */
	public static class AGM
	{
		
		public static double getLengthBetween(Vertex v1, Vertex v2) {
			return (Math.Sqrt (Math.Pow (v1.position.X - v2.position.X, 2) + Math.Pow (v1.position.Y - v2.position.Y, 2)));
		}

		public static PointD getCenterBetweenVertex(Vertex v1, Vertex v2) {
			return new PointD ((v1.position.X - v2.position.X) / 2, (v1.position.Y - v2.position.Y) / 2);
		}
	}
}

