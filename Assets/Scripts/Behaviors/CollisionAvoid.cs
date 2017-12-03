using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoid : Behaviour {
	
	private float radius, maxAcc;
	private AgentMeta[] targets;

	public CollisionAvoid ( AgentMeta character, AgentMeta[] Targets, float Radius = 0.1f, float MaxAcc = 0.5f) 
		: base("Collision Avoidance") {
		Character = character;
		targets = Targets;
		radius = Radius;
		maxAcc = MaxAcc;
	}

	public override SteeringOutput.SteeringOutput getSteering() {
		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput( new Vector2( 0.0f, 0.0f), 0.0f );
		float shortestTime = float.MaxValue;		// Tiempo faltante para la colision.
		AgentMeta firstTarget = null;				// Agente mas cercano a ocasionar una colision.

		// Distancias y posiciones relativas entre agentes.
		float   firstMinSeparation = -1, firstDistance = float.MaxValue;	
		Vector2 firstRelativePos = new Vector2 (), firstRelativeVel = new Vector2(), relativePos;

		foreach (AgentMeta target in targets) 	// Para cada objeto que puede causar una colision.
		{
			relativePos   = target.position - Character.position;			// Posicion de Agente -> Target
			Vector2 relativeVel   = target.velocity - Character.velocity;	// Velocidad Agente -> Target.
			float relativeSpeed   = relativeVel.magnitude;								// Rapidez relativa.
			float distance = relativePos.magnitude;										// Distancia relativa entre agentes.				
			float minSeparation = distance - relativeSpeed * shortestTime;				// Separacion minima (COLISION).

			/* Proyeccion del vector posicion sobre el vector velocidad para estimar el tiempo de colision. */
			float timeToCollision = Mathf.Abs(Vector2.Dot (relativePos, relativeVel) / (relativeSpeed * relativeSpeed));
			if (minSeparation > 2 * radius) { continue; }		
			if (timeToCollision > 0 && timeToCollision < shortestTime)
			{
				/* Actualizacion del objetivo mas proximo a ocasionar colision */
				shortestTime = timeToCollision;
				Debug.Log ("ShortT: " + shortestTime);
				firstTarget = target;
				firstMinSeparation = minSeparation;
				firstDistance = distance;
				firstRelativePos = relativePos;
				firstRelativeVel = relativeVel;
			}
		}
		/* Realizar evasion */
		if (firstTarget != null) {
			if (firstMinSeparation <= 0 || firstDistance < 2 * radius) {
				/* Caso 1: Ya choque, que chimbo */
				relativePos = firstTarget.position - Character.position;
			} 
			else 
			{
				/* Caso 2: Si sigo asi voy a chocar, y eso no es bueno :( */
				relativePos = firstRelativePos + firstRelativeVel * shortestTime;
			}
			relativePos.Normalize ();
			steering.linear = -relativePos * maxAcc;
		}
		return steering;
	}
}
