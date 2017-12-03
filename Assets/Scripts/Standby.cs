using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Standby : Behaviour {

	public Standby () : base ("Standby") {}
	public Standby (AgentMeta target, AgentMeta character) : base ("Standby", target, character) {}

}
