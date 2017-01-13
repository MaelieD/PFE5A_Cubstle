﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cubeBehaviour : MonoBehaviour {

	[SerializeField]
	public Renderer rend;
	[SerializeField]
	Material brokenMaterial;
	[SerializeField]
	Material grabMaterial;
	Material normalMaterial;

	//int m_materialIndex;
	public bool g_isGrabbed;
	public bool g_isColliding;
	//public List<Material> g_materialList;

	// Use this for initialization
	void Start () {
		g_isColliding = false;
		g_isGrabbed = false;
		normalMaterial = rend.material;
		//m_materialIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {

//		foreach(GameObject cube in builderController.g_cubeList){
//			Vector3 cubeToCurrentCubeVec = cube.transform.position - transform.position;
//			float distance = cubeToCurrentCubeVec.magnitude;
//			if(cube.activeSelf && distance != 0.0f && distance < 1.0f){
//				cubeToCurrentCubeVec.Normalize ();
//
//				m_rigidbody.AddForce (2 * (1 / Mathf.Pow(2, distance)) * cubeToCurrentCubeVec);
//			}
//
//		}
	
	}

	void OnCollisionStay(Collision col){

		//int materialIndex = 0;

		switch(col.gameObject.name){

		case "Remove Tool":
			rend.material = brokenMaterial;//materialIndex = 2;
			break;
		case "Grab Tool":
			rend.material = grabMaterial;//materialIndex = 3;
			break;

//		case "Placed Cube":
//			Vector3 cubeToCurrentCubeVec = col.gameObject.transform.position - transform.position;
//			float distance = cubeToCurrentCubeVec.magnitude;
//
//			cubeToCurrentCubeVec.Normalize ();
//			GetComponent<Rigidbody> ().AddForce (cubeToCurrentCubeVec);
//			break;

		default:
			if (g_isGrabbed) {
				rend.material = normalMaterial;//materialIndex = 1;
			}

			g_isColliding = true;
			break;
		}
		/*if(m_materialIndex != materialIndex){
			rend.material = g_materialList [materialIndex];
		}*/


	}

//	void OnCollisionEnter(Collision col){
//		if(col.gameObject.name == "Placed Cube"){
//			Vector3 cubeToCurrentCubeVec = col.gameObject.transform.position - transform.position;
//
//			cubeToCurrentCubeVec.Normalize ();
//			m_rigidbody.AddForce (cubeToCurrentCubeVec);
//		}
//	}



	void OnCollisionExit(Collision col){
		//rend.material = g_materialList [0];
		rend.material = normalMaterial;
			
		g_isColliding = false;
	}
		
		
}
