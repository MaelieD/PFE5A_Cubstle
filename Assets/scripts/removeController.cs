using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class removeController : MonoBehaviour {

	bool m_isRemoving;

	public List<Material> g_materialList;

	// Use this for initialization
	void Start () {

		m_isRemoving = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void moveRemoveTool(Vector3 p_pos){
		transform.position = p_pos;
	}

	public void setIsRemoving(bool p_isRemoving){
		m_isRemoving = p_isRemoving;

		if(m_isRemoving){
			GetComponent<Renderer> ().material = g_materialList [1];
		}
		else{
			GetComponent<Renderer> ().material = g_materialList [0];
		}
	}

	void OnCollisionStay(Collision col){
		GameObject collidedObj = col.gameObject;

		if(collidedObj.name == "Placed Cube"){
			if(m_isRemoving){
				Destroy (collidedObj);
			}
		}
	}
		

	public void setActive(bool p_isActive){
		gameObject.SetActive (p_isActive);

	}
}
