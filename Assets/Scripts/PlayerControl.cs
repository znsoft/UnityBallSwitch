using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour {

	private Transform m_Cam;                  // A reference to the main camera in the scenes transform
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;
	private bool m_Jump, pause = false;                      // the world-relative desired move direction, calculated from the camForward and user input.
	public GameObject myBody;
	Rigidbody myRigidBody;
	//GameObject line;
	LineRenderer line;

	private void Start()
	{
		line = GetComponent<LineRenderer> ();
		line.enabled = false;
		myRigidBody = myBody.GetComponent<Rigidbody> ();
	}



	
	private void Update()
	{
			m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		if (m_Jump)
			pause = !pause;
		Time.timeScale = pause ? 0 : 1;
		line.enabled = pause;
		if (pause) {
			DrawWay();
		
		}
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}




	void DrawWay(){

		Vector3 myVelocity = myRigidBody.velocity;
		var gravity = Physics.gravity;
		float t = 1;//forecast time
		Vector3 futurePosition = myVelocity * t + (gravity * t * t) / 2;
		line.SetPosition (0, myBody.transform.position);
		line.SetPosition (1, myBody.transform.position + futurePosition);
		//Debug.DrawRay (transform.position, futurePosition, Color.red);
	
	}
	
	// Fixed update is called in sync with physics
	private void FixedUpdate()
	{


	}
}

