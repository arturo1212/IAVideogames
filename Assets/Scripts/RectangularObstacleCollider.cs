using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangularObstacleCollider : ObstacleColliders {

	public float height;
	public float width;
	public bool drawBox;
	private Vector2 center;

	//public RectangleObstacleCollider ( float Height, float Wide, Vector2 Center ) 
	//: base( new List<Vector2> { new Vector2( Center.x - wide/2, Center.y 

	void Start() {

		center = transform.position;
		MonoBehaviour.print (center);

		vertex = new List<Vector2> { new Vector2 (center.x - width / 2, center.y - height / 2),
			new Vector2 (center.x + width / 2, center.y - height / 2),
			new Vector2 (center.x + width / 2, center.y + height / 2),
			new Vector2 (center.x - width / 2, center.y + height / 2)
		};

		objCollider = new BoundingBox (vertex);


			objCollider.drawForm (Color.green);
			//GUI.Box( new Rect( vertex[0], new Vector2( width, height) ), );

	}
//		Sprite sp = GetComponent<SpriteRenderer> ().sprite;


}
