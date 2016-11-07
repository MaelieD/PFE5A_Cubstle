using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class builderController : MonoBehaviour {
	
	enum m_modes {SINGLE_CUBE, WALL, REMOVE, PLAY};
	int m_currentMode;

	public float g_cubeDistance;
	public bool g_isDrawingWall;
	public bool g_isRemoving;

	static public float g_cubeHeight;

	// Use this for initialization
	void Start () {



		//hauteur d'un bloc
		g_cubeHeight = 1.0f;

		//distance entre le joueur et le cube
		g_cubeDistance = 3.0f;

		//détermine si l'on est en train de dessiner un mur
		g_isDrawingWall = false;

		//détermine si l'on est en train de supprimer des blocs
		g_isRemoving = false;

		//sert à décrire l'état dans lequel le builder se trouve
		m_currentMode = (int) m_modes.SINGLE_CUBE;

		wallController.init ();
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 mousePos = getMousePos ();

		//contrôles sur les changements de mode
		if (Input.GetKeyDown ("s")) {
			if(m_currentMode != (int) m_modes.SINGLE_CUBE){
				m_currentMode = (int) m_modes.SINGLE_CUBE;
				enterSingleCubeMode ();
				exitWallMode ();
				exitRemoveMode ();
				exitPlayMode ();
			}

		}
		else if(Input.GetKeyDown("w")){
			if(m_currentMode != (int) m_modes.WALL){
				m_currentMode = (int) m_modes.WALL;
				enterWallMode ();
				exitSingleCubeMode ();
				exitRemoveMode ();
				exitPlayMode ();
			}

		}
		else if(Input.GetKeyDown("r")){
			if(m_currentMode != (int) m_modes.REMOVE){
				m_currentMode = (int) m_modes.REMOVE;
				enterRemoveMode ();
				exitSingleCubeMode ();
				exitWallMode ();
				exitPlayMode ();
			}

		}
		else if(Input.GetKeyDown("p")){
			if(m_currentMode != (int) m_modes.PLAY){
				m_currentMode = (int) m_modes.PLAY;
				enterPlayMode ();
				exitSingleCubeMode ();
				exitWallMode ();
				exitRemoveMode ();
			}

		}

		//SINGLE CUBE MODE
		if(m_currentMode == (int) m_modes.SINGLE_CUBE){
			
			if(Input.GetMouseButtonDown(0)){
				singleCubeController.createCube (mousePos);
			}

			if(Input.GetMouseButton(0)){
				singleCubeController.moveCube (mousePos);
			}

			if(Input.GetMouseButtonUp(0)){
				singleCubeController.placeCube ();
			}
		}

		//WALL MODE
		if(m_currentMode == (int) m_modes.WALL){
			
			if(Input.GetMouseButtonDown(0)){
				g_isDrawingWall = true;
				wallController.createWall (mousePos);
			}

			if(Input.GetMouseButton(0)){
				wallController.drawWall (mousePos);
			}


			if(Input.GetMouseButtonUp(0)){
				g_isDrawingWall = false;
				wallController.placeWall ();
			}

			if(!g_isDrawingWall){
				wallController.moveWallStart (mousePos);
			}
		}

		//REMOVE MODE
		if(m_currentMode == (int) m_modes.REMOVE){

			if(Input.GetMouseButtonDown(0)){
				removeController.setIsRemoving (true);
			}

			if(Input.GetMouseButtonUp(0)){
				removeController.setIsRemoving (false);
			}

			removeController.moveRemoveTool (mousePos);
		}

		//PLAY MODE
		if(m_currentMode == (int) m_modes.PLAY){

		}
			

		//Avec la molette, modification de la distance joueur/outil
		if(Input.GetAxis("Mouse ScrollWheel") > 0.0f){
			g_cubeDistance++;
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0.0f){
			g_cubeDistance--;
		}

		//on clampe la distance pour éviter de perdre le cube ou de trop le rapprocher de nous
		g_cubeDistance = Mathf.Clamp (g_cubeDistance, 2.0f, 15.0f);
	
	}



	//Renvoie ce que pointe la souris dans les coordonnées du monde
	//on définit manuellement la distance joueur/bloc
	//si la hauteur est trop basse, on a rehausse pour éviter de déplacer ou créer un bloc en intersection avec le sol
	Vector3 getMousePos(){
		var mousePos = Input.mousePosition;
		mousePos.z = g_cubeDistance;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		if(mousePos.y < g_cubeHeight / 2){
			mousePos.y = g_cubeHeight / 2;
		}
		return mousePos;
	}
		



	void enterSingleCubeMode(){
		
	}

	void enterWallMode(){
		wallController.enterWallMode ();
		
	}

	void enterRemoveMode(){

	}

	//fonction pour entrer dans le mode play
	//on annule toutes les contraintes sur les blocs
	void enterPlayMode(){
		Debug.Log ("entered Play Mode");

		foreach(GameObject gameObject in singleCubeController.g_cubeList){
			if(gameObject != null){
				gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			}

		}

	}

	void exitSingleCubeMode(){
		
	}

	void exitWallMode(){
		wallController.exitWallMode ();
	}

	void exitRemoveMode(){
		
	}

	void exitPlayMode(){
		
	}
		
		
}
