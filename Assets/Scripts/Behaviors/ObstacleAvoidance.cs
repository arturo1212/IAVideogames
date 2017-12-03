using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : Behaviour {

	public ObstacleAvoidance ( AgentMeta character): base("Obstacle Avoidance"){
		Character = character;
	}
		
	public override SteeringOutput.SteeringOutput getSteering() {
		/* Calcular direccion y magnitud del cono  */
		float angle = Character.orientation;
		Vector3 orientation = new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0f); 
		GameObject obstacle = GameObject.Find ("Obstacle_1");
		ObstacleCreator oc = obstacle.GetComponent<ObstacleCreator> ();
		Vector3 startPos = Character.transform.position;
		startPos.z = -1;
		Vector3 endPos = startPos + orientation.normalized * Character.viewRadius;
		endPos.z = -1;
		bool choca = oc.bbx.rayCollides(startPos, endPos);
		foreach (Vector2 v in oc.bbx.vertex) {
			if ((startPos - new Vector3(v.x,v.y)).magnitude < Character.viewRadius && Vector3.Angle (startPos, v) <= Character.viewAngle) {
				choca = true;
				break;
			}
		}
		Debug.Log ("BOOL: " + choca.ToString() + "positions" + startPos + endPos);
		/* Pintar perolito*/
		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput( new Vector2( 0.0f, 0.0f), 0.0f );
		if (choca) {
			steering.linear = new Vector2 (orientation.y, -orientation.x).normalized*Character.maxAcceleration;
			Character.lineRender.material.color = Color.red;
			Character.lineRender.startColor = Color.red;
			Character.lineRender.endColor   = Color.red;
		} else {
			Character.lineRender.material.color = Color.green;
			Character.lineRender.startColor = Color.green;
			Character.lineRender.endColor   = Color.green;
		}
		Vector3[] linePositions = { startPos, endPos };
		Character.lineRender.SetPositions (linePositions);
		return steering;
	}
}
