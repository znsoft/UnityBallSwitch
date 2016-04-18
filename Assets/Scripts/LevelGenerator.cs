using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class LevelGenerator : MonoBehaviour {
	public GameObject playerBall;
	public GameObject botBalls;
	GameObject playerBalls;
	public GameObject myCamera;
	Rigidbody botRigidBody;
	public GameObject[] prefabs;
	Vector3 startPoint;
	public GameObject[] platforms; 
	
	 List<Vector3> verts = new List<Vector3>(); // вершины
	 List<int> tris = new List<int>(); 			//треугольники
	 List<Vector2> UVs = new List<Vector2>(); 	//развертка
	protected MeshRenderer meshRenderer;
	protected MeshFilter meshFilter;
	protected MeshCollider meshCollider;
	protected Mesh mesh;
	// Use this for initialization
	void Start (){
		meshRenderer = GetComponent<MeshRenderer>();
		meshFilter = GetComponent<MeshFilter>();
		meshCollider = GetComponent<MeshCollider>();
		//mesh = meshFilter.mesh;
		mesh = new Mesh();
		meshFilter.sharedMesh = mesh;
		//startPoint = botBalls.transform.position;
		botRigidBody = botBalls.GetComponent<Rigidbody> ();
		FaceQuad (transform.position,  Vector3.forward*10, Vector3.right*10);
		//gameObject.transform.position = botBalls.transform.position;
		this.StartCoroutine ("RepeatAction", botBalls);
	}
	
	// Update is called once per frame
	void Update() {

	}

	public void GenerateMesh(Vector3 start, Vector3 off1)
	{

		FaceQuad(start, off1);
		mesh.vertices = verts.ToArray();
		mesh.uv = UVs.ToArray();
		mesh.triangles = tris.ToArray();
		meshCollider.sharedMesh = null;
		meshCollider.sharedMesh = mesh;
		mesh.RecalculateNormals();
		mesh.Optimize();
	}


	//отрисовка клетки меша
	void FaceQuad(Vector3 start, Vector3 off1, Vector3 off2)
	{
		int index = verts.Count;
		
		verts.Add(start);
		verts.Add(start + off2);
		verts.Add(start + off1);
		verts.Add(start + off1 + off2);
		
		//развертка
		UVs.Add(new Vector2(0, 0));
		UVs.Add(new Vector2(0, 1));
		UVs.Add(new Vector2(1, 0));
		UVs.Add(new Vector2(1, 1));
		
		//треугольники
		tris.Add(index + 0);
		tris.Add(index + 1);
		tris.Add(index + 2);
		//triangle 2
		tris.Add(index + 3);
		tris.Add(index + 2);
		tris.Add(index + 1);
	}


	//отрисовка клетки меша
	void FaceQuad(Vector3 start, Vector3 off1)
	{
		int index = verts.Count;
		
		verts.Add(start);
		verts.Add(start + off1);

		//развертка
		UVs.Add(new Vector2(0, 0));
		UVs.Add(new Vector2(0, 1));
		UVs.Add(new Vector2(1, 0));
		UVs.Add(new Vector2(1, 1));
		
		//треугольники
		tris.Add(index - 1);
		tris.Add(index );
		tris.Add(index - 2);

		tris.Add(index + 1);
		tris.Add(index );
		tris.Add(index - 1);
	}

	
	IEnumerator RepeatAction ( GameObject botBall)
	{
		Vector3 futurePosition = CalculateFuturePosition ();
		Vector3 fPosition = transform.InverseTransformPoint(botBall.transform.position + futurePosition);
		GenerateMesh(fPosition , Vector3.right*10);
		yield return new WaitForSeconds (1);
		this.StartCoroutine ("RepeatAction", botBall);
	}

	Vector3 CalculateFuturePosition ()
	{
		Vector3 botVelocity = botRigidBody.velocity;
		var gravity = Physics.gravity;
		float t = 1;//forecast time
		return botVelocity * t + (gravity * t * t) / 2;
	}

}
