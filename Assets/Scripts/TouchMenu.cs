using UnityEngine;
using System.Collections;

public class TouchMenu : MonoBehaviour {
	public GameObject particle;
	Transform cam; 
	// Use this for initialization
	void Start () {
	
	}
	protected virtual void Awake()
	{
		// find the camera in the object hierarchy
		cam = GetComponentInChildren<Camera>().transform;
		//m_Pivot = m_Cam.parent;
	}
	//var particle : GameObject;
	void Update () {
	//	cam = GetComponentInChildren<Camera>().transform;
		//int i;		for (i=0; i < 3; i++)			Debug.LogFormat ("{0} key is {1}",i, Input.GetMouseButton (i));
		if ( Input.touchCount > 0){
			Time.timeScale =  0 ;
		}

		if( Input.GetMouseButton(0) ){
			Time.timeScale =  0 ;
			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray)) {
				this.transform.LookAt(transform.position);
				//Debug.Log(Physics.Raycast (ray)
				          // Create a particle if hit
				//	Instantiate (particle, transform.position, transform.rotation);
			}

		}

		foreach (Touch touch in Input.touches) {
			Time.timeScale =  0 ;
			if (touch.phase == TouchPhase.Began) {
				// Construct a ray from the current touch coordinates
				var ray = Camera.main.ScreenPointToRay (touch.position);
				if (Physics.Raycast (ray)) {
					this.transform.LookAt(transform.position);
					// Create a particle if hit
				//	Instantiate (particle, transform.position, transform.rotation);
				}
			}
		}
	}
}
