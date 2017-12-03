using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class PredictivePathFollowing : Behaviour {

	private Path path;
	private float PathOffset;
	private float currentParam;

	private float predictTime;

	public PredictivePathFollowing ( AgentMeta character, List<Vector2> nodes, float pathOffset, float PredictTime) 
		: base("Predictive Path Following") {

		Character = character;
		path = new Path (nodes);
		PathOffset = pathOffset;
		currentParam = 0.0f;
		predictTime = PredictTime;

	}

	public override SteeringOutput.SteeringOutput getSteering() {

		Vector2 futurePos = Character.position + Character.velocity * predictTime;

		currentParam = path.getParam (futurePos, currentParam);

		float targetParam = currentParam + PathOffset;

		GameObject dummy = (GameObject) MonoBehaviour.Instantiate (Resources.Load ("Prefab/Dummy"));
		AgentMeta dummyAgent = dummy.GetComponent<AgentMeta> ();
		dummyAgent.position = path.getPosition (targetParam);

		Behaviour seek = new Seek (dummyAgent, Character);
		SteeringOutput.SteeringOutput steering = seek.getSteering ();

		MonoBehaviour.Destroy (dummy);
		return steering; 

	}
}

