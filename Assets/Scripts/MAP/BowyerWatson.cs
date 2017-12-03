using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BowyerWatson : MonoBehaviour {
	public class triangle
	{
		public Vector3 v1,v2,v3,center;
		public triangle(Vector3 v11,Vector3 v22,Vector3 v33)
		{
			v1 = v11;
			v2 = v22;
			v3 = v33;
		}
	}
		
	public List<triangle> triangles = new List<triangle>();
	public bool hecho = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI() 
	{
		if (!hecho) {
			Vector3 tl = new Vector3 (-10f, 10f, 0f);
			Vector3 br = new Vector3 (10f, -10f, 0f);
			List<triangle> list_ini = squareToTriangle (tl, br);
			int iter = 0;
			while(list_ini.Count>0){
				triangle tr = list_ini [0];
				DrawTriangle(tr.v1,tr.v2, tr.v3, Color.white, 200000f); 
				if (iter<40)
				{
					List<triangle> new_trs = splitTriangle (tr);
					foreach (triangle trr in new_trs) {
						list_ini.Add (trr);
					}
					iter++;
				}
				list_ini.RemoveAt(0);
			}
			hecho = true;
		}

	}
	/*--------------------------------- GRAFO --------------------------------- */
	public class graph
	{	// En la Z se almacena el costo.
		public Dictionary<Vector3,List<Vector3>> connections = new Dictionary<Vector3,List<Vector3>>();
	}
	public bool pointInLine(Vector3 p1, Vector3 p2, Vector3 p3, float threshold)
	{
		bool cond1 = distBetweenPoints (p1, p2) + distBetweenPoints (p1, p3) - distBetweenPoints (p2, p3) <= threshold;
		return cond1;
	}

	public graph createGraph()
	{
		graph grafo = new graph ();
		foreach (triangle t1 in triangles) 
		{
			foreach (triangle t2 in triangles) 
			{
				if (t1 != t2)
				{
					bool found = false;
					Vector3[] points = { t1.v1, t1.v2, t1.v3 };
					foreach (Vector3 p in points) 
					{
						bool cond1 = pointInLine (p, t2.v1, t2.v3, 0.001f);
						bool cond2 = pointInLine (p, t2.v2, t2.v3, 0.001f);
						bool cond3 = pointInLine (p, t2.v1, t2.v2, 0.001f);
						if (cond1 || cond2 || cond3) {
							found = true;
						}
					}
					if (found) 
					{
						float peso = distBetweenPoints (t1.center, t2.center);
						Vector3 aux = t2.center;
						aux.z = peso;
						if (grafo.connections.ContainsKey (t1.center))
						{

							grafo.connections [t1.center].Add (aux);
						} 
						else 
						{
							grafo.connections [t1.center] = new List<Vector3> ();
							grafo.connections [t1.center].Add (aux);
						}
					}
				}
			}
		}
		return grafo;
	}

	/* ------------------------------ TRIANGULOS ------------------------------ */
	public Vector3 getMidPoint(triangle t1)
	{
		Vector3[] options = { t1.v1, t1.v2, t1.v3 };
		Vector3 sum = t1.v1 + t1.v2 + t1.v3;
		Vector3 mass_center = new Vector3 (sum.x / 3, sum.y / 3, 0);
		return mass_center;
	}

	float sign (Vector3 p1, Vector3 p2, Vector3 p3)
	{
		return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
	}

	bool PointInTriangle (Vector3 pt, Vector3 v1, Vector3 v2, Vector3 v3)
	{
		bool b1, b2, b3;

		b1 = sign(pt, v1, v2) < 0.0f;
		b2 = sign(pt, v2, v3) < 0.0f;
		b3 = sign(pt, v3, v1) < 0.0f;

		return ((b1 == b2) && (b2 == b3));
	}

	public Vector3 bestVertex(triangle t1){
		Vector3[] options = { t1.v1, t1.v2, t1.v3 };
		Vector3 mass_center = getMidPoint (t1);
		Vector3 actual = new Vector3 ();
		float min = 10000000f;
		foreach (Vector3 v in options) 
		{
			if (distBetweenPoints (v, mass_center) < min) 
			{
				actual = v;
				min = (distBetweenPoints (v, mass_center));
			}
		}
		return actual;
	}

	private float Area (triangle t1) {
		Vector3[] m_points = { t1.v1, t1.v2, t1.v3 };
		int n = m_points.Length;
		float A = 0.0f;
		for (int p = n - 1, q = 0; q < n; p = q++) {
			Vector2 pval = m_points[p];
			Vector2 qval = m_points[q];
			A += pval.x * qval.y - qval.x * pval.y;
		}
		return (A * 0.5f);
	}

	public List<triangle> splitTriangle(triangle t1)
	{
		// Seleccionar un vertice
		System.Random rand = new System.Random ();
		int choice = rand.Next (0, 3);
		Vector3[] options = { t1.v1, t1.v2, t1.v3 };
		Vector3 main_vertex = bestVertex (t1);
		// Encontrar centro entre los otros dos vertices
		List<Vector3> free = new List<Vector3>();
		foreach(Vector3 v in options) {
			if (v != main_vertex) 
			{
				free.Add (v);
			}
		}
		Vector3 dir = (free [0] - free [1]).normalized;
		Vector3 mid = new Vector3 ((free [0].x + free [1].x) / 2, (free [0].y + free [1].y) / 2, 0);
		mid = mid + dir * rnd (-distBetweenPoints(free[0],free[1])/4, distBetweenPoints(free[0],free[1])/4);
		triangle t2 = new triangle (main_vertex, mid, free [0]);
		triangle t3 = new triangle (main_vertex, mid, free [1]);
		t2.center = getMidPoint (t2);
		t3.center = getMidPoint (t3);
		return new List<triangle> { t2, t3 };
	}

	/* ---------------------------------- CUADRADOS ---------------------------------- */
	public List<triangle> squareToTriangle(Vector3 tl, Vector3 br)
	{
		Vector3 top_r = new Vector3 (br.x, tl.y, 0);
		Vector3 bot_l = new Vector3 (tl.x, br.y, 0);
		triangle t1 = new triangle (tl, br, top_r);
		triangle t2 = new triangle (tl, br, bot_l);
		t1.center = getMidPoint (t1);
		t2.center = getMidPoint (t2);
		List<triangle> triangs = new List<triangle> {t1, t2};
		return triangs;
	}

	/*---------------------------------- POINTS --------------------------------------- */
	float distBetweenPoints(Vector3 p1, Vector3 p2)
	{
		double value = Math.Sqrt (Math.Pow (p1.x - p2.x, 2) + Math.Pow (p1.y - p2.y, 2));
		return Convert.ToSingle(value);
	}


	/*------------------------------- STOCASTICS -------------------------------------- */
	System.Random r = new System.Random();
	float rnd( float a, float b )
	{
		double value = a + r.NextDouble()*(b-a);
		return Convert.ToSingle(value);
	}


	/*--------------------------------- DIBUJOS --------------------------------------- */
	void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 10f)
	{
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.SetColors(color, color);
		lr.SetWidth(0.1f, 0.1f);
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
		lr.sortingLayerName = "Player";
		//GameObject.Destroy(myLine, duration);
	}

	void DrawTriangle(Vector3 p1, Vector3 p2, Vector3 p3, Color color, float duration = 10f)
	{
		DrawLine(p1, p2, color);
		DrawLine(p2, p3, color);
		DrawLine (p3, p1, color);
	}

}
