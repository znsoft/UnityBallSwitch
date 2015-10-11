using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

public class Buttons : MonoBehaviour {
	public GameObject playerBall;
	public GameObject botBalls;
	GameObject playerBalls;
	public GameObject myCamera;
	// Use this for initialization
	IEnumerator	 Start() {
		botBalls.SendMessage ("SetBotBall", true);
		botBalls.SendMessage ("SetPlayerBall", "Paper");
		yield return new WaitForSeconds(1);
		playerBalls = Instantiate (playerBall);
		myCamera.GetComponent<AutoCam> ().SetTarget( playerBalls.transform);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}

	public void AnyBallClick(GameObject button){

		playerBalls.SendMessage( "SetPlayerBall", button.name);

	} 

	public void AnyButtonClickStartMethod(string methodName){
		playerBalls.SendMessage( methodName);
	}

	public void ReloadLevel ()
	{
		Application.LoadLevel (Application.loadedLevelName);
	}

}
