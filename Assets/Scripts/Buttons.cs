using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {
	public GameObject allBalls;
	// Use this for initialization
	void Start () {
		var tempBalls = Instantiate<GameObject> (allBalls);
		//allBalls.SetActive (false);
		//tempBalls.SendMessage ("SetMetal");


	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}

	public void AnyButtonClick(string buttonMethod){
		allBalls.SendMessage (buttonMethod);

	} 

}
