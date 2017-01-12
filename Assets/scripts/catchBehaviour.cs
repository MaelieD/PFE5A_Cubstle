using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catchBehaviour : MonoBehaviour {

	public GameObject triggeredCube;

	public GameObject g_leftController;

	public bool isActive;

	wandController m_leftWandController;
	catchBehaviour m_catchBehaviour;

	// Use this for initialization
	void Start () {

		isActive = false;
		Debug.Log ("catch behaviour : " + isActive);
		m_leftWandController = g_leftController.GetComponent<wandController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isActive) {
			Debug.Log ("trigger state : " + m_leftWandController.m_triggerState);
			if (m_leftWandController.m_triggerState == (int)wandController.m_pressStates.PRESSED) {
				//Debug.Log ("triggered cube : " + triggeredCube.name);
				if (triggeredCube) {
//					Debug.Log ("Moving cube");
					triggeredCube.GetComponent<Rigidbody> ().useGravity = false;
					triggeredCube.transform.SetParent (transform);
				}
			}
			if (m_leftWandController.m_triggerState == (int)wandController.m_pressStates.UNPRESSED) {
				if (triggeredCube) {
					triggeredCube.transform.SetParent (null);
					triggeredCube.GetComponent<Rigidbody> ().useGravity = true;
					triggeredCube = null;
				}
			}
		}
	}

	void OnTriggerEnter(Collider col){

		if (col.gameObject.name == "Placed Cube") {
			Debug.Log ("Trigger enter");
			triggeredCube = col.gameObject;
		}

	}

	void OnTriggerExit(Collider col) {
		if (triggeredCube && col.gameObject.name == "Placed Cube") {
			triggeredCube = null;
		}
	}
	//quand plus en collision, triggered cube à null
	// pressed  ==> pressing et unpressed ==> idle cause pressed and unpressed se voient pas
		
}
