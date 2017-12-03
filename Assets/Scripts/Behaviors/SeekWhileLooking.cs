using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class SeekWhileLooking : Behaviour {

	public SeekWhileLooking () : base ("Seek while looking") {}
	public SeekWhileLooking( AgentMeta target, AgentMeta character ) : base( "Seek while looking", target, character ) {}

	public override SteeringOutput.SteeringOutput getSteering(){

		Behaviour seek = new Seek (Target, Character);

		Behaviour lwyg = new LWYG (Character);
		SteeringOutput.SteeringOutput steering = lwyg.getSteering () + seek.getSteering();

		return steering;

	}

}
