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
		m_leftWandController = g_leftController.GetComponent<wandController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isActive) {
			if (m_leftWandController.m_triggerState == (int)wandController.m_pressStates.PRESSED) {
				if (triggeredCube) {
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
		
}
