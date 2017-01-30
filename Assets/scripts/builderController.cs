using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HTC.UnityPlugin.Utility;
using Valve.VR;
using UnityEngine.UI;

public class builderController : MonoBehaviour {

	public enum g_modes {IDLE, WALL, REMOVE, GRAB, COLOR};

	wallController m_wallController;
	ToolController m_toolController;
	colorController m_colorController;
	wandController m_rightWandController;
	wandController m_leftWandController;
	zoomController m_zoomController;
	Vector3 m_toolPos;

	[SerializeField]
	Text m_activeToolText;
	[SerializeField]
	public GameObject rightCanvas;
	[SerializeField]
	public GameObject leftCanvas;
	[SerializeField]
	GameObject startGameCanvas;

	[SerializeField]
	public GameObject rightControllerCanvas;

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
	public GameObject g_leftController;
	public GameObject g_rightController;
	public gameController m_gameController;

	private float touchPadAxisY;
	private float touchPadAxisX;

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
		//m_removeController = g_removeTool.GetComponent<removeController> ();
		//m_grabController = g_grabTool.GetComponent<grabController> ();
		m_toolController = GetComponent<ToolController>();
		m_colorController = GetComponent<colorController> ();
		m_zoomController = GetComponent<zoomController> ();

		m_rightWandController = g_rightController.GetComponent<wandController> ();
		m_leftWandController = g_leftController.GetComponent<wandController> ();

	}
		

	// Update is called once per frame
	void Update () {

		getToolPos ();

//		if (m_rightWandController.m_triggerState == (int)wandController.m_pressStates.PRESSED) {
//			Debug.Log ("builder controller : trigger pressed");
//		}

		if (m_rightWandController.isReady) {
			
			switch (g_currentMode) {

			//WALL MODE
			case (int) g_modes.WALL:
				if (m_rightWandController.m_padPressState == (int)wandController.m_pressStates.PRESSED) {
					g_isDrawingWall = true;
					m_wallController.createWall (m_toolPos);
				}

				if (m_rightWandController.m_padPressState == (int)wandController.m_pressStates.PRESSING) {
					m_wallController.drawWall (m_toolPos);
				}


				if (m_rightWandController.m_padPressState == (int)wandController.m_pressStates.UNPRESSED) {
					g_isDrawingWall = false;
					m_wallController.placeWall ();
				}

				if (!g_isDrawingWall) {
					m_wallController.moveWallTool (m_toolPos);
				}
				break;

			//REMOVE MODE
			case (int) g_modes.REMOVE:
				if (m_rightWandController.m_padPressState == (int)wandController.m_pressStates.PRESSING) {
					m_toolController.setIsRemoving (true);
				}

				if (m_rightWandController.m_padPressState == (int)wandController.m_pressStates.UNPRESSED) {
					m_toolController.setIsRemoving (false);
				}
				break;

			//GRAB MODE
			case (int) g_modes.GRAB:
				if (m_rightWandController.m_padPressState == (int)wandController.m_pressStates.PRESSING) {
					m_toolController.setIsGrabbing (true);
				}
				if (m_rightWandController.m_padPressState == (int)wandController.m_pressStates.UNPRESSED) {
					m_toolController.setIsGrabbing (false);

					if (m_toolController.g_grabbedCube) {
						m_toolController.dropCube ();
					}
				}

				if (m_toolController.g_grabbedCube) {
					m_toolController.moveGrabbedCube (m_toolPos);
				}
				break;

				//COLOR MODE
			case (int) g_modes.COLOR:
				if (m_rightWandController.m_padPressState == (int)wandController.m_pressStates.PRESSING) {
					m_colorController.setColor (m_rightWandController.m_padAxis.x, m_rightWandController.m_padAxis.y);
					//Debug.Log ("m_angle : " + Mathf.Rad2Deg * (Mathf.Atan2 (m_rightWandController.m_padAxis.y, m_rightWandController.m_padAxis.x)) + " - " + m_rightWandController.m_padAxis.x + " : " + m_rightWandController.m_padAxis.y);
					m_colorController.setIsPainting (true);
				}
				if (m_rightWandController.m_padPressState == (int)wandController.m_pressStates.UNPRESSED) {
					m_colorController.setIsPainting (false);
				}
				break;

			}				

			if (m_rightWandController.m_gripState == (int)wandController.m_pressStates.PRESSING) {
				m_zoomController.zoom (1);
			}

			if(m_rightWandController.m_menuState == (int)wandController.m_pressStates.PRESSED){
				m_gameController.SwitchGameMode ();
				//startGameCanvas.SetActive (!startGameCanvas.activeSelf);

				//m_activeToolText.text = "SELECTIONNER";
				//setGrabMode (true);
			}

			if (m_rightWandController.m_padTouchState == (int)wandController.m_touchStates.TOUCHING && m_rightWandController.m_padAxis.y != 0.0f) {
				g_currentCubeDistance = ((m_rightWandController.m_padAxis.y + 1.0f) * g_currentCubeDistanceMax - g_currentCubeDistanceMin) / 2.0f + g_currentCubeDistanceMin;
			//			Debug.Log ("distance : " + g_currentCubeDistance + " distanceMax : " + g_currentCubeDistanceMax + " distanceMin : " + g_currentCubeDistanceMin);
			}

			m_rightWandController.setContinuousMode ();
			
		}

		if(m_leftWandController.isReady) {
			
			touchPadAxisY = m_leftWandController.m_padAxis.y;
			touchPadAxisX = m_leftWandController.m_padAxis.x;

			if (m_leftWandController.m_padPressState == (int)wandController.m_pressStates.PRESSED) {
				if (touchPadAxisY > 0.7f)
				{
					m_activeToolText.text = "CONSTRUIRE";
					setWallMode (true);
				}

				else if (touchPadAxisY < -0.7f)
				{
					m_activeToolText.text = "GOMMER";
					setRemoveMode (true);
				}

				if (touchPadAxisX > 0.7f)
				{
					m_activeToolText.text = "SELECTIONNER";
					setGrabMode (true);

				}

				else if (touchPadAxisX < -0.7f)
				{
					m_activeToolText.text = "COLORER";
					setColorMode (true);
				}

			}	

			if (m_leftWandController.m_gripState == (int)wandController.m_pressStates.PRESSING) {
					m_zoomController.zoom (-1);
			}

			if(m_leftWandController.m_menuState == (int)wandController.m_pressStates.PRESSED){
				rightCanvas.SetActive (!rightCanvas.activeSelf);
				leftCanvas.SetActive (rightCanvas.activeSelf);
			}
			
			m_leftWandController.setContinuousMode ();
		}
	}
		

	void getToolPos(){
		Ray controllerRay = new Ray (g_rightController.transform.position, g_rightController.transform.forward);
		m_toolPos = controllerRay.GetPoint (g_currentCubeDistance);
	}

	public void setWallMode(bool p_isEnter){
		if(p_isEnter){
			g_currentMode = (int)g_modes.WALL;
			setRemoveMode (false);
			setGrabMode (false);
			setColorMode (false);
			m_wallController.setActive (true);
		}
		else{
			m_wallController.setActive (false);
		}
	}

	public void setRemoveMode(bool p_isEnter){
		if(p_isEnter){
			g_currentMode = (int)g_modes.REMOVE;
			setWallMode (false);
			setGrabMode (false);
			setColorMode (false);
			m_toolController.setActive (true);
		}
		else{
			m_toolController.setActive (false);
		}
	}

	public void setGrabMode(bool p_isEnter){
		if(p_isEnter){
			g_currentMode = (int)g_modes.GRAB;
			setWallMode (false);
			setRemoveMode (false);
			setColorMode (false);
			m_toolController.setActive (true);
		}
		else{
			m_toolController.setActive (false);
		}
	}
	
	public void setColorMode(bool p_isEnter){
		if(p_isEnter){
			g_currentMode = (int)g_modes.COLOR;
			setWallMode (false);
			setRemoveMode (false);
			setGrabMode (false);
			m_colorController.setActive (true);
		}
		else{
			m_colorController.setActive (false);
		}
	}

	public void setIdleMode(){
		g_currentMode = (int)g_modes.IDLE;
		setWallMode (false);
		setRemoveMode (false);
		setGrabMode (false);
		setColorMode (false);
	}
		
}
