using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class geometry {
	public struct line
	{
		public Vector2 p1;
		public float m;
		public float b;
	}

	public static bool insideCone(Vector2 pos1, Vector2 pos2, int orientation, float radius = 6, float angle_threshold = 45)
	{
		/* 	Funcion que indica si un punto esta dentro de un cono de medidas conocidas 
			Se utiliza para determinar si el centro de masa del objetivo esta en el interior del 
			cono. */
		float orientation1 = orientation * Mathf.Deg2Rad;	// Transformacion de grados a radianes
		Vector2 angle_vector1 = new Vector2((float)Mathf.Cos(orientation1), (float)Mathf.Sin(orientation1));	// Vector de orientacion
		Vector2 angle_vector2 = pos2 - pos1;																	// Vector al target

		/* Informacion del target */
		float distance = Vector2.Distance(pos1, pos2); 					// Distancia entre dos puntos
		float d_angle  = Vector2.Angle(angle_vector1, angle_vector2);	// Variacion del angulo

		/* Verificacion de condiciones */
		bool cond1 = distance <= radius;							// Dentro del radio admitido
		bool cond2 = (d_angle*Mathf.Rad2Deg)  <= angle_threshold;	// Dentro de los lados del cono
		if (cond1 && cond2) { return true; }
		return false;
	}

	public static line createLine(Vector2 p1, Vector2 p2)
	{
		/* 	Ecuacion de la recta: y = m*x + b 
			Basta con devolver m y b. */
		line l1 = new line ();
		l1.m = (p2.y - p1.y) / (p2.x - p1.x);
		l1.b = p1.y - l1.m * p1.x;
		l1.p1 = new Vector2 (p1.x, p1.y);
		return l1;
	}

	public static void intersectLineWCircle(line l1, Vector2 center, float radius)
	{
		Vector2[] result = { new Vector2 (), new Vector2 () };
		float p = center.x;
		float q = center.y;
		float c = l1.p1.y - l1.m*l1.p1.x;
		float A = Mathf.Pow (l1.m, 2) + 1;
		float B = 2 * (l1.m * c - l1.m * q - p);
		float C = Mathf.Pow (q, 2) - Mathf.Pow (radius, 2) + Mathf.Pow (p, 2) - 2 * c * q + Mathf.Pow (c, 2);
		float[] resolv = resolveQuad(A,B,C);
		result[0].x = resolv[0];
		result[1].x = resolv[1];
		result[0].y = l1.m * resolv[0] + c;
		result[1].y = l1.m * resolv[1] + c;
	}

	public static float[] resolveQuad(float A, float B, float C)
	{
		/* Devuelve los valores de x que satisfacen la ecuacion */
		float[] result = { 0, 0 };
		float det = Mathf.Pow (B, 2) - 4 * A * C;
		result[0] = (-B + Mathf.Sqrt (det))/2*A;
		result[1] = (-B - Mathf.Sqrt (det))/2*A;
		return result;
	}
}
