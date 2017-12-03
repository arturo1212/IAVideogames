using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Flee : Behaviour {

	public Flee () : base ("Flee") {}
	public Flee( AgentMeta target, AgentMeta character ) : base( "Flee", target, character ) {}

	public override SteeringOutput.SteeringOutput getSteering(){
		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput();
		steering.linear = Character.position - Target.position;
		steering.linear = steering.linear.normalized * Character.maxAcceleration;
		steering.angular = 0.0f;
		return steering;

	}

}
