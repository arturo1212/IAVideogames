using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Seek : Behaviour {

	public Seek () : base ("Seek") {}
	public Seek( AgentMeta target, AgentMeta character ) : base( "Seek", target, character ) {}

	public override SteeringOutput.SteeringOutput getSteering(){

		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput();

		steering.linear = Target.position - Character.position;

		steering.linear = steering.linear.normalized * Character.maxAcceleration;

		steering.angular = 0.0f;

		return steering;

	}

}
