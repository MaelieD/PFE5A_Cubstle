using UnityEngine;
using System.Collections;

public class cubeBehaviour : MonoBehaviour {
	public bool g_isPlaced;
	public bool g_isColliding;
	bool m_isFreezed;
	Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
		m_rigidbody = this.GetComponent<Rigidbody> ();
		g_isColliding = false;
		m_isFreezed = false;

	}
	
	// Update is called once per frame
	void Update () {

		if(!m_isFreezed){
			if(g_isPlaced && m_rigidbody.velocity.magnitude < 0.01f){
				m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
				m_isFreezed = true;
				Debug.Log ("Freezed");
			}
		}

		g_isColliding = false;
	}

	void OnCollisionEnter(Collision col){
		GameObject collidedObj = col.collider.gameObject;
		if(collidedObj.name == "Placed Cube"){
			if(!collidedObj.GetComponent<cubeBehaviour>().m_isFreezed){
				g_isColliding = true;
			}
		}

	}
}
