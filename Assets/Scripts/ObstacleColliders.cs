using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleColliders : MonoBehaviour {

	protected BoundingBox objCollider;
	protected List<Vector2> vertex;

	//	public ObstacleColliders ( List<Vector2> Vertex ) {
	//
	//		collider = new BoundingBox (Vertex);
	//		vertex = Vertex;
	//
	//	}

	public void updateVertex ( List<Vector2> Vertex ) {

		vertex = Vertex;
		objCollider = new BoundingBox(Vertex);

	}

}
