using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SteamVR_LaserPointer;

public class colorController : MonoBehaviour {

	[SerializeField]
	GameObject rightController;

	[SerializeField]
	Material mat;
	SteamVR_LaserPointer m_laserPointer;
	Color color = new Color();
	private bool m_isPainting;
	private float m_angle;
	[SerializeField]
	GameObject colorPalet;
	GameObject target;

	// Use this for initialization
	void Start () {
		m_laserPointer = rightController.GetComponent<SteamVR_LaserPointer> ();
		m_isPainting = false;
	}

	// Update is called once per frame
	void Update () {

	}

	public void setActive(bool p_isActive){
		if (p_isActive) {
			colorPalet.SetActive (true);
			m_laserPointer.PointerIn += Painting;
		} else {
			Material newMaterial = mat;
			m_laserPointer.pointer.GetComponent<MeshRenderer> ().material = newMaterial;
			colorPalet.SetActive (false);
			m_laserPointer.PointerIn -= Painting;
		}
	}

	public void setColor(float x, float y){
		// calcul de la couleur en fonction de son emplacement
		Mathf.Clamp (x, 0.01f, 1.0f);
		m_angle = Mathf.Rad2Deg * (Mathf.Atan2 (y, x));
		if(m_angle < 0)
			m_angle = 180 + (180 + m_angle);

		Material newMaterial = new Material (Shader.Find ("Unlit/Color"));

		if (m_angle <= 180 / 4.0f) {
			color = Color.black;
		} else if (m_angle <= 180 / 2.0f && m_angle > 180 / 4.0f) {
			color = Color.blue;
		} else if (m_angle <= (3.0f * 180) / 4.0f && m_angle > 180 / 2.0f) {
			color = Color.cyan;
		} else if (m_angle <= 180 && m_angle > (3.0f * 180) / 4.0f) {
			color = Color.green;
		} else if (m_angle <= (5.0f * 180) / 4.0f && m_angle > 180) {
			color = Color.magenta;
		} else if (m_angle <= (3.0f * 180) / 2.0f && m_angle > (5.0f * 180) / 4.0f) {
			color = Color.yellow;
		} else if (m_angle <= (7.0f * 180) / 4.0f && m_angle > (3.0f * 180) / 2.0f) {
			color = Color.white;
		} else if (m_angle <= 2.0f * 180 && m_angle > (7.0f * 180) / 4.0f) {
			color = Color.grey;
		}

		newMaterial.SetColor ("_Color", color);
		m_laserPointer.pointer.GetComponent<MeshRenderer> ().material = newMaterial;
	}

	public void setIsPainting(bool p_isPainting) {
		m_isPainting = p_isPainting;
		Debug.Log ("painting : " + m_isPainting);
		if (!target)
			return;
		if (m_isPainting && target.name == "Placed Cube") {
			target.GetComponent<MeshRenderer> ().material.SetColor("_Color", color);// = newMaterial;
		}
	}

	public void Painting(object sender, PointerEventArgs e) {
		target = e.target.gameObject;
	}
}
