using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Behaviour {

	protected string Name;

	protected AgentMeta Target;
	protected AgentMeta Character;

	public float slowRadius;
	public float targetRadius;

	public float timeToTarget;
	public float maxPrediction;

	// Constructores
	public Behaviour( string behaviourName ) {

		Name = behaviourName;
	}

	public Behaviour( string behaviourName, AgentMeta target, AgentMeta character ){

		Name = behaviourName;
		Target = target;
		Character = character;

	}

	public Behaviour( string behaviourName, AgentMeta target, AgentMeta character, float SlowRadius, float TargetRadius, float TimeToTarget){

		Name = behaviourName;
		Target = target;
		Character = character;
		slowRadius = SlowRadius;
		targetRadius = TargetRadius;
		timeToTarget = TimeToTarget;

	}

	public Behaviour( string behaviourName, AgentMeta target, AgentMeta character, float MaxPrediction ){

		Name = behaviourName;
		Target = target;
		Character = character;
		maxPrediction = MaxPrediction;

	}

	public virtual SteeringOutput.SteeringOutput getSteering(){

		Character.fullStop ();

		return new SteeringOutput.SteeringOutput( new Vector2( 0.0f, 0.0f), 0.0f );

	}

	// Random Binomial.
	protected float randomBinomial(){

		return Random.Range ( -1.0f, 1.0f);

	}

	// Set de las variables
	public void setTarget( AgentMeta target ){

		Target = target;

	}

	public void setCharacter( AgentMeta character ){

		Character = character;

	}

	// Get de las variables
	public string getBehaviourName(){

		return Name;

	}

}