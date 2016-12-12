using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameController : MonoBehaviour {

	public enum modes {BUILD, PLAY};
	public Material transparentMaterial;
	public GameObject gameZonePlane;
	public GameObject startGameCanvas;
	public GameObject controllerCanvas;

	public static int g_currentMode;
	builderController m_builderController;
	catapultController m_catapultController;

	// Use this for initialization
	void Start () {

		g_currentMode = (int)modes.BUILD;
		m_builderController = GetComponent<builderController> ();
		m_catapultController = GetComponent<catapultController> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startGame(){
		gameZonePlane.GetComponent<Renderer> ().material = transparentMaterial;
		startGameCanvas.GetComponent<AudioSource> ().Play ();
		startGameCanvas.transform.position = new Vector3(startGameCanvas.transform.position.x, -100.0f, startGameCanvas.transform.position.z);
		controllerCanvas.SetActive (false);

		foreach (GameObject cube in builderController.g_cubeList) {
			if(cube != null){
				cube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
				cube.GetComponent<Rigidbody> ().useGravity = true;
			}
		}
		m_builderController.setIdleMode ();
		m_catapultController.startProjectileLaunch ();
		g_currentMode = (int)modes.PLAY;
	}
}
