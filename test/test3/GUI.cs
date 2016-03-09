using System;
using System.IO;
using Cairo;
using Gtk;
using System.Collections.Generic;
using test3;

/*
 * TODO
 *  debug dijkstra
 * 	dijkstra error if list is null (no vertexes chosen)
 */
class MainClass
{
	static Cairo.Context g, g3;
	static Graph graph;
	static Window w;
	static List<Vertex> vertexes;
	static DrawingArea a;
	//static List<PointD> points;

	public static void drawPoint(PointD point, Context g) {
		g.Arc (point.X, point.Y, 4, 0, Math.PI * 2);
		g.SetSourceColor(new Color(255, 0, 0));
		g.Fill ();
	}

	public static void drawNumber(Vertex vertex, Context g) {
		g.SetSourceRGB (0, 0, 0);
		g.SelectFontFace ("Arial", FontSlant.Normal, FontWeight.Normal);
		g.SetFontSize (14);
		g.MoveTo(new PointD(vertex.position.X - 20, vertex.position.Y - 20));
		g.ShowText(vertex.number.ToString());

	}

	public static void drawLine(Vertex vertex1, Vertex vertex2, Context g) {
		g.MoveTo (vertex1.position);
		g.LineTo (vertex2.position);
		g.Stroke ();
	}
	public static void drawGraph(List<Vertex> vertexes, DrawingArea a, int[,] adjmatrix) {
		g = Gdk.CairoHelper.Create (a.GdkWindow);
		g.SetSourceRGB (255, 255, 255);
		g.Paint ();
		g.SetSourceRGB(0, 0, 0);
		try { 
			g.MoveTo (vertexes [0].position);
		}
		catch (System.ArgumentOutOfRangeException e) {
			
		}
		for (int i = 0; i < vertexes.Count; i++) {
			for (int j = 0; j < vertexes.Count; j++) {
				if (adjmatrix [i, j] == 1) {
					drawLine (vertexes [i], vertexes [j], g);
				}
			}
		}

		foreach (Vertex vert in vertexes) {
			drawPoint (vert.position, g);
		}
		foreach (Vertex vert in vertexes) {
			drawNumber (vert, g);
		}
		g.GetTarget ().Dispose ();
		g.Dispose ();
	}
	static Widget labelBox(string labelText) {
		HBox box = new HBox (false, 0);
		box.BorderWidth = 2;

		Label label = new Label (labelText);
		box.PackStart(label, true, true, 3);

		label.Show();
		return box;
	}

	static Widget drawingAreaBox() {
		
		VBox vbox = new VBox (false, 0);
		a = new DrawingArea ();
		a.AddEvents ((int)Gdk.EventMask.ButtonPressMask);
		a.ButtonPressEvent += buttonPressed;
		vbox.PackStart (a, true, true, 3);
		a.Show();
		return vbox;
	}
	static void clearButtonPressed(object obg, EventArgs args){
		vertexes.Clear ();
		graph.vertexes.Clear ();
		Vertex.nextInt = 1;
		g3 = Gdk.CairoHelper.Create (a.GdkWindow);
		g3.SetSourceRGB (255, 255, 255);
		g3.Paint ();
		g3.GetTarget ().Dispose ();
		g3.Dispose ();
	}
	static void buttonPressed(object obj, ButtonPressEventArgs args) {
		//Console.WriteLine ("button pressed");
		vertexes.Add(new Vertex(new PointD(args.Event.X, args.Event.Y)));
	}
	static void callback(object obj, EventArgs args) {
		//Console.WriteLine ("ccc");
		graph = new Graph(vertexes);
		drawGraph (vertexes, a, graph.adjmatrix);

	}

