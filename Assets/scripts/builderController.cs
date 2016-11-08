using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class builderController : MonoBehaviour {

	enum m_modes {SINGLE_CUBE, WALL, REMOVE, PLAY};
	int m_currentMode;

	singleCubeController m_singleCubeController;
	wallController m_wallController;
	removeController m_removeController;
	Vector3 m_mousePos;
	float m_floorDistanceMin = 1.0f;

	public static float g_cubeDistance = 3.0f;
	public static float g_cubeDistanceMin = 2.0f;
	public static float g_cubeDistanceMax = 15.0f;

	public bool g_isDrawingWall;
	public bool g_isRemoving;

	public GameObject g_singleCubeTool;
	public GameObject g_wallTool;
	public GameObject g_removeTool;

	static public float g_cubeHeight;

	// Use this for initialization
	void Start () {

		//hauteur d'un bloc
		g_cubeHeight = 1.0f;

		//distance entre le joueur et le cube
		//g_cubeDistance = 3.0f;

		//détermine si l'on est en train de dessiner un mur
		g_isDrawingWall = false;

		//détermine si l'on est en train de supprimer des blocs
		g_isRemoving = false;

		//sert à décrire l'état dans lequel le builder se trouve
		m_currentMode = (int) m_modes.SINGLE_CUBE;

		m_singleCubeController = g_singleCubeTool.GetComponent<singleCubeController> ();
		m_wallController = g_wallTool.GetComponent<wallController> ();
		m_removeController = g_removeTool.GetComponent<removeController> ();

	}
		

	// Update is called once per frame
	void Update () {

		getMousePos ();

	
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
				m_singleCubeController.setActive (false);
				m_singleCubeController.createCube (m_mousePos);
			}


			m_singleCubeController.moveSingleCubeTool (m_mousePos);


			if(Input.GetMouseButtonUp(0)){
				m_singleCubeController.placeCube ();
				m_singleCubeController.setActive (true);
			}
		}

		//WALL MODE
		if(m_currentMode == (int) m_modes.WALL){

			if(Input.GetMouseButtonDown(0)){
				g_isDrawingWall = true;
				m_wallController.createWall (m_mousePos);
			}

			if(Input.GetMouseButton(0)){
				m_wallController.drawWall (m_mousePos);
			}


			if(Input.GetMouseButtonUp(0)){
				g_isDrawingWall = false;
				m_wallController.placeWall ();
			}

			if(!g_isDrawingWall){
				m_wallController.moveWallStart (m_mousePos);
			}
		}

		//REMOVE MODE
		if(m_currentMode == (int) m_modes.REMOVE){

			if(Input.GetMouseButtonDown(0)){
				m_removeController.setIsRemoving (true);
			}

			if(Input.GetMouseButtonUp(0)){
				m_removeController.setIsRemoving (false);
			}

			m_removeController.moveRemoveTool (m_mousePos);
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
		g_cubeDistance = Mathf.Clamp (g_cubeDistance, g_cubeDistanceMin, g_cubeDistanceMax);

	}



	//Renvoie ce que pointe la souris dans les coordonnées du monde
	//on définit manuellement la distance joueur/bloc
	//si la hauteur est trop basse, on a rehausse pour éviter de déplacer ou créer un bloc en intersection avec le sol
	void getMousePos(){
		var mousePos = Input.mousePosition;
		mousePos.z = g_cubeDistance;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);

//		if(mousePos.y < g_cubeHeight / 2){
//			mousePos.y = g_cubeHeight / 2;
//			Vector3 playerToMouseVec = (mousePos - transform.position);
//			float coef = g_cubeDistanceMin / playerToMouseVec.magnitude;
//
//			mousePos = transform.position + playerToMouseVec * coef;
//			mousePos.y = g_cubeHeight / 2;
//		}

		m_mousePos = mousePos;
	}



	void enterSingleCubeMode(){
		m_singleCubeController.setActive (true);
	}

	void enterWallMode(){
		m_wallController.setActive (true);

	}

	void enterRemoveMode(){
		m_removeController.setActive (true);
	}

	//fonction pour entrer dans le mode play
	//on annule toutes les contraintes sur les blocs
	void enterPlayMode(){
		Debug.Log ("entered Play Mode");

		foreach(GameObject gameObject in m_singleCubeController.g_cubeList){
			if(gameObject != null){
				gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			}

		}

	}

	void exitSingleCubeMode(){
		m_singleCubeController.setActive (false);
	}

	void exitWallMode(){
		m_wallController.setActive (false);
	}

	void exitRemoveMode(){
		m_removeController.setActive (false);
	}

	void exitPlayMode(){

	}


}
