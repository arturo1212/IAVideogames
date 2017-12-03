using System;
using UnityEngine;

namespace BehaviourAndWeight
{
	public struct BehaviourAndWeight
	{
		public Behaviour behaviour;
		public float weight;

		public BehaviourAndWeight( Behaviour _Behaviour, float Weight){

			behaviour = _Behaviour;
			weight = Weight;

		}
	}
}