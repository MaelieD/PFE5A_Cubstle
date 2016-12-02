using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HTC.UnityPlugin.Utility;
using Valve.VR;

public class builderController : MonoBehaviour {

	public enum g_modes {IDLE, WALL, REMOVE, GRAB};
	enum m_pressStates {PRESSED, PRESSING, UNPRESSED, IDLE};
	enum m_touchStates {TOUCHED, TOUCHING, UNTOUCHED, IDLE};

	int m_triggerState;
	int m_menuState;
	int m_padPressState;
	int m_padTouchState;

	wallController m_wallController;
	removeController m_removeController;
	grabController m_grabController;
	wandController m_wandController;
	zoomController m_zoomController;
	Vector3 m_mousePos;
	Vector3 m_toolPos;


	public static List<GameObject> g_cubeList = new List<GameObject>();
	public static float g_cubeDistance = 3.0f;
	public static float g_cubeDistanceMin = 2.0f;
	public static float g_cubeDistanceMax = 15.0f;
	public static float g_currentCubeDistance = 3.0f;
	public static float g_currentCubeDistanceMin = 2.0f;
	public static float g_currentCubeDistanceMax = 15.0f;
	public static int g_currentMode;

	static public bool g_isPlayMode;
	static public float g_cubeHeight;

	public bool g_isDrawingWall;
	public bool g_isRemoving;

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

		m_triggerState = (int)m_pressStates.IDLE;
		m_menuState = (int)m_pressStates.IDLE;
		m_padPressState = (int)m_pressStates.IDLE;

		g_isPlayMode = false;

		m_wallController = g_wallTool.GetComponent<wallController> ();
		m_removeController = g_removeTool.GetComponent<removeController> ();
		m_grabController = g_grabTool.GetComponent<grabController> ();
		m_wandController = GetComponent<wandController> ();
		m_zoomController = GetComponent<zoomController> ();


	}
		

	// Update is called once per frame
	void Update () {

		getMousePos ();
		getToolPos ();

	
		if(m_menuState == (int)m_pressStates.PRESSED){
			Debug.Log(g_currentMode);
			switch (g_currentMode) {

			case (int)g_modes.IDLE:
				g_currentMode = (int)g_modes.WALL;
				setWallMode (true);
				setRemoveMode (false);
				setGrabMode (false);
				break;

			case (int)g_modes.WALL:
				g_currentMode = (int)g_modes.REMOVE;
				setRemoveMode (true);
				setWallMode (false);
				setGrabMode (false);
				break;

			case (int)g_modes.REMOVE:
				g_currentMode = (int)g_modes.GRAB;
				setGrabMode (true);
				setWallMode (false);
				setRemoveMode (false);
				break;
			
			case (int)g_modes.GRAB:
				g_currentMode = (int)g_modes.IDLE;
				setWallMode (false);
				setRemoveMode (false);
				setGrabMode (false);
				break;
			}
		}
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
			if (Input.GetMouseButtonDown (0) || m_triggerState == (int)m_pressStates.PRESSED) {
				g_isDrawingWall = true;
				m_wallController.createWall (m_toolPos);
			}

			if (Input.GetMouseButton (0) || m_triggerState == (int)m_pressStates.PRESSING) {
				m_wallController.drawWall (m_toolPos);
			}


			if (Input.GetMouseButtonUp (0) || m_triggerState == (int)m_pressStates.UNPRESSED) {
				g_isDrawingWall = false;
				m_wallController.placeWall ();
			}

			if (!g_isDrawingWall) {
				m_wallController.moveWallTool (m_toolPos);
			}

			if(Input.GetKeyDown("e")){
				m_wallController.g_isEmpty = !m_wallController.g_isEmpty;
			}
			break;

		//REMOVE MODE
		case (int) g_modes.REMOVE:
			if (Input.GetMouseButtonDown (0) || m_triggerState == (int)m_pressStates.PRESSED) {
				m_removeController.setIsRemoving (true);
			}

			if (Input.GetMouseButtonUp (0) || m_triggerState == (int)m_pressStates.UNPRESSED) {
				m_removeController.setIsRemoving (false);
			}

			m_removeController.moveRemoveTool (m_toolPos);
			break;

		//GRAB MODE
		case (int) g_modes.GRAB:
			if (Input.GetMouseButtonDown (0) || m_triggerState == (int)m_pressStates.PRESSED) {
				m_grabController.setIsGrabbing (true);
			}
			if (Input.GetMouseButtonUp (0) || m_triggerState == (int)m_pressStates.UNPRESSED) {
				m_grabController.setIsGrabbing (false);

				if (m_grabController.g_grabbedCube) {
					m_grabController.dropCube ();
				}
			}

			m_grabController.moveGrabTool (m_toolPos);
			if (m_grabController.g_grabbedCube) {
				m_grabController.moveGrabbedCube (m_toolPos);
			}
			break;
		}

		//Avec la molette, modification de la distance joueur/outil
		if(Input.GetAxis("Mouse ScrollWheel") > 0.0f){
			g_currentCubeDistance++;
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0.0f){
			g_currentCubeDistance--;
		}
			
		float touchPadAxisY = m_wandController.GetTouchpadAxis ().y;

		if (m_padPressState == (int)m_pressStates.PRESSING) {
			if (touchPadAxisY > 0.0f) {
				m_zoomController.zoom (1);
			}
			if (touchPadAxisY < 0.0f) {
				m_zoomController.zoom (-1);
			}
		} else if(m_padTouchState == (int)m_touchStates.TOUCHING && touchPadAxisY != 0.0f) {
			g_currentCubeDistance = ((touchPadAxisY + 1.0f) * g_currentCubeDistanceMax - g_currentCubeDistanceMin) / 2.0f + g_currentCubeDistanceMin;
			Debug.Log ("distance : " + g_currentCubeDistance + " distanceMax : " + g_currentCubeDistanceMax + " distanceMin : " + g_currentCubeDistanceMin);
		}

		//on clampe la distance pour éviter de perdre le cube ou de trop le rapprocher de nous
