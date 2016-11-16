using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cubeBehaviour : MonoBehaviour {

	int m_materialIndex;
	public bool g_isGrabbed;
	public bool g_isColliding;
	public List<Material> g_materialList;


	// Use this for initialization
	void Start () {
		g_isColliding = false;
		g_isGrabbed = false;
		m_materialIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {

		foreach(GameObject cube in builderController.g_cubeList){
			Vector3 cubeToCurrentCubeVec = cube.transform.position - transform.position;
			if(cube){
				if(cubeToCurrentCubeVec.x != 0.0f){
					cubeToCurrentCubeVec.x = 1.0f / (cubeToCurrentCubeVec.x * 10000.0f);
				}
				if(cubeToCurrentCubeVec.y != 0.0f){
					cubeToCurrentCubeVec.y = 0.0f / (cubeToCurrentCubeVec.y * 10000.0f);
				}
				if(cubeToCurrentCubeVec.z != 0.0f){
					cubeToCurrentCubeVec.z = 1.0f / (cubeToCurrentCubeVec.z * 10000.0f);
				}

				GetComponent<Rigidbody> ().AddForce (cubeToCurrentCubeVec);
			}

		}
	
	}

	void OnCollisionStay(Collision col){

		int materialIndex = 0;

		switch(col.gameObject.name){

		case "Remove Tool":
			materialIndex = 2;
			break;
		case "Grab Tool":
			materialIndex = 3;
			break;
		default:
			if (g_isGrabbed) {
				materialIndex = 1;
			}

			g_isColliding = true;
			break;
		}
		if(m_materialIndex != materialIndex){
			GetComponent<Renderer> ().material = g_materialList [materialIndex];
		}


	}

	void OnCollisionExit(Collision col){
		GetComponent<Renderer> ().material = g_materialList [0];
		g_isColliding = false;
	}
		
}
