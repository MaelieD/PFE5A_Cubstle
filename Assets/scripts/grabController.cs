using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class grabController : MonoBehaviour {

	bool m_isGrabbing;
	GameObject m_grabbedCube;
	Vector3 m_grabbedCubeStartPos;

	public List<Material> g_materialList;
	public bool g_hasCube;

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
		
		m_grabbedCube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		m_grabbedCube.GetComponent<cubeBehaviour> ().g_isGrabbed = false;

		if(m_grabbedCube.GetComponent<cubeBehaviour>().g_isColliding){
			m_grabbedCube.transform.position = m_grabbedCubeStartPos;
		}

		m_grabbedCube = null;
		g_hasCube = false;
		setActive (true);
	}

	public void moveGrabbedCube(Vector3 p_pos){
		m_grabbedCube.transform.position = p_pos;
	}

	void OnCollisionStay(Collision col){
		GameObject collidedObj = col.gameObject;
		if(m_isGrabbing && collidedObj.name == "Placed Cube"){
			m_grabbedCube = collidedObj;
			m_grabbedCube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
			m_grabbedCube.GetComponent<cubeBehaviour> ().g_isGrabbed = true;
			g_hasCube = true;
			m_grabbedCubeStartPos = m_grabbedCube.transform.position;
			setActive (false);
		}
	}
		
}
