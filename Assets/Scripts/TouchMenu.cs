using UnityEngine;
using System.Collections;

public class TouchMenu : MonoBehaviour {
	public GameObject particle;
	// Use this for initialization
	void Start () {
	
	}
	
	//var particle : GameObject;
	void Update () {
		if (Input.touches.Length > 0) {
			Time.timeScale =  0 ;
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
