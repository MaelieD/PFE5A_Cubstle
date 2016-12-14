using UnityEngine;
using System.Collections;

public class projectileBehaviour : MonoBehaviour {

	[SerializeField]
	public Rigidbody rigidbody;
	[SerializeField]
	public AudioSource audioSource;

	bool isPlayed;

	// Use this for initialization
	void Start () {

		isPlayed = false;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "Placed Cube" && rigidbody.velocity.magnitude > 3.0f && isPlayed == false) {
			audioSource.Play ();
			isPlayed = true;
		}
	}
}
