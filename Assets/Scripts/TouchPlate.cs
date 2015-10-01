
using System.Collections;
using UnityEngine;

public class TouchPlate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col)
	{
		AudioSource touchSound = col.gameObject.GetComponent<AudioSource> ();
		if(touchSound!=null)touchSound.Play ();
		//Debug.Log ("PlaySound");
		//if(col.gameObject.name == "prop_powerCube")		Destroy(col.gameObject);
		//this.Play()
	}
}
