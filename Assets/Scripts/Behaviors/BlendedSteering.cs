using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;
using BehaviourAndWeight;

public class BlendedSteering : Behaviour {

	private List<BehaviourAndWeight.BehaviourAndWeight> weightedBehaviours;

	public BlendedSteering ( AgentMeta character, List<Behaviour> behaviours, List<float> Weights ) : base( "Blended Steering" ) {

		if (behaviours.Count != Weights.Count || behaviours.Count == 0 || Weights.Count == 0)
			return;

		weightedBehaviours = new List<BehaviourAndWeight.BehaviourAndWeight> ();

		for (int i = 0; i < behaviours.Count; i++)
			weightedBehaviours.Add (new BehaviourAndWeight.BehaviourAndWeight (behaviours [i], Weights [i]));

		Character = character;

	}

	public BlendedSteering ( AgentMeta character, List<BehaviourAndWeight.BehaviourAndWeight> behaviourWeighted ) : base( "Blended Steering" ) {

		if (behaviourWeighted.Count == 0)
			return;

		weightedBehaviours = behaviourWeighted;

	}

	public override SteeringOutput.SteeringOutput getSteering(){

		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput (new Vector2 (.0f, .0f), .0f);
		SteeringOutput.SteeringOutput auxSteering;

		for (int i = 0; i < weightedBehaviours.Count; i++) {

			auxSteering = weightedBehaviours [i].behaviour.getSteering ();

			auxSteering.linear.x *= weightedBehaviours [i].weight;
			auxSteering.linear.y *= weightedBehaviours [i].weight;

			auxSteering.angular *= weightedBehaviours [i].weight;

			steering += auxSteering;

		}

		Behaviour lwyg = new LWYG (Character);
		steering += lwyg.getSteering ();

		if (steering.linear.magnitude > Character.maxAcceleration)
			steering.linear = steering.linear.normalized * Character.maxAcceleration;

		if (steering.angular > Character.maxAngularAcceleration)
			steering.angular = Character.maxAngularAcceleration;

		return steering;

	}

}
