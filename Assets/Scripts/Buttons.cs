using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {
	public GameObject playerBalls;
	public GameObject botBalls;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}

	public void AnyBallClick(GameObject ball){
		playerBalls.SendMessage( "SetPlayerBall", ball.name);

	} 

	public void AnyButtonClickStartMethod(string methodName){
		playerBalls.SendMessage( methodName);
	}

	public void ReloadLevel ()
	{
		Application.LoadLevel (Application.loadedLevelName);
	}

}