	static void dfsPressed(object obj, EventArgs args) {
		List<Vertex> arrVerts = new List<Vertex> (graph.DFS ());
		Context g2 = Gdk.CairoHelper.Create (a.GdkWindow);
		int i = 1;
		foreach (Vertex vert in arrVerts) {
			g2.MoveTo (vert.position);
			g2.SetSourceRGB (0, 100, 100);
			g2.Arc (vert.position.X, vert.position.Y, 10, 0, 2 * Math.PI);
			g2.Fill ();

		}
		foreach (Vertex vert in arrVerts) {
			g2.SetSourceRGB (0, 0, 0);
			g2.SelectFontFace ("Arial", FontSlant.Normal, FontWeight.Normal);
			g2.SetFontSize (12);
			g2.MoveTo (vert.position);
			g2.ShowText (i.ToString ());
			i++;
		}
		g2.GetTarget ().Dispose ();
		g2.Dispose ();
	}
	static void bfsPressed(object obg, EventArgs args) { //BFS button event method.
		List<Vertex> arrVerts = new List<Vertex>(graph.BFS()); //get list of arranged vertexes from Graph class
		Context g1 = Gdk.CairoHelper.Create (a.GdkWindow); //create new context to draw on
		int i = 1; //counter to show vertexes order on the screen
		foreach (Vertex vert in arrVerts) {
			//draw a filled blue circle on a place of each vertex
			g1.MoveTo (vert.position);
			g1.SetSourceRGB (0, 100, 100);
			g1.Arc (vert.position.X, vert.position.Y, 10, 0, 2 * Math.PI);
			g1.Fill ();
		}
		foreach (Vertex vert in arrVerts) {
			//showing counters of order 
			g1.SetSourceRGB (0, 0, 0);
			g1.SelectFontFace ("Arial", FontSlant.Normal, FontWeight.Normal);
			g1.SetFontSize (12);
			g1.MoveTo (vert.position);
			g1.ShowText (i.ToString ());
			i++;
		}
		g1.GetTarget ().Dispose ();
		g1.Dispose ();
	}
	static void Main ()
	{
		vertexes = new List<Vertex>();
		Application.Init ();
		w = new Window ("Test Window");
		w.SetDefaultSize (500, 700);
		//we need boxes to put all of the widgets to the window
		VBox vbox = new VBox (false, 0);
		HBox hbox = new HBox(false, 0);
		vbox.BorderWidth = 2;
		//create our buttons
		Button build = new Button ("Build graph");
		Button bfs = new Button ("BFS");
		Button dfs = new Button ("DFS");
		Button clear = new Button ("Clear all");
		Button open = new Button ("Open");
		Button save = new Button ("Save");
		Button dijkstra = new Button ("Dijkstra");
		//add clicking events
		save.Clicked += Save_Clicked;
		open.Clicked += openClicked;
		bfs.Clicked += bfsPressed;
		clear.Clicked += clearButtonPressed;
		build.Clicked += callback;
		dfs.Clicked += dfsPressed;
		dijkstra.Clicked += Dijkstra_Clicked;
		//pack all the buttons to the box
		hbox.PackStart (build, false, false, 3);
		hbox.PackStart (clear, false, false, 3);
		hbox.PackStart (bfs, false, false, 3);
		hbox.PackStart (dijkstra, false, false, 3);
		hbox.PackStart (dfs, false, false, 3);
		hbox.PackStart (open, false, false, 3);
		hbox.PackStart (save, false, false, 3);
		//creating drawing area box
		Widget dbox = drawingAreaBox ();
		dbox.Show ();
		hbox.Show ();
		vbox.PackStart (hbox, false, false, 3);
		vbox.PackStart (dbox, true, true, 2);
		vbox.Show ();
		w.Add(vbox);
		//add closing window event
		w.DeleteEvent += close_window;
		w.ShowAll ();
		Application.Run ();
	}
	/*
	 * Save graph to file and open graph from file  methods.
	 * File looks like 
	 * 0 1 0 0 0 
	 * 1 0 0 1 1 
	 * 0 0 0 0 1 
     * 0 1 0 0 1 
	 * 0 1 1 1 0 
	 * 86, 139
 	 * 214, 295
	 * 203, 433
	 * 351, 364
	 * 207, 131
	 * 
	 * Adjacency matrix first, coordinates second. Everything needs to be separated with white spaces or commas.
	 */
	static void openClicked (object sender, EventArgs e)
	{
		//New object invoked by our button - choosing dialog. We set FileChooser action as "Open", which is obvious.
		var chooser = new FileChooserDialog ("Choose the file to open", 
			              w, FileChooserAction.Open,
			              "Open", ResponseType.Accept,
			              "Cancel", ResponseType.Cancel); 
		if (chooser.Run () == (int)ResponseType.Accept) {
			string fileContent = File.ReadAllText (chooser.Filename); //read all text from file
			//split it to strings
			string[] integerStrings = fileContent.Split (new char[] { ' ', '\t', '\r', '\n', ',' }, StringSplitOptions.RemoveEmptyEntries);
			int[] integers = new int[integerStrings.Length];
			//make our strings integer
			for (int n = 0; n < integerStrings.Length; n++)
				integers [n] = int.Parse (integerStrings [n]);
			//we find the size of the adjacency matrix
			int vertsNumber = 0;
			for (int i = 0; i < integers.Length; i++)
				if (integers [i] == 0 || integers [i] == 1)
					vertsNumber++;
			vertsNumber = (int)Math.Sqrt ((double)vertsNumber);
			//clear our vertex list to avoid errors if smth was put in before opening file
			vertexes.Clear ();
			graph = new Graph (vertexes);
			//set vertex first id to 1
			Vertex.nextInt = 1;
			graph.adjmatrix = new int[vertsNumber, vertsNumber];
			//we fill in graph adjacency matrix from file
			for (int i = 0; i < integers.Length;) {
				if (integers [i] == 0 || integers [i] == 1) {
					graph.adjmatrix [i % vertsNumber, i / vertsNumber] = integers [i];
					i++;
				} else {
					vertexes.Add (new Vertex (new PointD ((double)integers [i], (double)integers [i + 1]))); //and add vertexes to the list by their coordinates
					i += 2;
				}
			}
			graph.vertexes = new List<Vertex> (vertexes);
			drawGraph (vertexes, a, graph.adjmatrix); 
		}
		chooser.Destroy ();
	}

