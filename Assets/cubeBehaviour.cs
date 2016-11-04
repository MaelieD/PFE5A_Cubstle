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

		//On vérifie si le bloc est gelé, et si ce n'est pas le cas
		//on teste si le bloc est placé, et si sa vitesse est nulle
		//Le cas échéant, on gèle le bloc en le contraignant en position et en rotation
		if(!m_isFreezed){
			if(g_isPlaced && m_rigidbody.velocity.magnitude < 0.01f){
				m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
				m_isFreezed = true;
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
