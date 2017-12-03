using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class VelocityMatching : Behaviour {

	public VelocityMatching () : base ("Velocity Matching") {}
	public VelocityMatching (AgentMeta target, AgentMeta character) : base ("Velocity Matching", target, character) {}

	public override SteeringOutput.SteeringOutput getSteering(){

		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput();

		steering.linear = Target.velocity - Character.velocity;

		steering.linear /= Character.maxAcceleration;

		steering.angular = 0.0f;
		return steering;

	}

}
