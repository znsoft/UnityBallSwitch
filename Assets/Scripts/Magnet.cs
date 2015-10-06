using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {
	public float forceField;
	Vector3 myPosition;
	Vector3 directionForceUp;
	Collider myCollider;
	// Use this for initialization
	void Start () {
		myPosition = transform.position;
		Vector3 directionForce = Vector3.Normalize (transform.up);//getPlate.localPosition);
		directionForce = Vector3.Scale(directionForce, new Vector3(forceField, forceField, forceField));
		myPosition = transform.position;
		myCollider = GetComponent<Collider> ();
	}
	
	void Update () {
	}
	

	void MoveColliderClosest(Collision col){
	
		Vector3 ballPosition = col.transform.position;
		Vector3 closestPointOnBounds = myCollider.ClosestPointOnBounds (ballPosition);

		Debug.DrawLine (closestPointOnBounds, Vector3.zero);
		var touch = col.gameObject.GetComponent<Rigidbody> ();
		Vector3 directToClose = Vector3.Normalize(closestPointOnBounds + ballPosition) * 10.0F;
		Debug.DrawRay (closestPointOnBounds, directToClose, new Color (255.0f, 0, 0), 100.0f);
		touch.velocity = Vector3.zero;
		touch.AddForce (directToClose, ForceMode.Force);
	}


	void OnCollisionEnter (Collision col)
	{
		MoveColliderClosest (col);
	}

	void OnCollisionStay (Collision col)
	{
		MoveColliderClosest (col);
	}


	void OnCollisionExit (Collision col)
	{
		
		var touch = col.gameObject.GetComponent<Rigidbody> ();
		touch.constraints = RigidbodyConstraints.None;//FreezePositionY;
		//touch.AddForce (directionForce, ForceMode.Impulse);;
	}
}
