using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : Forms {

	private float orientation;

	public BoundingBox ( List<Vector2> Vertex ) : base( Vertex ) {}

	public bool rayCollides ( Vector2 startPoint, Vector2 endPoint ) {

		bool collides = false;
		bool condMayor = true;
		bool condMenor = true;
		Vector2 auxStartVector;
		Vector2 auxEndVector;

		float m;	// Pendiente de la recta
		float b;	// Punto de corte

		// Determinamos si la la recta puede pasar por el rectángulo. Si los puntos que forman el vector
		// no poseen al menos una COORDENADA de un vertice entre ellos, no PUEDE estar dentro del rectángulo.
		for (int i = 0; (i < countVertex) && !collides ; i++) {
			auxStartVector = (startPoint - vertex [i]);
			auxEndVector = (endPoint - vertex [i]);
			collides = collides || ( auxStartVector.x * auxEndVector.x <= 0) || (auxStartVector.x * auxEndVector.x <= 0);
		}

		// Si no se cumple la condición mínma para existir colisión, salgo.
		if (!collides)
			return false;

		// Si alguno de los puntos pertenece al polígono, hay colisión.
		if (belongsTo (startPoint) || belongsTo (endPoint))
			return true;

		// Determino la ecuación de la recta
		m = (endPoint.y - startPoint.y) / (endPoint.x - startPoint.x);
		b = -m * startPoint.x + startPoint.y;

		// Determino si todos los vértices pertenecen a la región mayor o menor a la recta
		// Si al menos uno de los puntos no pertenece a la región, el bool se torna falso.

		// Nota: Se utilizan los dos dado que son opuestos. Si todos están en una región
		// puede generar false. De esta forma ambas deben ser false para poder existir colisión.
		for (int i = 0; i < countVertex; i++) {
			condMayor = condMayor && ( vertex[i].y > m * vertex[i].x + b);
			condMenor = condMayor && ( vertex[i].y < m * vertex[i].x + b);
		}

		// Si se cumple la condición anterior, existe colisión.
		collides = collides && !condMayor && !condMenor;

		return collides;
	}

}
