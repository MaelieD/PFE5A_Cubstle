using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HTC.UnityPlugin.Utility;
using Valve.VR;

public class builderController : MonoBehaviour {

	public enum g_modes {IDLE, WALL, REMOVE, GRAB};

	wallController m_wallController;
	removeController m_removeController;
	grabController m_grabController;
	wandController m_leftWandController;
	wandController m_rightWandController;
	zoomController m_zoomController;
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
	public GameObject g_leftController;
	public GameObject g_rightController;



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
		m_zoomController = GetComponent<zoomController> ();

		m_leftWandController = g_leftController.GetComponent<wandController> ();
		m_rightWandController = g_rightController.GetComponent<wandController> ();

	}
		

	// Update is called once per frame
	void Update () {

		getToolPos ();

//		if (m_rightWandController.m_triggerState == (int)wandController.m_pressStates.PRESSED) {
//			Debug.Log ("builder controller : trigger pressed");
//		}

		Debug.Log ("builder Controller : trigger state " + m_rightWandController.m_triggerState);

		if (m_rightWandController.isReady) {
			
			
			switch (g_currentMode) {

			//WALL MODE
			case (int) g_modes.WALL:
				if (m_rightWandController.m_triggerState == (int)wandController.m_pressStates.PRESSED) {
					g_isDrawingWall = true;
					m_wallController.createWall (m_toolPos);
				}

				if (m_rightWandController.m_triggerState == (int)wandController.m_pressStates.PRESSING) {
					m_wallController.drawWall (m_toolPos);
				}


				if (m_rightWandController.m_triggerState == (int)wandController.m_pressStates.UNPRESSED) {
					g_isDrawingWall = false;
					m_wallController.placeWall ();
				}

				if (!g_isDrawingWall) {
					m_wallController.moveWallTool (m_toolPos);
				}
				break;

			//REMOVE MODE
			case (int) g_modes.REMOVE:
				if (m_rightWandController.m_triggerState == (int)wandController.m_pressStates.PRESSED) {
					m_removeController.setIsRemoving (true);
				}

				if (m_rightWandController.m_triggerState == (int)wandController.m_pressStates.UNPRESSED) {
					m_removeController.setIsRemoving (false);
				}

				m_removeController.moveRemoveTool (m_toolPos);
				break;

			//GRAB MODE
			case (int) g_modes.GRAB:
				if (m_rightWandController.m_triggerState == (int)wandController.m_pressStates.PRESSED) {
					m_grabController.setIsGrabbing (true);
				}
				if (m_rightWandController.m_triggerState == (int)wandController.m_pressStates.UNPRESSED) {
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
				
			float touchPadAxisY = m_rightWandController.m_padAxis.y;

			if (m_rightWandController.m_padPressState == (int)wandController.m_pressStates.PRESSING) {
				if (touchPadAxisY > 0.0f) {
					m_zoomController.zoom (1);
				}
				if (touchPadAxisY < 0.0f) {
					m_zoomController.zoom (-1);
				}
			} else if (m_rightWandController.m_padTouchState == (int)wandController.m_touchStates.TOUCHING && touchPadAxisY != 0.0f) {
				g_currentCubeDistance = ((touchPadAxisY + 1.0f) * g_currentCubeDistanceMax - g_currentCubeDistanceMin) / 2.0f + g_currentCubeDistanceMin;
				//			Debug.Log ("distance : " + g_currentCubeDistance + " distanceMax : " + g_currentCubeDistanceMax + " distanceMin : " + g_currentCubeDistanceMin);
			}

			m_leftWandController.setContinuousMode ();
			m_rightWandController.setContinuousMode ();
			
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
			m_removeController.setActive (true);
		}
		else{
			m_removeController.setActive (false);
		}
	}

	public void setGrabMode(bool p_isEnter){
		if(p_isEnter){
			g_currentMode = (int)g_modes.GRAB;
			setWallMode (false);
			setRemoveMode (false);
			m_grabController.setActive (true);
		}
		else{
			m_grabController.setActive (false);
		}
	}

	public void toggleEmptyWallMode(){
		m_wallController.g_isEmpty = !m_wallController.g_isEmpty;
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

	public void togglePlayMode(){
		g_isPlayMode = !g_isPlayMode;
		if(g_isPlayMode){
			setPlayMode (true);
		}
		else{
			setPlayMode (false);
		}
	}
		
}
