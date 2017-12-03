using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path {

	public List<Vector2> Nodes;
	private List<Vector2> pathDirections;
	private List<float> accDistance;

	private float totalDistance;

	public Path (List<Vector2> nodes) {
		Nodes = nodes;
		pathDirections = new List<Vector2> ();
		accDistance = new List<float> ();
		BuildPath ();
	}

	private void BuildPath (){

		Vector2 pathVector;
		float currentSum = 0;

		for (int i = 0; i < Nodes.Count - 1; i++) {

			pathVector = (Nodes [i + 1] - Nodes [i]);

			currentSum += pathVector.magnitude;
			accDistance.Add(currentSum);

			pathDirections.Add(pathVector.normalized);
		}

		for (int i = 0; i < Nodes.Count - 1; i++)
			accDistance [i] = accDistance [i] / currentSum;

		totalDistance = currentSum;

	}

	private int BinarySearch( float lastParam ) {

		lastParam = Mathf.Clamp (lastParam, 0f, 1f);

		int hi = accDistance.Count - 1;
		int lo = 0;
		int mid;

		while ( lo < hi ){

			mid = ( lo + hi ) / 2;

			if (accDistance [mid] >= lastParam)
				hi = mid;
			else
				lo = mid + 1;

		}

		if (accDistance [lo] >= lastParam && lo != 0)
			lo--;

		return lo;
			
	}

	public float getParam( Vector2 currentPosition, float lastParam ){

		lastParam = Mathf.Clamp (lastParam, 0f, 1f);

		int lastNode = BinarySearch (lastParam);
		MonoBehaviour.print (pathDirections);
		MonoBehaviour.print (lastNode);

		float dist = Vector2.Dot (currentPosition - Nodes [lastNode], pathDirections [lastNode]);

		dist = Mathf.Abs (dist) / totalDistance + accDistance [lastNode];

		return Mathf.Clamp (dist, 0f, 1f);

	}

	public Vector2 getPosition( float param ){
		/*
		param = Mathf.Clamp (param, 0f, 1f);

		int lastNode = BinarySearch (param);

		float dist = totalDistance * (param - accDistance [lastNode]);

		return pathDirections [lastNode] * dist + Nodes [lastNode];
		*/
		param = Mathf.Clamp (param, 0f, 1f);

		int lastNode = BinarySearch (param);

		float dist = totalDistance * (param - accDistance [lastNode]);

		return Nodes[lastNode];



	}

}

