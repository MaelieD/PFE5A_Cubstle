using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class singleCubeController: MonoBehaviour {

	bool m_isColliding;

	public GameObject g_currentCube;
	public GameObject g_unitCube;
	public List<GameObject> g_cubeList;
	public List<Material> g_materialList;

	// Use this for initialization
	 void Start () {

		//liste de tous les cubes posés
		g_cubeList = new List<GameObject> ();

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
			g_currentCube.GetComponent<Rigidbody> ().useGravity = false;
		}

		setActive (true);


	}

	public void moveSingleCubeTool(Vector3 p_pos){
		transform.position = p_pos;
	}

	public void setActive(bool p_isActive){
		gameObject.SetActive (p_isActive);
		transform.position = new Vector3 (0.0f, -1.0f, 0.0f);
	}

	void OnCollisionStay(Collision col){
			GetComponent<Renderer> ().material = g_materialList [1];
			m_isColliding = true;

	}

	void OnCollisionExit(Collision col){
			GetComponent<Renderer> ().material = g_materialList [0];
			m_isColliding = false;
	}
}
