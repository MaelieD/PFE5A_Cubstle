﻿using UnityEngine;
using System.Collections;

public class singleCubeController: ScriptableObject {

	static public GameObject g_currentCube;
	static public Rigidbody g_currentRigidbody;
	static public GameObject g_unitCube;

	// Use this for initialization
	static public void init () {
	
		//cube unitaire sur lequel on clone les blocs
		g_unitCube = GameObject.Find ("Unit Cube");
	}
	


	//fonction de création des cubes, à l'endroit du clic souris
	//on annule la gravité pour pouvoir déplacer le bloc à sa guise ensuite
	static public void createCube(Vector3 p_pos){

		g_currentCube = Instantiate (g_unitCube);

		g_currentCube.name = "Current Cube";
		g_currentRigidbody = g_currentCube.GetComponent<Rigidbody> ();
		g_currentRigidbody.MovePosition (p_pos);
		g_currentRigidbody.useGravity = false;
	}

	//fonction qui gère le déplacement des blocs
	//pour cela, on applique une force de déplacement, et une force de frottement pour éliminer les rebonds
	static public void moveCube(Vector3 p_pos){
		Vector3 cubeToMouseVector = p_pos - g_currentCube.transform.position;

		g_currentRigidbody.AddForce (cubeToMouseVector, ForceMode.VelocityChange);
		g_currentRigidbody.AddForce (- g_currentRigidbody.velocity, ForceMode.VelocityChange);

	}

	//fonction de placement des cubes
	//on déclare le cube placé, et on lui redonne sa gravité
	static public void placeCube(){
		g_currentCube.name = "Placed Cube";
		cubeBehaviour cBehaviour = g_currentCube.GetComponent<cubeBehaviour> ();
		cBehaviour.g_isPlaced = true;
		g_currentRigidbody.useGravity = true;


	}
}
