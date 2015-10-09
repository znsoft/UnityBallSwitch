using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Balls : MonoBehaviour
{
	Rigidbody myRigidBody;
	private Dictionary<string,GameObject> ball;
	string currentBallName;

	void Start ()
	{
		myRigidBody = this.GetComponent<Rigidbody> ();
		ball = this.GetComponentsInChildren<Collider> ().ToDictionary (s => s.gameObject.name, (s) => {
			s.gameObject.SetActive (false);
			return s.gameObject;});
		var first = ball.Values.ElementAt<GameObject> (0);
		currentBallName = first.name;
		first.SetActive (true);
		Debug.Log ("Start balls " + currentBallName);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 myVelocity = myRigidBody.velocity;
		var gravity = Physics.gravity;
		float t = 1;//forecast time
		Vector3 futurePosition = myVelocity * t + (gravity * t * t) / 2;
		Debug.DrawRay (transform.position, futurePosition, Color.red);



	}

	void OnCollisionStay (Collision col)
	{
		//col.gameObject.transform.position.

	}

	void OnCollisionEnter (Collision col)
	{
		AudioSource touchSound = null;
		touchSound = col.collider.gameObject.GetComponent<AudioSource> ();
		if (touchSound == null)
			touchSound = col.gameObject.GetComponent<AudioSource> ();
		if (touchSound == null)
			touchSound = ball [currentBallName].GetComponent<AudioSource> ();
		if (touchSound == null)
			touchSound = transform.gameObject.GetComponent<AudioSource> ();
		if (touchSound != null)
			touchSound.Play ();
		if (col.collider.gameObject.name == "Exit") {
			myRigidBody.useGravity = false;
			myRigidBody.velocity = Vector3.zero;

		}
	}

	void RestartLevel ()
	{
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void SetPlayerBall (string ballName)
	{
		myRigidBody.useGravity = true;
		if (currentBallName == ballName)
			return;
		ball [currentBallName].SetActive (false);
		ball [ballName].SetActive (true);
		currentBallName = ballName;	
		Debug.Log (currentBallName);


	}
}
