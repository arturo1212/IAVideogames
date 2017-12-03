using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Wander : Behaviour {

	private float wanderOffset;
	private float wanderRadius;
	private float wanderRate;
	private float wanderOrientation;

	public Wander () : base ("Wander") {}
	public Wander ( AgentMeta character, float offset, float radius, float rate, float orientation ) 
		: base ("Wander") {
		Character = character;
		wanderOffset = offset;
		wanderRadius = radius;
		wanderRate = rate;
		wanderOrientation = orientation;
	}

	public override SteeringOutput.SteeringOutput getSteering ()
	{
		wanderOrientation = base.randomBinomial() * wanderRate;						// Hallar Orientacion aleatoria
		float targetOrientation = Mathf.Abs(wanderOrientation + Character.orientation);	// Obtener nueva orientacion objetivo.
		float targetOrientationRadian = targetOrientation * Mathf.Deg2Rad;			// Orientacion en radianes
		float orientationRadian = Character.orientation * Mathf.Deg2Rad;
		Debug.Log ("WO:" + wanderOrientation);
		Vector2 target = Character.position
		                 + wanderOffset * new Vector3 (Mathf.Cos (orientationRadian), Mathf.Sin (orientationRadian),0)
		                 + wanderRadius * new Vector3 (Mathf.Cos (targetOrientationRadian), Mathf.Sin (targetOrientationRadian),0);

		//GameObject dummy = (GameObject) MonoBehaviour.Instantiate (Resources.Load ("Prefab/Dummy"));
		AgentMeta dummyAgent = new AgentMeta();		// Agente auxiliar para cambiar orientacion.
		dummyAgent.position = target;
		dummyAgent.orientation = wanderOrientation + Character.orientation;

		/* Moverse en la direccion de la orientacion */
		Behaviour face = new Face (dummyAgent, Character);
		SteeringOutput.SteeringOutput steering = face.getSteering ();
		steering.linear = Character.maxAcceleration * new Vector2 (Mathf.Cos (orientationRadian),
																   Mathf.Sin (orientationRadian));
		/* Limpiar y salir */
		MonoBehaviour.Destroy (dummyAgent);
		return steering;

	}

}

