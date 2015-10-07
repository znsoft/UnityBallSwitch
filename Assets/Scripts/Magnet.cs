using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {
	public float releativeForce;
	// Use this for initialization
	void Start () {
	}
	
	void Update () {
	}
	
	void AddMagnetForce(Collision col, Rigidbody touch)
	{
		foreach (ContactPoint coll_contact in col.contacts) 
			touch.AddForce (coll_contact.normal * releativeForce, ForceMode.Force);
	}

	
	void OnCollisionEnter (Collision col)
	{
		var touch = col.gameObject.GetComponent<Rigidbody> ();
		touch.velocity = Vector3.Scale(touch.velocity, new Vector3(1,0,1));
		AddMagnetForce(col, touch);
	}

	void OnCollisionStay (Collision col)
	{
		
		var touch = col.gameObject.GetComponent<Rigidbody> ();
		AddMagnetForce(col, touch);
	}



	void OnCollisionExit (Collision col)
	{
	}
}
