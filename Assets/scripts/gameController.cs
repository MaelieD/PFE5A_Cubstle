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

	// Use this for initialization
	void Start () {

		g_currentMode = (int)modes.BUILD;
		m_builderController = GetComponent<builderController> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startGame(){
		gameZonePlane.GetComponent<Renderer> ().material = transparentMaterial;
		startGameCanvas.SetActive (false);
		controllerCanvas.SetActive (false);

		foreach (GameObject cube in builderController.g_cubeList) {
			if(cube != null){
				cube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
				cube.GetComponent<Rigidbody> ().useGravity = true;
			}
		}
		m_builderController.setIdleMode ();
		g_currentMode = (int)modes.PLAY;
	}
}
