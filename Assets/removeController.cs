using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class removeController : MonoBehaviour {

	GameObject m_removeTool;
	bool m_isRemoving;

	public List<Material> g_materialList;

	// Use this for initialization
	void Start () {

		m_removeTool = gameObject;
		m_isRemoving = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void moveRemoveTool(Vector3 p_pos){
		m_removeTool.transform.position = p_pos;
	}

	public void setIsRemoving(bool p_isRemoving){
		m_isRemoving = p_isRemoving;

		if(m_isRemoving){
			m_removeTool.GetComponent<Renderer> ().material = g_materialList [1];
		}
		else{
			m_removeTool.GetComponent<Renderer> ().material = g_materialList [0];
		}
	}

	void OnCollisionStay(Collision col){
		GameObject collidedObj = col.gameObject;

		if(collidedObj.name == "Placed Cube"){
			
			if(m_isRemoving){
				Destroy (collidedObj);
			}
			else{
				collidedObj.GetComponent<Renderer> ().material = g_materialList [3];
			}
		}
	}

	void OnCollisionExit(Collision col){

		GameObject collidedObj = col.gameObject;

		if(collidedObj.name == "Placed Cube"){
			collidedObj.GetComponent<Renderer> ().material = g_materialList [2];
		}
	}

	public void setActive(bool p_isActive){
		m_removeTool.SetActive (p_isActive);

	}
}
