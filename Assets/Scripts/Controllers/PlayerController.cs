using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : AgentMeta {
	public int AgentCurrentBehaviour;
	void Awake()
	{
		AgentCurrentBehaviour = 0;
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void Update()
	{
		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis ("Horizontal");

		//Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis ("Vertical");

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		this.linear = (movement.normalized * maxAcceleration);

		if (Input.GetKeyDown ("f"))
			AgentCurrentBehaviour++;
		if (Input.GetKeyDown ("q"))
			angular = maxAngularAcceleration;
		if (Input.GetKeyDown ("e"))
			angular = -maxAngularAcceleration;
		if (Input.GetKeyDown ("z"))
			stop ();
		if (Input.GetKeyDown ("x"))
			fullStop ();
		if (Input.GetKeyDown ("space"))
			velocity.z = this.jumpVelocity;
		if (Input.GetKeyDown ("o")) {
			Vector3 dir = new Vector3(Mathf.Cos(this.orientation*Mathf.Deg2Rad), Mathf.Sin(this.orientation*Mathf.Deg2Rad), 1.5f);
			Vector3 vel = Vector3.Scale(new Vector3 (3f, 3f, 1.2f), dir.normalized);
			ObjectCreator.createProjectile (vel, this.position);
		}
		if (Input.GetKeyDown ("n")) {
			//ObjectCreator.createAgent (this.getPosition());
		}
	}

	// Renderización de frames para poder identificar la velocidad del objeto.
	public int getCurrentBehaviour(){
		return AgentCurrentBehaviour;
	}

}