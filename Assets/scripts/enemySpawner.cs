using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {

	public GameObject skeletor;
	public float minTime = 5;
	public float maxTime = 15;
	private float time;
	private float spawnTime;
	public List<GameObject> skeletorList;
	public List<GameObject> flagList;
	bool start;

	Transform m_current_transform;
	[SerializeField]
	public GameObject target;

	//public builderController g_builderController;
	// Use this for initialization
	void Start () {
		setRandomTime ();
		time = 0;
		List<GameObject> skeletorList = new List<GameObject>();
		start = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (start)
		{
			time += Time.deltaTime;
			if (time >= spawnTime) {
				sortFlag ();
				spawn ();
				time = 0;
				setRandomTime ();
			}
		}

	}

	void spawn(){
		if (skeletorList.Count <= 20) {
			GameObject newSkeletor = Instantiate (skeletor, m_current_transform.position + new Vector3(0.0f, 0.0f, - 5.0f), skeletor.transform.rotation);
			skeletorList.Add (newSkeletor);
			//newSkeletor.transform.position = target.position;
			newSkeletor.name = "Spawned Skeletor";

			Rigidbody enemyRigidbody = newSkeletor.GetComponent<Rigidbody> ();
			enemyController eC = newSkeletor.GetComponent<enemyController> ();
			eC.startEnemy ();
			eC.target = target.transform;

		}
	}

	void setRandomTime(){
		spawnTime = Random.Range(minTime, maxTime);
	}

	public void startEnemy(){
		start = true;
	}

	public void stopEnemy(){
		start = false;
	}

	void sortFlag(){
		float randValue = Random.Range (0.0f, flagList.Count - 1);
		int index = (int)Mathf.Round (randValue);
		Debug.Log ("index : " + index);
		m_current_transform = flagList [index].transform;

	}
}
