using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class grabController : MonoBehaviour {

	bool m_isGrabbing;
	public GameObject g_grabbedCube;
	Vector3 m_grabbedCubeStartPos;

	public List<Material> g_materialList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void moveGrabTool(Vector3 p_pos){
		transform.position = p_pos;
	}

	public void setActive(bool p_isActive){
		gameObject.SetActive (p_isActive);
		transform.position = new Vector3 (0.0f, -1.0f, 0.0f);
	}

	public void setIsGrabbing(bool p_isGrabbing){
		m_isGrabbing = p_isGrabbing;

		if(m_isGrabbing){
			GetComponent<Renderer> ().material = g_materialList [1];
		}
		else{
			GetComponent<Renderer> ().material = g_materialList [0];
		}
	}

	public void dropCube(){
		
		g_grabbedCube.GetComponent<cubeBehaviour> ().g_isGrabbed = false;

		if(g_grabbedCube.GetComponent<cubeBehaviour>().g_isColliding){
			g_grabbedCube.transform.position = m_grabbedCubeStartPos;
		}

		g_grabbedCube = null;
		setActive (true);
	}

	public void moveGrabbedCube(Vector3 p_pos){
		g_grabbedCube.transform.position = p_pos;
	}

	void OnCollisionStay(Collision col){
		GameObject collidedObj = col.gameObject;

		if(m_isGrabbing && collidedObj.name == "Placed Cube"){
			if(!g_grabbedCube){
				g_grabbedCube = collidedObj;
				g_grabbedCube.GetComponent<cubeBehaviour> ().g_isGrabbed = true;
				m_grabbedCubeStartPos = g_grabbedCube.transform.position;
				setActive (false);
			}

		}
	}
		
}
