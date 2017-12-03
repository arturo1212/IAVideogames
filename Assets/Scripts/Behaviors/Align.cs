using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Align : Behaviour {

	public Align () : base ("Align") {}
	public Align( AgentMeta target, AgentMeta character, float SlowRadius, float TargetRadius, float TimeToTarget) 
		: base( "Align", target, character, SlowRadius, TargetRadius, TimeToTarget ) {}

	public override SteeringOutput.SteeringOutput getSteering(){
		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput( new Vector2( 0.0f, 0.0f), 0.0f );
		float rotation = Character.orientation - Target.orientation;	// Diferencia en las orientaciones
		rotation *= Mathf.Deg2Rad - Mathf.PI;			// Mapping

		float rotationSize = Mathf.Abs (rotation);		// Diferencia en las orientaciones (Solo magnitud).
		bool cond1 = rotationSize < targetRadius;
		if (cond1) {			// Si la diferencia es muy pequenia, chao.
			Character.rotation = 0f;
			return steering;
		}
		/* Calculo de aceleracion */
		float targetRotation = Character.maxRotation;	// Velocidad de rotacion maxima. (Fuera del radio)
		if (rotationSize <= slowRadius)					// Si estoy acercandome, disminuir aceleracion
			targetRotation *= rotationSize / slowRadius;
		
		targetRotation *= rotation / rotationSize;
		steering.angular = targetRotation - Character.rotation;
		steering.angular /= timeToTarget;

		return steering;

	}

}
