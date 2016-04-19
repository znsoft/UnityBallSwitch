using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour {

	private Transform m_Cam;                  // A reference to the main camera in the scenes transform
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;
	private bool m_Jump, pause = false;                      // the world-relative desired move direction, calculated from the camForward and user input.
	public Rigidbody myRigidBody;
	
	private void Start()
	{
	//	myRigidBody = GetComponent<Rigidbody> ();
	}
	
	
	private void Update()
	{
			m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		if (m_Jump)
			pause = !pause;
		Time.timeScale = pause ? 0 : 1;

		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}


	
	// Fixed update is called in sync with physics
	private void FixedUpdate()
	{


	}
}

