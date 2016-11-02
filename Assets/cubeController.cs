using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class cubeController : MonoBehaviour {
	float m_cubeHeight;
	List<GameObject> m_cubeList;

	public float g_mass;


	public GameObject g_unitCube;
	public GameObject g_currentCube;
	public Rigidbody g_currentRigidbody;

	// Use this for initialization
	void Start () {

		//masse des blocs
		g_mass = 1000.0f;

		//liste de tous les cubes posés
		m_cubeList = new List<GameObject> ();

		g_unitCube = GameObject.Find ("Unit Cube");
		m_cubeHeight = g_unitCube.transform.localScale.y;

	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)){
			Vector3 mousePos = getMousePos ();

			createCube (mousePos);
		}

		//pour placer un bloc
		if(Input.GetMouseButton(0)){
			Vector3 mousePos = getMousePos ();

			moveCube (mousePos);

		}
		if(Input.GetMouseButtonUp(0)){
			placeCube ();
		}

		if(Input.GetKeyDown("p")){
			enterPlayMode ();
		}
	
	}

	void createCube(Vector3 p_pos){

		g_currentCube = Instantiate (g_unitCube);

		g_currentCube.name = "Current Cube";
		g_currentRigidbody = g_currentCube.GetComponent<Rigidbody> ();
		g_currentCube.transform.position = p_pos;
		Debug.Log (p_pos);
		g_currentRigidbody.mass = g_mass;
		g_currentRigidbody.isKinematic = false;
		g_currentRigidbody.MovePosition (p_pos);
		g_currentRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		g_currentRigidbody.useGravity = false;
	}

	Vector3 getMousePos(){
		var mousePos = Input.mousePosition;
		mousePos.z = 3.0f;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		if(mousePos.y < m_cubeHeight / 2){
			mousePos.y = m_cubeHeight / 2;
		}
		return mousePos;
	}
		

	void moveCube(Vector3 p_pos){
		Vector3 cubeToMouseVector = p_pos - g_currentCube.transform.position;

		g_currentRigidbody.AddForce (cubeToMouseVector, ForceMode.VelocityChange);
		g_currentRigidbody.AddForce (- g_currentRigidbody.velocity, ForceMode.VelocityChange);
		
	}

	void placeCube(){
		g_currentCube.name = "Placed Cube";
		cubeBehaviour cBehaviour = g_currentCube.GetComponent<cubeBehaviour> ();
		cBehaviour.g_isPlaced = true;
		g_currentRigidbody.useGravity = true;
		m_cubeList.Add (g_currentCube);

	}

	void enterPlayMode(){
		Debug.Log ("entered Play Mode");

		foreach(GameObject gameObject in m_cubeList){
			gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		}
	}
		
		
}
