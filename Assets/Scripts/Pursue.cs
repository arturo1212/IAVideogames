using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Pursue : Behaviour {

	public Pursue () : base ("Pursue") {}
	public Pursue ( AgentMeta target, AgentMeta character, float MaxPrediction ) 
		: base ( "Pursue", target, character, MaxPrediction) {}

	public override SteeringOutput.SteeringOutput getSteering(){

		Vector2 direction = Target.position - Character.position;
		float distance = direction.magnitude;

		float speed = Character.velocity.magnitude;

		float prediction;
		if (speed <= distance / maxPrediction)
			prediction = maxPrediction;
		else
			prediction = distance / maxPrediction;

		AgentMeta dummyAgent = new AgentMeta ();
		dummyAgent.position = Target.position + Target.velocity * prediction;
		Behaviour seek = new Seek( dummyAgent, Character);
		SteeringOutput.SteeringOutput steering = seek.getSteering();

		/* Limpiar y devolver resultados */
		MonoBehaviour.Destroy (dummyAgent);		
		return steering;
	}

}
