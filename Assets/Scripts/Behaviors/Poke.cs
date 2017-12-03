using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Poke : Behaviour {

	private float pokeDistance;

	public Poke () : base ("Poke") {}
	public Poke ( AgentMeta target, AgentMeta character, float pokeRadius) 
		: base( "Poke", target, character ) {

		pokeDistance = pokeRadius;
	}

	public override SteeringOutput.SteeringOutput getSteering(){

		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput( new Vector2( 0.0f, 0.0f), 0.0f );
		Behaviour selectedBehaviour;

		float distance = (Target.position - Character.position).magnitude;

		if (distance < pokeDistance)
			selectedBehaviour = new Flee (Target, Character);
		else
			selectedBehaviour = new SeekWhileLooking (Target, Character);

		steering = selectedBehaviour.getSteering ();
		return steering;

	}

}
