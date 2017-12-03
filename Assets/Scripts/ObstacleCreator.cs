using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour {
	public BoundingBox bbx;
	public List<Vector3> vertices;
	LineRenderer lineRender;
	void Start () {
		SpriteRenderer sp = gameObject.GetComponent<SpriteRenderer> ();
		vertices = getVertices (sp);
		Debug.Log ("Mode: " + sp.drawMode + "Bounds: " + sp.bounds.ToString());
		List<Vector2> vertices1 = vector3To2 (vertices);
		bbx = new BoundingBox (vertices1);
		/* Mostrar cositas */
		lineRender = gameObject.AddComponent<LineRenderer> ();
		lineRender.positionCount = vertices.Count;
		lineRender.startWidth = 0.1f;
		lineRender.endWidth = 0.1f;
		lineRender.startColor = Color.green;
		lineRender.endColor = Color.green;
		Material mat = new Material (Shader.Find ("Unlit/Texture"));	// Solo lo usamos para poder mostrar el fcking color.
		lineRender.material = mat;
		lineRender.sortingLayerName = "Proyectil";
		lineRender.useWorldSpace = true;
		//lineRender.SetPositions (vertices.ToArray());
	}

	void Update(){
		LineRenderer lineRenderer = GetComponent<LineRenderer> ();
		Vector3[] vectors = vertices.ToArray ();

		Debug.Log ("Numero: "+vertices.Count+ vectors.ToString());
		lineRenderer.SetPositions (vectors);
	}

	public List<Vector3> getVertices(SpriteRenderer sp)
	{
		List<Vector3> result = new List<Vector3> ();
		Vector3 [] array = SpriteLocalToWorld(sp);
		for(int i = 0; i<2; i++){
			for(int j = 0 ; j<2;j++){
				result.Add(new Vector3 (array [i].x, array [j].y,-1));
			}
		}
		Vector3 aux = result [2];
		result [2] = result [3];
		result [3] = aux;
		return result;
	}

	List<Vector2> vector3To2(List<Vector3> vectors){
		List<Vector2> result = new List<Vector2> ();
		foreach (Vector3 v in vectors) {
			result.Add (new Vector2 (v.x, v.y));
		}
		return result;
	}

	Vector3[] SpriteLocalToWorld(SpriteRenderer sp) 
	{
		Vector3 pos = transform.position;
		Vector3 [] array = new Vector3[2];
		array[0] = pos + sp.bounds.extents; // Bottom Left (?)
		array[1] = pos - sp.bounds.extents; // Top Right (?)
		return array;
	}

}
