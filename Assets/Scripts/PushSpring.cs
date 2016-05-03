using UnityEngine;
using System.Collections;

public class PushSpring : MonoBehaviour {
	public float amplifier;
	Vector3 directionForce;
	Vector3 myPosition;
	// Use this for initialization
	void Start () {
		directionForce = Vector3.Normalize (transform.up);//getPlate.localPosition);
		directionForce = Vector3.Scale(directionForce, new Vector3(amplifier, amplifier, amplifier));
		myPosition = transform.position;
	}
	
	void Update () {
	}

	IEnumerator ReturnPlate(int countDown){
		//transform.Translate (transform.up  );
		if (countDown > 0)
			yield return ReturnPlate (--countDown);
		yield break;

	}


	void OnCollisionEnter (Collision col)
	{
			
			StartCoroutine (ReturnPlate (10));
		var touch = col.gameObject.GetComponent<Rigidbody> ();
		if (touch != null) {
				
			touch.velocity = Vector3.zero;
			touch.AddForce (directionForce, ForceMode.Impulse);
		}
	}

}
