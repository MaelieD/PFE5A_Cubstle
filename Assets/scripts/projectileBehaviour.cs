using UnityEngine;
using System.Collections;

public class projectileBehaviour : MonoBehaviour {

	bool isPlayed;

	// Use this for initialization
	void Start () {

		isPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "Placed Cube" && GetComponent<Rigidbody>().velocity.magnitude > 3.0f && isPlayed == false) {
			GetComponent<AudioSource> ().Play ();
			isPlayed = true;
		}
	}
}
