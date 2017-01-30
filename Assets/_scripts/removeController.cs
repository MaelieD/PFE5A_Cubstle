using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class removeController : MonoBehaviour {

	[SerializeField]
	public Renderer rend;

	bool m_isRemoving;
	public GameObject wallTool;
	public List<Material> g_materialList;

	wallController wC;


	// Use this for initialization
	void Start () {

		m_isRemoving = false;
		wC = wallTool.GetComponent<wallController> ();
	
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
			rend.material = g_materialList [1];
		}
		else{
			rend.material = g_materialList [0];
		}
	}

	void OnCollisionStay(Collision col){
		GameObject collidedObj = col.gameObject;

		if(collidedObj.name == "Placed Cube"){
			if(m_isRemoving){
				collidedObj.SetActive (false);
				wC.disabledCubesNumber++;
				wC.updateCubeNumberText ();

			}
		}
	}
		

	public void setActive(bool p_isActive){
		gameObject.SetActive (p_isActive);

	}
}
