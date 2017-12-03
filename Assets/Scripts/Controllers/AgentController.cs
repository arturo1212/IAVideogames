using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentController : AgentMeta {

	public List<Behaviour> ListBehaviours;
	public Behaviour currentBehaviour;
	private GameObject[] surroundingAgents;
	private PlayerController Player;

	void Awake(){

		shipSprite = Resources.LoadAll<Sprite> ("Sprites/Spaceship");	// Cargamos las imagenes para ser usadas
		GameObject player = GameObject.Find("Player");
//		List<BehaviourAndWeight.BehaviourAndWeight> listWeighted;
		
		Player = player.GetComponent<PlayerController> ();
		surroundingAgents = GameObject.FindGameObjectsWithTag ("Agents");
		Debug.Log (surroundingAgents.Length);

		for (int i = 0; i < surroundingAgents.Length; i++) {
			if (surroundingAgents [i] == this.gameObject) {
				surroundingAgents [i] = surroundingAgents [surroundingAgents.Length - 1];
				//surroundingAgents[i] = player;
			}
		}
		Array.Resize (ref surroundingAgents, surroundingAgents.Length - 1);
		Debug.Log (surroundingAgents.Length);

		AgentMeta[] agents = new AgentMeta[surroundingAgents.Length];

		for (int i = 0; i < surroundingAgents.Length; i++)
			agents [i] = surroundingAgents [i].GetComponent<AgentController> ();

		maxSpeed = Player.maxSpeed;								
		maxAcceleration = Player.maxAcceleration;				
		maxRotation = Player.maxRotation;						
		maxAngularAcceleration = Player.maxAngularAcceleration;

		// Inicializamos los comportamientos.
		ListBehaviours = new List<Behaviour>();
		AgentMeta[] targets = { Player };
		var Nodes = new List<Vector2> ();
		Nodes.Add (new Vector2 (-20.0f, 4.0f));
		Nodes.Add (new Vector2 (0.0f, 4.0f));
		Nodes.Add (new Vector2 (0.0f, 0.0f));
//		Nodes.Add (new Vector2 (-3.0f, -2.0f));
//		Nodes.Add (new Vector2 (0.0f, 0.0f));
		ListBehaviours.Add (new Standby (Player, this));
		// ListBehaviours.Add(new Separation ( this, agents, 2f, 10f )); // Lista de targets vacia
		ListBehaviours.Add (new Seek(Player, this));
		ListBehaviours.Add (new Flee (Player, this));
		ListBehaviours.Add (new Arrive (Player, this, 5.0f, 1.0f, .1f));
		ListBehaviours.Add (new Pursue (Player, this, 5.0f));
		ListBehaviours.Add (new Evade (Player, this, 5.0f));
		ListBehaviours.Add (new VelocityMatching (Player, this));
		ListBehaviours.Add (new Align (Player, this, Mathf.PI/100, Mathf.PI/10000, .1f));
		ListBehaviours.Add (new Wander (this, .5f, .25f	, 1.0f, .0f));
		ListBehaviours.Add (new Face (Player, this));
		ListBehaviours.Add (new PathFollowing (this, Nodes, 10f));
		ListBehaviours.Add (new PredictivePathFollowing (this, Nodes, 1f, 0.1f));
		ListBehaviours.Add (new SeekWhileLooking( Player, this));
		ListBehaviours.Add (new ArriveWhileLooking (Player, this, 5f, 1f, .1f));
		ListBehaviours.Add (new BlendedSteering (this, new List<Behaviour> { 
			new SeekWhileLooking (Player, this), 
			new Separation (this, agents, 2f, 10f) }, 
			new List<float> { .1f, 1f }));

		var randomSigned = UnityEngine.Random.Range (-1, 1);
		if (randomSigned < 0)
			Nodes.Reverse();
		ListBehaviours.Add (new BlendedSteering (this, new List<Behaviour> {
			new PathFollowing (this, Nodes, 10f),
			new Separation (this, agents, 2f, 10f)
		}, 
			new List<float> { 1f, .5f }));

		currentBehaviour = ListBehaviours [0];
	
	}

	// Update is called once per frame
	void Update () {

		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput ();

		currentBehaviour = ListBehaviours [Player.getCurrentBehaviour () % ListBehaviours.Count];
		print (currentBehaviour.getBehaviourName ());
		//print (this.position);
		steering = currentBehaviour.getSteering ();

		this.linear = steering.linear;
		this.angular = steering.angular;

	}
	void OnGUI(){
		Vector2 size = new Vector2 (400, 40);
		Vector2 center = new Vector2 (0, 0); 
		Rect r1 = new Rect (center.x, center.y, size.x, size.y);
		GUI.Box(r1,"Behaviour: " +  currentBehaviour.getBehaviourName ());

	}

}
