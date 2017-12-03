﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class EvadeWhileLooking : Behaviour {

	public EvadeWhileLooking () : base ("Evade") {}
	public EvadeWhileLooking ( AgentMeta target, AgentMeta character, float MaxPrediction ) 
		: base ( "Evade", target, character, MaxPrediction) {}

	public override SteeringOutput.SteeringOutput getSteering(){

		Vector2 direction = Target.position - Character.position;
		float distance = direction.magnitude;

		float speed = Character.velocity.magnitude;

		float prediction;
		if (speed <= distance / maxPrediction)
			prediction = maxPrediction;
		else
			prediction = distance / maxPrediction;

		GameObject dummy = (GameObject) MonoBehaviour.Instantiate (Resources.Load ("Prefab/Dummy"));
		AgentMeta dummyAgent = dummy.GetComponent<AgentMeta> ();
		dummyAgent.position = Target.position + Target.velocity * prediction;

		Behaviour flee = new Flee( dummyAgent, Character);

		SteeringOutput.SteeringOutput steering = flee.getSteering();

		MonoBehaviour.Destroy (dummy);

		Behaviour lwyg = new LWYG (Character);
		steering += lwyg.getSteering ();

		return steering;

	}

}
