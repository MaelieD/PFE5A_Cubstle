using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class builderController : MonoBehaviour {

	public enum g_modes {IDLE, WALL, REMOVE, GRAB};


	wallController m_wallController;
	removeController m_removeController;
	grabController m_grabController;
	Vector3 m_mousePos;

	public static List<GameObject> g_cubeList = new List<GameObject>();
	public static float g_cubeDistance = 3.0f;
	public static float g_cubeDistanceMin = 2.0f;
	public static float g_cubeDistanceMax = 15.0f;
	public static int g_currentMode;

	static public bool g_isPlayMode;
	static public float g_cubeHeight;

	public bool g_isDrawingWall;
	public bool g_isRemoving;

	public GameObject g_singleCubeTool;
	public GameObject g_wallTool;
	public GameObject g_removeTool;
	public GameObject g_grabTool;



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
		g_currentMode = (int) g_modes.IDLE;

		g_isPlayMode = false;

		m_wallController = g_wallTool.GetComponent<wallController> ();
		m_removeController = g_removeTool.GetComponent<removeController> ();
		m_grabController = g_grabTool.GetComponent<grabController> ();

	}
		

	// Update is called once per frame
	void Update () {

		getMousePos ();

	
		//contrôles sur les changements de mode
		if(Input.GetKeyDown("w")){
			if(g_currentMode != (int) g_modes.WALL){
				g_currentMode = (int) g_modes.WALL;
				setWallMode (true);
				setRemoveMode (false);
				setGrabMode (false);
			}

		}
		else if(Input.GetKeyDown("r")){
			if(g_currentMode != (int) g_modes.REMOVE){
				g_currentMode = (int) g_modes.REMOVE;
				setRemoveMode (true);
				setWallMode (false);
				setGrabMode (false);
			}

		}
		else if(Input.GetKeyDown("p")){
			g_isPlayMode = !g_isPlayMode;
			if(g_isPlayMode){
				setPlayMode (true);
			}
			else{
				setPlayMode (false);
			}

		}
		else if(Input.GetKeyDown("g")){
			if(g_currentMode != (int) g_modes.GRAB){
				g_currentMode = (int)g_modes.GRAB;
				setGrabMode (true);
				setWallMode (false);
				setRemoveMode (false);
			}
		}
			


		switch(g_currentMode){

		//WALL MODE
		case (int) g_modes.WALL:
			if (Input.GetMouseButtonDown (0)) {
				g_isDrawingWall = true;
				m_wallController.createWall (m_mousePos);
			}

			if (Input.GetMouseButton (0)) {
				m_wallController.drawWall (m_mousePos);
			}


			if (Input.GetMouseButtonUp (0)) {
				g_isDrawingWall = false;
				m_wallController.placeWall ();
			}

			if (!g_isDrawingWall) {
				m_wallController.moveWallTool (m_mousePos);
			}

			if(Input.GetKeyDown("e")){
				m_wallController.g_isEmpty = !m_wallController.g_isEmpty;
			}
			break;

		//REMOVE MODE
		case (int) g_modes.REMOVE:
			if (Input.GetMouseButtonDown (0)) {
				m_removeController.setIsRemoving (true);
			}

			if (Input.GetMouseButtonUp (0)) {
				m_removeController.setIsRemoving (false);
			}

			m_removeController.moveRemoveTool (m_mousePos);
			break;

		//GRAB MODE
		case (int) g_modes.GRAB:
			if (Input.GetMouseButtonDown (0)) {
				m_grabController.setIsGrabbing (true);
			}
			if (Input.GetMouseButtonUp (0)) {
				m_grabController.setIsGrabbing (false);

				if (m_grabController.g_grabbedCube) {
					m_grabController.dropCube ();
				}
			}

			m_grabController.moveGrabTool (m_mousePos);
			if (m_grabController.g_grabbedCube) {
				m_grabController.moveGrabbedCube (m_mousePos);
			}
			break;
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

		m_mousePos = mousePos;
	}

	void setWallMode(bool p_isEnter){
		if(p_isEnter){
			m_wallController.setActive (true);
		}
		else{
			m_wallController.setActive (false);
		}
	}

	void setRemoveMode(bool p_isEnter){
		if(p_isEnter){
			m_removeController.setActive (true);
		}
		else{
			m_removeController.setActive (false);
		}
	}

	void setGrabMode(bool p_isEnter){
		if(p_isEnter){
			m_grabController.setActive (true);
		}
		else{
			m_grabController.setActive (false);
		}
	}

	void setPlayMode(bool p_isEnter){
		if(p_isEnter){
			foreach(GameObject gameObject in g_cubeList){
				if(gameObject != null){
					gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
					gameObject.GetComponent<Rigidbody> ().useGravity = true;
				}

			}
		}
		else{
			foreach(GameObject gameObject in g_cubeList){
				if(gameObject != null){
					gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
					gameObject.GetComponent<Rigidbody> ().useGravity = false;
				}

			}
		}
	}
}
