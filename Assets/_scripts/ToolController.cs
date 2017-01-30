using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour {

	[SerializeField]
	Material normalMaterial;
	[SerializeField]
	Material eraseMaterial;
	[SerializeField]
	Material grabMaterial;
	[SerializeField]
	Material invalidMaterial;

	[SerializeField]
	GameObject rightController;
	SteamVR_LaserPointer m_laserPointer;

	bool m_isGrabbing;
	bool m_isRemoving;
	public GameObject g_grabbedCube;
	Vector3 m_grabbedCubeStartPos;
	GameObject target;

	// Use this for initialization
	void Start () {
		m_laserPointer = rightController.GetComponent<SteamVR_LaserPointer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setActive(bool p_isActive){
		if (p_isActive) {
			m_laserPointer.PointerIn += Selecting;
			m_laserPointer.PointerOut += PointerGoingOut;
		} else {
			m_laserPointer.PointerIn -= Selecting;
			m_laserPointer.PointerOut -= PointerGoingOut;
		}
	}

	public void setIsGrabbing(bool p_isGrabbing){
		m_isGrabbing = p_isGrabbing;
		if (m_isGrabbing && target.name == "Placed Cube") {
			if (!g_grabbedCube) {
				g_grabbedCube = target;
				g_grabbedCube.GetComponent<cubeBehaviour> ().g_isGrabbed = true;
				m_grabbedCubeStartPos = g_grabbedCube.transform.position;
			} else if (g_grabbedCube.GetComponent<cubeBehaviour> ().g_isColliding) {
				g_grabbedCube.GetComponent<MeshRenderer> ().material = invalidMaterial;
			} else {
				g_grabbedCube.GetComponent<MeshRenderer> ().material = grabMaterial;
			}
		}
	}

	public void setIsRemoving(bool p_isRemoving){
		m_isRemoving = p_isRemoving;
		if (!target)
			return;
		if (m_isRemoving && target.name == "Placed Cube") {
			Destroy (target);
			g_grabbedCube = null;
		}
	}

	public void moveGrabbedCube(Vector3 p_pos){
		g_grabbedCube.transform.position = p_pos;
	}

	public void dropCube(){
		g_grabbedCube.GetComponent<cubeBehaviour> ().g_isGrabbed = false;

		if(g_grabbedCube.GetComponent<cubeBehaviour>().g_isColliding){
			g_grabbedCube.transform.position = m_grabbedCubeStartPos;
		}
		g_grabbedCube = null;
		target = null;
	}

	public void Selecting(object sender, PointerEventArgs e) {
		if (e.target.gameObject.name == "Placed Cube") {
			target = e.target.gameObject;
			if(!g_grabbedCube)
				normalMaterial = target.GetComponent<MeshRenderer> ().material;
			if (!m_isGrabbing && builderController.g_currentMode == (int)builderController.g_modes.GRAB) {
				e.target.gameObject.GetComponent<MeshRenderer> ().material = grabMaterial;
			} 
			if (!m_isRemoving && builderController.g_currentMode == (int)builderController.g_modes.REMOVE) {
				e.target.gameObject.GetComponent<MeshRenderer> ().material = eraseMaterial;
			}
		}//else if(e.target.gameObject.tag == "ButtonMenu" && rightController.GetComponent<wandController>().m_padPressState == (int)wandController.m_pressStates.PRESSED) {
		//	Invoke("LaunchMenu", 0);
		//}
	}

	public void PointerGoingOut(object sender, PointerEventArgs e) {
		if(e.target.gameObject.name == "Placed Cube" && !g_grabbedCube)
			e.target.gameObject.GetComponent<MeshRenderer> ().material = normalMaterial;
	}

}
