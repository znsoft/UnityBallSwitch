using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

public class RagDollMenu : MonoBehaviour {

	public GameObject playerChar;
	public GameObject myCamera;
	Rigidbody playerRigidBody;
	public GameObject[] prefabs;
	Vector3 startPoint;
	public GameObject controlPanel;
	
	public bool paused = false;
	
	// Use this for initialization
	IEnumerator	 Start() {
		controlPanel.SetActive (paused);
		startPoint = playerChar.transform.position;
		yield return new WaitForSeconds(1);;
		Random.seed = 0;
	}
	
	
	
	
	IEnumerator RepeatAction ( GameObject botBall)
	{
		//------------
		Vector3 botVelocity = playerRigidBody.velocity;
		var gravity = Physics.gravity;
		float t = 1;//forecast time
		Vector3 futurePosition = botVelocity * t + (gravity * t * t) / 2;
		//Debug.DrawRay (transform.position, futurePosition, Color.red);
		//GenerateBlock(botBalls,
		if((botVelocity.y>0 && botBall.transform.position.y>0)||
		   (botVelocity.y <0 && botBall.transform.position.y <0))
		if (botVelocity.magnitude > 7.0F) {
			GameObject block = Instantiate<GameObject> (prefabs [0]);//Random.Range(0,prefabs.GetUpperBound(0))]);
			block.transform.position = botBall.transform.position + futurePosition;
			block.transform.rotation = Quaternion.LookRotation(futurePosition);//Random.rotation;
			//block.transform.rotation.SetLookRotation (Vector3.up);
			//block.transform.rotation.SetLookRotation (botBall.transform.position);
		}
		//===========
		yield return new WaitForSeconds (1);//Random.Range(1.0F ,3));
		this.StartCoroutine ("RepeatAction", botBall);
	}
	
	
	
	
	
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}
	
	public void PlayPauseButton(){
		paused = !paused;
		controlPanel.SetActive (paused);
		
		Time.timeScale = paused ? 0 : 1;
	}
	
	public void AnyBallClick(GameObject button){
		if (paused) {
			return; }
	} 
	
	public void AnyButtonClickStartMethod(string methodName){
	}
	
	
	
	public void ReloadLevel ()
	{
		Application.LoadLevel (Application.loadedLevelName);
	}

}
