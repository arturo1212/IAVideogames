using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentControllerOld : AgentMeta {

	public List<Behaviour> ListBehaviours;
	public Behaviour currentBehaviour;
	private PlayerController Player;

	void Awake(){

		shipSprite = Resources.LoadAll<Sprite> ("Sprites/Spaceship");	// Cargamos las imagenes para ser usadas
		var player = GameObject.Find("Player");
		
		Player = player.GetComponent<PlayerController> ();

		maxSpeed = Player.maxSpeed;								
		maxAcceleration = Player.maxAcceleration;				
		maxRotation = Player.maxRotation;						
		maxAngularAcceleration = Player.maxAngularAcceleration;

		// Inicializamos los comportamientos.
		ListBehaviours = new List<Behaviour>();
		AgentMeta[] targets = { Player };
		var Nodes = new List<Vector2> ();
		Nodes.Add (new Vector2 (-1.0f, -1f));
		Nodes.Add (new Vector2 (-1f, 1.2f));
		Nodes.Add (new Vector2 (1f, 1.2f));
		Nodes.Add (new Vector2 (1f, -1.2f));

		/* Behaviors */ 
		ListBehaviours.Add (new Standby (Player, this));
		/*
		ListBehaviours.Add (new Seek(Player, this));
		ListBehaviours.Add (new Flee (Player, this));
		ListBehaviours.Add (new Arrive (Player, this, 5.0f, 1.0f, .1f));
		ListBehaviours.Add (new Pursue (Player, this, 5.0f));
		ListBehaviours.Add (new Evade (Player, this, 5.0f));
		ListBehaviours.Add (new VelocityMatching (Player, this));
		ListBehaviours.Add (new Align (Player, this, Mathf.PI/4, Mathf.PI/10, .1f));
		ListBehaviours.Add (new Face (Player, this));
*/
		// ListBehaviours.Add (new Wander (this, 1f, 1.0f, 10f, 0f));
		// Probar con 0.2 de aceleracion
		//ListBehaviours.Add (new PathFollowing (this, Nodes, 10f));
		//ListBehaviours.Add (new PredictivePathFollowing (this, Nodes, 1f, 0.1f));

		ListBehaviours.Add (new ObstacleAvoidance (this));
		ListBehaviours.Add (new Separation ( this, targets, 1f, 0.2f ));

		currentBehaviour = ListBehaviours [0];
	
	}

	// Update is called once per frame
	void Update () {
		if (currentBehaviour != null) {
			SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput ();
			currentBehaviour = ListBehaviours [Player.getCurrentBehaviour () % ListBehaviours.Count];
			print (currentBehaviour.getBehaviourName ());
			steering = currentBehaviour.getSteering ();
			this.linear = steering.linear;
			this.angular = steering.angular;
		}
	}
	void OnGUI(){
		Vector2 size = new Vector2 (400, 40);
		Vector2 center = new Vector2 (0, 0); 
		Rect r1 = new Rect (center.x, center.y, size.x, size.y);
		GUI.Box(r1,"Behaviour: " +  currentBehaviour.getBehaviourName ());

	}
}
