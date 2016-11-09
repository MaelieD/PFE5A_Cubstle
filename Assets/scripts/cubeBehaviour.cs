using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cubeBehaviour : MonoBehaviour {

	public bool g_isGrabbed;
	public bool g_isColliding;
	public List<Material> g_materialList;


	// Use this for initialization
	void Start () {
		g_isColliding = false;
		g_isGrabbed = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){

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
		GetComponent<Renderer> ().material = g_materialList [materialIndex];
	}

	void OnCollisionExit(Collision col){
		GetComponent<Renderer> ().material = g_materialList [0];
		g_isColliding = false;
	}
		
}
