using UnityEngine;
using System.Collections;

public class removeController : MonoBehaviour {

	static GameObject m_removeTool;
	static Rigidbody m_removeToolRigidbody;
	static bool m_isRemoving;

	// Use this for initialization
	void Start () {

		m_removeTool = gameObject;
		m_removeToolRigidbody = m_removeTool.GetComponent<Rigidbody> ();
		m_isRemoving = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	static public void moveRemoveTool(Vector3 p_pos){
		m_removeToolRigidbody.MovePosition (p_pos);
	}

	static public void setIsRemoving(bool p_isRemoving){
		m_isRemoving = p_isRemoving;
	}

	void OnCollisionStay(Collision col){
		GameObject collidedObj = col.gameObject;

		if(collidedObj.name == "Placed Cube" && m_isRemoving){
			Destroy (collidedObj);
		}
	}

	static public void setActive(bool p_isActive){
		m_removeTool.SetActive (p_isActive);
	}
}
