using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {

	Vector3 myPosition;
	// Use this for initialization
	void Start () {
		myPosition = transform.position;
	}
	
	void Update () {
	}
	

	
	void OnCollisionEnter (Collision col)
	{
		
		var touch = col.gameObject.GetComponent<Rigidbody> ();
		touch.constraints = RigidbodyConstraints.FreezePositionY;
		//touch.AddForce (directionForce, ForceMode.Impulse);;
	}


	void OnCollisionExit (Collision col)
	{
		
		var touch = col.gameObject.GetComponent<Rigidbody> ();
		touch.constraints = RigidbodyConstraints.None;//FreezePositionY;
		//touch.AddForce (directionForce, ForceMode.Impulse);;
	}
}
