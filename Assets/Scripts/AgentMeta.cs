using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AgentMeta : MonoBehaviour {
	#region Variables
	protected Sprite[] shipSprite;	// Sprites de las naves para la presentación
	public BoxCollider2D m_Collider;

	/* Variables del movimiento */ 
	public Vector3 position; 	// Posición del objeto.
	public float orientation;	// Orientación del objeto.
	public Vector3 velocity; 	// Velocidad lineal del objeto.
	public float rotation;		// Velocidad de rotación del objeto.
	public Vector3 linear;		// Aceleración lineal.
	public float angular;		// Aceleración angular.

	/* Restricciones de movimiento */ 
	public float maxSpeed;					// Maxima velocidad.
	public float maxAcceleration;			// Maxima aceleración.
	public float maxRotation;				// Máxima velocidad angular.
	public float maxAngularAcceleration;	// Máxima aceleración angular.
	public float jumpVelocity;				// Velocidad maxima para iniciar saltos.

	/* Restricciones de vision */
	public float viewRadius;		// Radio de vision del agente.
	public float viewAngle;			// Angulos de vision (mid-plane).

	/* Herramientas para dibujar cosas cheveres */
	public LineRenderer lineRender;		// Dibujador de lineas.

	/* Colisiones */
	public Rigidbody2D rb;
	#endregion

	void Start(){
		/* Inicializacion cinematica */
		position = transform.position;
		orientation = transform.eulerAngles.z;
		m_Collider = this.GetComponent<BoxCollider2D>();
		rb = this.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate(){
		
		/* ------------- SALTOS -------------------*/
		linear.z = -0.98f;	// Gravedad
		position    += velocity * Time.deltaTime;
		orientation += rotation * Time.deltaTime;
		orientation %= 360f;
		if (position.z < 0) 
		{ 
			position.z = 0;
			if( m_Collider != null)
				m_Collider.enabled = true;
		} 
		else 
		{
			if( m_Collider != null)
				m_Collider.enabled = false;
		}

		/* ------------- Velocidades --------------*/
		float aux = velocity.z;
		if( rb != null)
		velocity = rb.velocity;				 // Manejar el RigidBody.
		velocity.z = aux;
		velocity += linear * Time.deltaTime; // Actualizar velocidades.
		rotation += angular * Time.deltaTime;
		if( rb != null)
		rb.velocity = velocity;
		// Acotar velocidades
		if( velocity.magnitude > maxSpeed )
			velocity = velocity.normalized * maxSpeed;
		if (rotation > maxRotation)
			rotation = (rotation / Mathf.Abs (rotation)) * maxRotation;
		if (angular > maxAngularAcceleration) 
			angular = (angular / Mathf.Abs (angular)) * maxAngularAcceleration;

		/*-------------- Actualizacion --------------*/
		transform.rotation = Quaternion.AngleAxis(orientation, Vector3.forward);
		transform.position = position;
		transform.localScale = new Vector3 (position.z + 1, position.z + 1);

		/*-------------- Friccion -------------------*/
		// Agregar aqui
	}


	public void stop(){
		velocity = new Vector3 (0.0f, 0.0f, 0f);
		rotation = 0.0f;
	}

	public void fullStop()
	{
		linear = new Vector3 (0.0f, 0.0f,0f);
		angular = 0.0f;
	}
}