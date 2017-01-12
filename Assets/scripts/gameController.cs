using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

	[SerializeField]
	public Renderer rendGameZonePlane;
	[SerializeField]
	public AudioSource audioSourceStartGameCanvas;

	public enum modes {BUILD, PLAY};
	public Material transparentMaterial;
	public GameObject startGameCanvas;
	public GameObject controllerCanvas;
	public GameObject gameCanvas;
	public GameObject leftModel;

	public Text timeText;

	public static int g_currentMode;
	builderController m_builderController;
	catapultController m_catapultController;
	catchBehaviour m_catchBehaviour;

	float startTime;

	// Use this for initialization
	void Start () {

		g_currentMode = (int)modes.BUILD;
		m_builderController = GetComponent<builderController> ();
		m_catapultController = GetComponent<catapultController> ();
		m_catchBehaviour = leftModel.GetComponent<catchBehaviour> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (g_currentMode == (int)modes.PLAY) {
			float currentTime = Time.time - startTime;
			int minutes = (int)currentTime / 60;
			int secondes = (int)currentTime % 60;

			timeText.text = "Temps : " + minutes + ":" + secondes.ToString("D2");
		}

	}

	public void startGame(){
		// if text button == Play
		//Button.text = "Build" 
		rendGameZonePlane.material = transparentMaterial;
		audioSourceStartGameCanvas.Play ();
		startGameCanvas.transform.position = new Vector3(startGameCanvas.transform.position.x, -100.0f, startGameCanvas.transform.position.z);
		controllerCanvas.SetActive (false);
		gameCanvas.SetActive (true);

		foreach (GameObject cube in builderController.g_cubeList) {
			if(cube != null){
				cube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
				cube.GetComponent<Rigidbody> ().useGravity = true;
			}
		}
		m_builderController.setIdleMode ();
		m_catapultController.startProjectileLaunch ();
		g_currentMode = (int)modes.PLAY;
		startTime = Time.time;

		m_catchBehaviour.isActive = true;
		Debug.Log ("game controller :" + m_catchBehaviour.isActive);

		// if text button == Build
		//Button.text = "Play" et faire inverse de ci-dessous  
	}


}
