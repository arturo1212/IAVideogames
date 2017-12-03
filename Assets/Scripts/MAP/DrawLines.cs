using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DrawLines : MonoBehaviour {
	public int selGridInt = 0;
	public bool hecho = false;
	List<Vector3> vertices2D = new List<Vector3>();
	public struct triangle
	{
		public Vector3 v1,v2,v3;
		public triangle(Vector3 v11,Vector3 v22,Vector3 v33)
		{
			v1 = v11;
			v2 = v22;
			v3 = v33;
		}
	}
	public List<triangle> triangles = new List<triangle>();

	void OnGUI() 
	{
		if (!hecho) {
			List<Vector3> append = new List<Vector3> ();
			vertices2D = genPoints (new Vector2 (10, 10), 3);
			for (int i = 0; i < vertices2D.Count - 2; i++) {
				List<Vector3> closest = getClosestPoints (vertices2D [i], vertices2D, 2);
				DrawTriangle (vertices2D [i], closest[0], closest[1], Color.white, 2000);
				triangles.Add (new triangle (vertices2D [i], closest [0], closest [1]));
				append.Add ((closest[0]+closest[1])/2);
				append.Add ((vertices2D[i]+closest[1])/2);
				append.Add ((vertices2D[i]+closest[0])/2);
			}
			foreach (Vector3 v in append) {
				vertices2D.Add (v);
			}

			hecho = true;
		}
	}
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	public GameObject particle;
	void Update(){

		if (Input.GetButtonDown ("Fire1")) {
			Vector2 p1 = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			List<Vector3> closest = getClosestPoints (p1, vertices2D, 2);
			DrawTriangle (p1, closest[0], closest[1], Color.white, 2000);
			DrawLine((Vector3)p1, new Vector3(p1.x + 0.1f,p1.y + 0.1f), Color.red);
			vertices2D.Add (p1);
			Debug.Log (p1);
		}
	}
	System.Random r = new System.Random();
	float rnd( float a, float b )
	{
		double value = a + r.NextDouble()*(b-a);
		return Convert.ToSingle(value);
	}

	List<Vector3> genPoints(Vector2 dimensions, int capas)
	{
		float paso = dimensions.x / capas;
		List<Vector3> result = new List<Vector3> ();
		for (int i = 0; i < dimensions.x; i+=2) 
		{
			result.Add(new Vector3(i, dimensions.y, 0));
			result.Add(new Vector3(dimensions.x, i, 0));
			result.Add(new Vector3(i, -dimensions.y, 0));
			result.Add(new Vector3(-dimensions.x, i, 0));
			result.Add(new Vector3(0, 0, 0));
		}
		for (float i = -dimensions.x; i < dimensions.x; i += paso) 
		{
			for (float j = -dimensions.y; j < dimensions.y; j += paso) {
				float y = rnd (0, paso);
				float x = rnd (0, paso);
				result.Add (new Vector3 (i + x, j + y, 0));
			}
		}
		return result;
	}

	float distBetweenPoints(Vector3 p1, Vector3 p2)
	{
		double value = Math.Sqrt (Math.Pow (p1.x - p2.x, 2) + Math.Pow (p1.y - p2.y, 2));
		return Convert.ToSingle(value);
	}

	List<Vector3> getClosestPoints(Vector3 p1, List<Vector3> points, int winSize)
	{
		List<Vector3> result = new List<Vector3> ();	// Puntos cercanos.
		foreach (Vector3 pt in points) 					
		{
			if (pt == p1) 					// He aqui la distancia de tu alma.
			{
				continue;
			}
			if (result.Count < winSize)		// Por algo se empieza, no? 
			{  
				result.Add (pt);
			} 
			else 
			{
				/* Hallar el menor de los puntos actuales.*/
				float max = 0;						
				int index = -1;
				for(int i= 0; i<result.Count;i++) 
				{
					if (distBetweenPoints(p1, result [i]) > max && !result.Contains(p1)) 
					{
						max = distBetweenPoints (p1, result [i]);
						index = i;
					}
				}
				/* Ver si soy menor que el mayor*/
				float dist = distBetweenPoints (p1, pt);
				if (dist < max && index > -1) 
				{
					result [index] = pt;
				}
			}
		}
		return result;
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









/*
	List<Vector3> topPoints(Vector2 dimensions, int columns)
	{
		List<Vector3> result = new List<Vector3> ();
		result.Add(new Vector3(dimensions.x, dimensions.y, 0));
		result.Add(new Vector3(-dimensions.x, dimensions.y, 0));
		for (float i = -dimensions.x; i < dimensions.x; i += 2) 
		{
			float x = rnd (-dimensions.x, dimensions.x);
			result.Add(new Vector3(x, dimensions.y, 0));
		}
	}

	List<Vector3> completePoints(Vector2 dimensions, List<Vector3> points, int winSize)
	{
		
	}
	*/
