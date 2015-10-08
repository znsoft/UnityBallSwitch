using UnityEngine;
using System.Collections;

public class Balls : MonoBehaviour
{
	public GameObject[] balls;
	public float[] mass;
	public float[] drag;
	int currentBall = 0;
	Rigidbody myRigidBody;
	// Use this for initialization
	enum Ball{	Steel, Glass, Wire, Plastic, Stone	}


	void Start ()
	{
		Physics.gravity.Set (0, 1000, 0);
		myRigidBody = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 myVelocity = myRigidBody.velocity;
		var gravity = Physics.gravity;
		float t = 3;//forecast time
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
		if(touchSound==null)touchSound = col.gameObject.GetComponent<AudioSource> ();
		if(touchSound==null)touchSound = balls [currentBall].GetComponent<AudioSource> ();
		if(touchSound==null)touchSound = transform.gameObject.GetComponent<AudioSource> ();
		if(touchSound!=null)touchSound.Play ();
		if (col.collider.gameObject.name == "Exit") {
			myRigidBody.useGravity = false;
			//myRigidBody.freezeRotation = true;
			myRigidBody.velocity = new Vector3(0.0f,0.0f,0.0f);

		}
	}

	void RestartLevel(){
	
		Application.LoadLevel (Application.loadedLevelName);
	}

	void SetBall (int ballNum)
	{
		myRigidBody.useGravity = true;
		if (currentBall == ballNum)
			return;
		balls [currentBall].SetActive (false);
		balls [ballNum].SetActive (true);
		myRigidBody.mass = mass [ballNum];
		myRigidBody.drag = drag [ballNum];
		currentBall = ballNum;
	}

	public void SetGlass ()
	{
		SetBall (0);

		Debug.Log ((int)Ball.Glass);

	}

	public void SetMetal ()
	{
		SetBall (1);
		Debug.Log ((int)Ball.Plastic);
	}

	public void SetPaper ()
	{
		SetBall (2);
	}

	public void SetSmoke ()
	{
		SetBall (3);
	}

}
