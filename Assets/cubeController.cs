using UnityEngine;
using System.Collections;



public class cubeController : MonoBehaviour {
	int m_i;
	float m_cubeLenght;
	float m_cubeHeight;

	public int g_wallSize;
	public float g_dropHigh;
	public float g_mass;
	public float g_gap;
	public float g_offset;
	public float g_sideOffset;
	public GameObject g_unitCube;

	// Use this for initialization
	void Start () {
		//itérateur de blocs du mur
		m_i = 0;
		//taille du mur
		g_wallSize = 20;
		//hauteur à partir de laquelle sont lachés les blocs du mur
		g_dropHigh = 0.3f;
		//masse des blocs
		g_mass = 1000.0f;
		//écart minimal entre deux blocs du mur
		g_gap = 0.2f;
		//décalage entre deux couches successives du mur
		g_offset = 0.7f;
		//décalage de côté pour les blocs
		g_sideOffset = 0.2f;

		g_unitCube = GameObject.Find ("Unit Cube");
		m_cubeLenght = g_unitCube.transform.localScale.x;
		m_cubeHeight = g_unitCube.transform.localScale.y;
	
	}
	
	// Update is called once per frame
	void Update () {

		//pour créer le mur
		if (Input.GetKey ("left ctrl")) {
			float randomizedGap = Random.Range (-g_gap, g_gap);
			float randomizedSideOffset = Random.Range (-g_sideOffset, g_sideOffset);
			createCube (new Vector3 ((m_i % g_wallSize) * (m_cubeLenght + 2 * g_gap) + randomizedGap + g_offset * (m_i / g_wallSize % 2), ((m_i / g_wallSize) + m_cubeHeight) + g_dropHigh, randomizedSideOffset));
			m_i++;
		}

		if(Input.GetMouseButtonDown(0)){
			var mousePos = Input.mousePosition;
			mousePos.z = 5.0f;
			mousePos = Camera.main.ScreenToWorldPoint (mousePos);
			mousePos.y = g_dropHigh + m_cubeHeight;
			float yAngle = transform.eulerAngles.y;
			Quaternion cubeQuat = Quaternion.Euler (new Vector3 (0.0f, yAngle, 0.0f));
		
			createCube (mousePos, cubeQuat);
		}

	
	}

	void createCube(Vector3 p_pos){
		GameObject cube = Instantiate (g_unitCube);
		Rigidbody rigidBody = cube.GetComponent<Rigidbody> ();
		cube.transform.position = p_pos;
		Debug.Log (p_pos);
		rigidBody.mass = g_mass;
	}

	void createCube(Vector3 p_pos, Quaternion p_quat){
		GameObject cube = Instantiate (g_unitCube);
		Rigidbody rigidBody = cube.GetComponent<Rigidbody> ();
		cube.transform.position = p_pos;
		cube.transform.rotation = p_quat;
		Debug.Log (p_pos);
		rigidBody.mass = g_mass;
	}

		
}
