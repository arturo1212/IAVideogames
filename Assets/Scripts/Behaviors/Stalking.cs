using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Stalking : Behaviour {

	private float stalkRadius;

	public Stalking () : base ("Stalk") {}
	public Stalking ( AgentMeta target, AgentMeta character, float SlowRadius, float TargetRadius, float TimeToTarget, float StalkRadius) 
		: base( "Stalk", target, character, SlowRadius, TargetRadius, TimeToTarget ) {

		stalkRadius = StalkRadius;
	}

	public override SteeringOutput.SteeringOutput getSteering(){

		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput( new Vector2( 0.0f, 0.0f), 0.0f );
		Behaviour selectedBehaviour;

		float orientationDiff = Mathf.Abs (Target.orientation - Character.orientation - 180f);

		if (orientationDiff < stalkRadius)
			selectedBehaviour = new FleeWhileLooking (Target, Character);
		else
			selectedBehaviour = new ArriveWhileLooking (Target, Character, 3f, 1f, .1f);

		steering = selectedBehaviour.getSteering ();
		return steering;

	}

}
