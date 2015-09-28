using UnityEngine;
using System.Collections;

public class Balls : MonoBehaviour
{
	public GameObject[] balls;
	int currentBall = 0;
	Rigidbody myRigidBody;
	// Use this for initialization
	void Start ()
	{
		myRigidBody = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void SetBall (int ballNum)
	{
		myRigidBody.useGravity = true;
		if (currentBall == ballNum)
			return;
		balls [currentBall].SetActive (false);
		balls [ballNum].SetActive (true);
		currentBall = ballNum;
	}

	public void SetGlass ()
	{
		SetBall (0);

	}

	public void SetMetal ()
	{
		SetBall (1);

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
