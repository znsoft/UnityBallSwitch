using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

	//private Transform m_Cam;                  // A reference to the main camera in the scenes transform
	//private Vector3 m_CamForward;             // The current forward direction of the camera
	//private Vector3 m_Move;
	private bool  pause = false;                      // the world-relative desired move direction, calculated from the camForward and user input.
	public GameObject myBody;
	public GameObject buttonStop;
	public GameObject canvas;
	public GameObject butPrefab;
	public GameObject butPanels;
	public GameObject[] ragdollPlates;
	Rigidbody myRigidBody;
	public float plateAngleSpeed = 5;
	//GameObject line;
	LineRenderer line;
	List<GameObject> buttonList = new List<GameObject>();
	GameObject plate = null;
	float lookAngle, tiltAngle ;
	public float futureTimeScale = 0.2f;


	private void Start ()
	{
		line = GetComponent<LineRenderer> ();
		line.enabled = false;
		myRigidBody = myBody.GetComponent<Rigidbody> ();
	}

	public void PauseGame ()
	{
		pause = !pause;

		if (!pause)
			foreach (GameObject go in buttonList)
				DestroyObject (go);
		buttonList.Clear ();
		plate = null;
		int plateCount = -1,platesCountMax = ragdollPlates.Length;
		if(pause)foreach (var childPlace in butPanels.GetComponentsInChildren<Transform>()) {
			//butPrefab.transform.localPosition = Vector3.zero;
			if (childPlace.Equals (butPanels.transform))
				continue;
			if(++plateCount>=platesCountMax) break;
			GameObject t = Instantiate (butPrefab);
			t.name = plateCount.ToString();
			t.transform.localPosition = childPlace.localPosition;
			t.transform.position = childPlace.position;
			//t.transform.parent = canvas.transform;
			t.transform.parent = childPlace;
			Button button = t.gameObject.GetComponent<Button>();	
			button.onClick.AddListener(() => ItemSelect(button)); 
			buttonList.Add(t);
		}
		Time.timeScale = pause ? 0 : 1;
	}

	private void Update ()
	{
		line.enabled = pause;
		if (pause) {
			DrawWay ();
			if (plate != null)			TurnPlate();
		}
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}

	void DrawWay ()
	{

		var futurePosition = GetFuturePosition (futureTimeScale);
		line.SetPosition (0, myBody.transform.position);
		line.SetPosition (1, myBody.transform.position + futurePosition);
		//Debug.DrawRay (transform.position, futurePosition, Color.red);
	
	}

	Vector3 GetFuturePosition (float t)
	{
		Vector3 myVelocity = myRigidBody.velocity;
		var gravity = Physics.gravity;
		//float t = futureTimeScale;
		//forecast time
		Vector3 futurePosition = myVelocity * t + (gravity * t * t) / 2;
		return futurePosition;
	}
	void OnCollisionStay (Collision col)
	{
		//col.gameObject.transform.position.
		
	}
	
	void OnCollisionEnter (Collision col)
	{
	
	}
	// Fixed update is called in sync with physics
	private void FixedUpdate ()
	{


	}

	public void ItemSelect (Button button)
	{
		Debug.Log (button.gameObject.name);
		GenerateNewRing (ragdollPlates[int.Parse(button.gameObject.name)]);
	}

	void GenerateNewRing (GameObject prefab)
	{
		if (plate != null)
			DestroyObject (plate);
		plate = Instantiate<GameObject> (prefab);
		lookAngle = 0;
		tiltAngle = 0;

		Vector3 directionPos = GetFuturePosition (futureTimeScale);
		plate.transform.position = myBody.transform.position + directionPos;
	}


	void TurnPlate(){
		var x = CrossPlatformInputManager.GetAxis ("Horizontal");
		var y = CrossPlatformInputManager.GetAxis ("Vertical");

		lookAngle += x * plateAngleSpeed;
		tiltAngle += y * plateAngleSpeed;
			tiltAngle = Mathf.Clamp (tiltAngle, -45, 75);
		Quaternion transformTargetRot = Quaternion.Euler (tiltAngle, lookAngle, plate.transform.position.z);
	
		plate.transform.localRotation = transformTargetRot;
			//plate.transform.localRotation += transformTargetRot;

	}

}

