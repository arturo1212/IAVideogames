﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparationOld : Behaviour {
	private AgentMeta[] Targets;
	private float Threshold;
	private float Decay;
	public SeparationOld () : base ("Separation") {}
	public SeparationOld ( AgentMeta character, AgentMeta[] targets, float threshold, float decay ) : base( "Separation" ) {
		Character = character;
		Targets = targets;
		Threshold = threshold;
		Decay = decay;
	}

	public override SteeringOutput.SteeringOutput getSteering()
	{
		float strength = 0;
		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput();
		foreach (AgentMeta target in Targets) 
		{
			Vector2 direction = -target.position+ Character.position;
			float distance = direction.magnitude;
			if (distance < Threshold && distance != 0) {
				strength = Decay / (distance * distance);
				direction.Normalize ();
				steering.linear += strength * direction; 
			} else 
			{
				Vector2 vel = Character.velocity;
				steering.linear = -vel;
			}

		}
		return steering;
	}
}