//		g_currentCubeDistance = Mathf.Clamp (g_currentCubeDistance, g_currentCubeDistanceMin, g_currentCubeDistanceMax);

		//on défini les états des contrôles sur la durée en fonction du fait qu'ils soient pressés ou non
		//pressed sur la durée = pressing
		//unpressed sur la durée = idle

		m_triggerState = setContinuousPressState (m_triggerState);
		m_menuState = setContinuousPressState (m_menuState);
		m_padPressState = setContinuousPressState (m_padPressState);
		m_padTouchState = setContinousTouchState (m_padTouchState);

	}

	int setContinuousPressState(int buttonState){
		if (buttonState == (int)m_pressStates.PRESSED) {
			buttonState = (int)m_pressStates.PRESSING;
		} else if (buttonState == (int)m_pressStates.UNPRESSED) {
			
			buttonState = (int)m_pressStates.IDLE;
		}

		return buttonState;
	}

	int setContinousTouchState(int buttonState){
		if (buttonState == (int)m_touchStates.TOUCHED) {
			buttonState = (int)m_touchStates.TOUCHING;
		} else if (buttonState == (int)m_touchStates.UNTOUCHED) {
			buttonState = (int)m_touchStates.IDLE;
		}

		return buttonState;
	}
		

	//Renvoie ce que pointe la souris dans les coordonnées du monde
	//on définit manuellement la distance joueur/bloc
	//si la hauteur est trop basse, on a rehausse pour éviter de déplacer ou créer un bloc en intersection avec le sol
	void getMousePos(){
		var mousePos = Input.mousePosition;
		mousePos.z = g_currentCubeDistance;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);

		m_mousePos = mousePos;
	}

	void getToolPos(){
		Ray controllerRay = new Ray (transform.position, transform.forward);
		m_toolPos = controllerRay.GetPoint (g_currentCubeDistance);
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

	public void setTriggerClicked(bool isClicked){
		if (isClicked) {
			m_triggerState = (int)m_pressStates.PRESSED;
		} else {
			m_triggerState = (int)m_pressStates.UNPRESSED;
		}
	}

	public void setMenuClicked(bool isClicked){
		if (isClicked) {
			m_menuState = (int)m_pressStates.PRESSED;
		} else {
			m_menuState = (int)m_pressStates.UNPRESSED;
		}
	}

	public void setPadClicked(bool isClicked){
		if (isClicked) {
			m_padPressState = (int)m_pressStates.PRESSED;
		} else {
			m_padPressState = (int)m_pressStates.UNPRESSED;
		}
	}

	public void setPadTouched(bool isTouched){
		if (isTouched) {
			m_padTouchState = (int)m_touchStates.TOUCHED;
		} else {
			m_padTouchState = (int)m_touchStates.UNTOUCHED;
		}
	}
		
}
