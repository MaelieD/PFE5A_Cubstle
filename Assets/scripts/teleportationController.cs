using UnityEngine;
using System.Collections;

public class teleportationController : MonoBehaviour {

	wandController m_leftWandController;
	ReticlePoser m_reticlePoser;

	public GameObject g_leftController;
	public GameObject g_reticle;
	public GameObject g_reticleSphere;

	// Use this for initialization
	void Start () {

		m_leftWandController = g_leftController.GetComponent<wandController> ();
		m_reticlePoser = g_reticle.GetComponent<ReticlePoser> ();
	
	}
	
	// Update is called once per frame
	void Update () {

//		Debug.Log("trigger state : " + m_leftWandController.m_triggerState);
	
		if (m_leftWandController.isReady) {
//			Debug.Log ("trigger state : " + m_leftWandController.m_triggerState + " target : " + m_reticlePoser.hitTarget.name);

			if (m_leftWandController.m_triggerState == (int)wandController.m_pressStates.PRESSED && m_reticlePoser.hitTarget.name == "Game Zone Plane") {
//				Debug.Log ("teleport");
				transform.position = g_reticleSphere.transform.position;
			}
		}

		m_leftWandController.setContinuousMode ();
			
	}
}
