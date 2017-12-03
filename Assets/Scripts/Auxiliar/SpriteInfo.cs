using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Sprite sp = gameObject.GetComponent<SpriteRenderer> ().sprite;
		Vector3 [] array = SpriteLocalToWorld(sp);
		Debug.Log ("Extremos:"+ array[0]+" " + array[1]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public List<Vector2> getVertices(Sprite sp)
	{
		List<Vector2> result = new List<Vector2> ();
		Vector3 [] array = SpriteLocalToWorld(sp);
		for(int i = 0; i<2; i++){
			for(int j = 0 ; j<2;j++){
				result.Add(new Vector2 (array [i].x, array [j].y));
			}
		}
		return result;
	}

	Vector3[] SpriteLocalToWorld(Sprite sp) 
	{
		Vector3 pos = transform.position;
		Vector3 [] array = new Vector3[2];
		array[0] = pos + sp.bounds.min; // Bottom Left (?)
		array[1] = pos + sp.bounds.max; // Top Right (?)
		return array;
	}
}
