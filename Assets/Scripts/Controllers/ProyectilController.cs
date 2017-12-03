using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilController : AgentMeta {
	public Vector3 startVelocity;

	void Start()
	{
		
		//startVelocity = this.velocity;
	}
	void Update () {
		if (this.position.z < 0.01f) {
			this.velocity = startVelocity / 1.3f;
			startVelocity = this.velocity;
			if (this.velocity.magnitude < 0.001){
				Destroy (this.gameObject);
			}
		}


	}
}
