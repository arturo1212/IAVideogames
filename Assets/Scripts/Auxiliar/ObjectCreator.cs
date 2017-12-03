using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObjectCreator{
	
	public static void createProjectile(Vector3 vel, Vector3 pos){
		GameObject dummy = (GameObject)ProyectilController.Instantiate (Resources.Load ("Prefab/Ball3"));
		ProyectilController am = (ProyectilController)dummy.GetComponent<ProyectilController> ();
		am.startVelocity = vel;
		am.position = pos;
	}

	public static void createAgent(Vector3 pos)
	{
		GameObject dummy = (GameObject)ProyectilController.Instantiate (Resources.Load ("Prefab/Agent_1"));
		AgentController am = (AgentController)dummy.GetComponent<AgentController> ();
		am.currentBehaviour = am.ListBehaviours[3];
	}
}