	static void Save_Clicked (object sender, EventArgs e)
	{
		var chooser = new FileChooserDialog ("Choose the path to save:", 
			w, FileChooserAction.Save,
			"Save", ResponseType.Accept,
			"Cancel", ResponseType.Cancel);
		if (chooser.Run () == (int)ResponseType.Accept) {
			//we put everything to the string first
			string output = "";
			for (int i = 0; i < graph.vertexes.Count; i++) {
				for (int j = 0; j < graph.vertexes.Count; j++) {
					output += graph.adjmatrix [i, j].ToString () + " ";
				}
				output += "\n";
			}
			for (int i = 0; i < graph.vertexes.Count; i++) {
				output += graph.vertexes [i].position.X.ToString () + " " + graph.vertexes [i].position.Y.ToString () + "\n";
			}
			//then add all the string to file. this method opens file, writes a strig, and closes the file.
			File.WriteAllText (chooser.Filename, output);
		}
		chooser.Destroy ();
	}
	/* 
	 * Method for Dijkstra button click event.
	 * 
	 */
	static void Dijkstra_Clicked (object sender, EventArgs e)
	{
		Context g4 = Gdk.CairoHelper.Create (a.GdkWindow);
		g4.SetSourceRGB (255, 0, 0);
		Dijkstra d = new Dijkstra (graph);
		List<Vertex> path = d.getMinWay ();
		g4.MoveTo (path [0].position);
		foreach (Vertex v in path) {
			g4.LineTo (v.position);
		}
		g4.Stroke ();
		g4.GetTarget ().Dispose ();
		g4.Dispose ();
	}

	static void close_window(object o, DeleteEventArgs args) {
		Application.Quit();
		return;
	}
}