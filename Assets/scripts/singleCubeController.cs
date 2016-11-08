using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class singleCubeController: MonoBehaviour {

	GameObject m_singleCubeTool;
	bool m_isColliding;

	public GameObject g_currentCube;
	public GameObject g_unitCube;
	public List<GameObject> g_cubeList;
	public List<Material> g_materialList;

	// Use this for initialization
	 void Start () {
	
		//cube unitaire sur lequel on clone les blocs
		g_unitCube = GameObject.Find ("Unit Cube");

		//liste de tous les cubes posés
		g_cubeList = new List<GameObject> ();

		m_singleCubeTool = gameObject;
	}

	void Update(){
		
	}

	//fonction de création des cubes, à l'endroit du clic souris
	//on annule la gravité pour pouvoir déplacer le bloc à sa guise ensuite
	public void createCube(Vector3 p_pos){

		g_currentCube = Instantiate (g_unitCube);

		g_currentCube.name = "Current Cube";
		g_currentCube.transform.position = p_pos;
	}
		

	//fonction de placement des cubes
	//on déclare le cube placé, et on lui redonne sa gravité
	public void placeCube(){
		if(m_isColliding){
			Destroy (g_currentCube);
		}
		else{
			g_currentCube.name = "Placed Cube";
			g_cubeList.Add (g_currentCube);
			g_currentCube.AddComponent<Rigidbody> ();
			g_currentCube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		}

		setActive (true);


	}

	public void moveSingleCubeTool(Vector3 p_pos){
		m_singleCubeTool.transform.position = p_pos;
	}

	public void setActive(bool p_isActive){
		m_singleCubeTool.SetActive (p_isActive);
		m_singleCubeTool.transform.position = new Vector3 (0.0f, -1.0f, 0.0f);
	}

	void OnCollisionStay(Collision col){
			m_singleCubeTool.GetComponent<Renderer> ().material = g_materialList [1];
			m_isColliding = true;

	}

	void OnCollisionExit(Collision col){
			m_singleCubeTool.GetComponent<Renderer> ().material = g_materialList [0];
			m_isColliding = false;
	}
}
