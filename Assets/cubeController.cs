using UnityEngine;
using System.Collections;



public class cubeController : MonoBehaviour {
	int m_i;
	float m_cubeLenght;

	public int g_wallSize;
	public float g_dropHigh;
	public float g_mass;
	public float g_gap;
	public float g_offset;
	public GameObject g_unitCube;

	// Use this for initialization
	void Start () {
		m_i = 0;
		g_wallSize = 20;
		g_dropHigh = 0.3f;
		g_mass = 1000.0f;
		g_gap = 0.2f;
		g_offset = 0.7f;

		g_unitCube = GameObject.Find ("Unit Cube");
		m_cubeLenght = g_unitCube.transform.localScale.x;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			float randomizedGap = Random.Range (-g_gap, g_gap);
			createCube (new Vector3 ((m_i % g_wallSize) * (m_cubeLenght + 2 * g_gap) + randomizedGap + g_offset * (m_i / g_wallSize % 2), ((m_i / g_wallSize) + 1) + g_dropHigh, 0.0f));
			m_i++;
		}

	
	}

	void createCube(Vector3 p_pos){
		GameObject cube = Instantiate (g_unitCube);
		Rigidbody rigidBody = cube.GetComponent<Rigidbody> ();
		cube.transform.position = p_pos;
		Debug.Log (p_pos);
		rigidBody.mass = g_mass;
	}

		
}
