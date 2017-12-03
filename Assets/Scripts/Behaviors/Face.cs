using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Face : Behaviour {

	public Face () : base ("Face") {}
	public Face ( AgentMeta target, AgentMeta character ) : base( "Face", target, character ) {}

	public override SteeringOutput.SteeringOutput getSteering(){
		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput();
		Vector2 dir = Target.position - Character.position;
		if (dir.magnitude == 0) 
		{
			return steering;
		}

		GameObject dummy = (GameObject) MonoBehaviour.Instantiate (Resources.Load ("Prefab/Dummy"));
		AgentMeta aux = dummy.GetComponent<AgentMeta> ();
		aux.position =  Target.position;
		aux.orientation = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
		Behaviour alinear = new Align (aux, Character, Mathf.PI/4, Mathf.PI/10, .1f);
		//steering.linear = steering.linear.normalized * Character.maxAcceleration;
		steering = alinear.getSteering();
		MonoBehaviour.Destroy (dummy);
		return steering;

	}

}
