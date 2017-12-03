using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class PathFollowing : Behaviour {

	private Path path;
	private float PathOffset;
	private float currentParam;
	int i = 0;

	public PathFollowing ( AgentMeta character, List<Vector2> nodes, float pathOffset) 
		: base("Path Following") {

		Character = character;
		path = new Path (nodes);
		PathOffset = pathOffset;
		currentParam = 0.0f;

	}

	public override SteeringOutput.SteeringOutput getSteering() {
		GameObject dummy = (GameObject) MonoBehaviour.Instantiate (Resources.Load ("Prefab/Dummy"));
		AgentMeta dummyAgent = dummy.GetComponent<AgentMeta> ();
		if ((Character.position - ((Vector3)path.Nodes[i])).magnitude < 1) 
		{
			i++;
			if (path.Nodes.Count == i) {
				i = 0;
			}
			Debug.Log ("NODE: " + path.Nodes [i].ToString());
		}

		dummyAgent.position = path.Nodes[i];
		//MonoBehaviour.print(dummyAgent.getPosition ());

		Behaviour seek = new ArriveWhileLooking (dummyAgent, Character,1f,0.5f,0.1f);
		SteeringOutput.SteeringOutput steering = seek.getSteering ();

		MonoBehaviour.Destroy (dummy);
		return steering; 

	}
}

