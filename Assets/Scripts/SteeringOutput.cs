using System;
using UnityEngine;

namespace SteeringOutput
{
	public struct SteeringOutput
	{
		public Vector2 linear;
		public float angular;

		public SteeringOutput( Vector2 Linear, float Angular){

			linear = Linear;
			angular = Angular;

		}

		public static SteeringOutput operator +( SteeringOutput s1, SteeringOutput s2){

			return new SteeringOutput (s1.linear + s2.linear, s1.angular + s2.angular);

		}

	}
}

