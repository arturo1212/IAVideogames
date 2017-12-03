using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Arrive : Behaviour {

	public Arrive () : base ("Arrive") {}
	public Arrive( AgentMeta target, AgentMeta character, float SlowRadius, float TargetRadius, float TimeToTarget) 
		: base( "Arrive", target, character, SlowRadius, TargetRadius, TimeToTarget ) {}

	public override SteeringOutput.SteeringOutput getSteering(){
		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput( new Vector2( 0.0f, 0.0f), 0.0f );
		Vector2 direction = Target.position - Character.position;
		float distance = direction.magnitude;
		if (distance < targetRadius) {
			steering.linear = -Character.velocity;
			return steering;
		}
		var targetSpeed = Character.maxSpeed;

		if (distance <= slowRadius)
			targetSpeed *= distance / slowRadius;

		var targetVelocity = direction.normalized * targetSpeed;

		steering.linear = ((Vector3)targetVelocity) - Character.velocity;
		steering.linear /= timeToTarget;

		steering.angular = 0.0f;
		return steering;

	}

}
