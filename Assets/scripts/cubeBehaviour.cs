using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cubeBehaviour : MonoBehaviour {

	public bool g_isGrabbed;
	public bool g_isColliding;

	// Use this for initialization
	void Start () {
		g_isColliding = false;
		g_isGrabbed = false;
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
		g_isColliding = true;
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
		g_isColliding = false;
	}
		
		
}
