﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour {

	[SerializeField]
	public Renderer rendGameZonePlane;
	[SerializeField]
	public AudioSource audioSourceStartGameCanvas;

	[SerializeField]
	public GameObject m_skeletor;

	[SerializeField]
	public GameObject m_flag;

	public enum modes {BUILD, PLAY};
	public Material transparentMaterial;
	public GameObject startGameCanvas;
	//public GameObject controllerCanvas;
	public GameObject gameCanvas;
	public GameObject leftModel;

	public Text timeText;

	public static int g_currentMode;
	builderController m_builderController;
	catapultController m_catapultController;
	enemyController m_enemyController;
	enemySpawner m_enemySpawner;
	catchBehaviour m_catchBehaviour;

	float startTime;

	// Use this for initialization
	void Start () {

		g_currentMode = (int)modes.BUILD;
		m_builderController = GetComponent<builderController> ();
		m_catapultController = GetComponent<catapultController> ();
		m_catchBehaviour = leftModel.GetComponent<catchBehaviour> ();
		m_enemySpawner = GetComponent<enemySpawner> ();
	
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

	public void SwitchGameMode(){
		if (g_currentMode == (int)modes.BUILD) {
			//rendGameZonePlane.material = transparentMaterial;
			audioSourceStartGameCanvas.Play ();
			startGameCanvas.transform.position = new Vector3 (startGameCanvas.transform.position.x, -100.0f, startGameCanvas.transform.position.z);
			//controllerCanvas.SetActive (false);
			gameCanvas.SetActive (true);

			foreach (GameObject cube in builderController.g_cubeList) {
				if (cube != null) {
					cube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
					cube.GetComponent<Rigidbody> ().useGravity = true;
				}
			}
			m_builderController.setIdleMode ();
			m_catapultController.startProjectileLaunch ();
			m_enemySpawner.startEnemy ();
			g_currentMode = (int)modes.PLAY;
			startTime = Time.time;
			m_flag.GetComponent<CapsuleCollider> ().enabled = true;

			m_builderController.leftCanvas.SetActive (false);
			m_builderController.rightCanvas.SetActive (false);
			m_builderController.rightControllerCanvas.SetActive (false);
			m_builderController.setIdleMode ();

			//m_catchBehaviour.isActive = true;
			//Debug.Log ("game controller :" + m_catchBehaviour.isActive);		
		} else {
			//startGameCanvas.transform.position = new Vector3 (startGameCanvas.transform.position.x, -100.0f, startGameCanvas.transform.position.z);
			gameCanvas.SetActive (false);

			foreach (GameObject cube in builderController.g_cubeList) {
				if (cube != null) {
					cube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
					cube.GetComponent<Rigidbody> ().useGravity = false;
				}
			}
			m_catapultController.stopProjectileLaunch ();
			m_catapultController.flushProjectileList ();
			m_enemySpawner.stopEnemy ();
			g_currentMode = (int)modes.BUILD;
			startTime = 0.0f;

			m_flag.GetComponent<CapsuleCollider> ().enabled = true;

			m_builderController.leftCanvas.SetActive (true);
			m_builderController.rightCanvas.SetActive (true);
			m_builderController.rightControllerCanvas.SetActive (true);
			m_builderController.setIdleMode ();

			//m_catchBehaviour.isActive = false;
			//Debug.Log ("game controller :" + m_catchBehaviour.isActive);	
		}
	}

	public void ExitGame() {
		Application.Quit();
	}

	public void LaunchMenu() {
		SceneManager.LoadScene ("menu");
	}

}
