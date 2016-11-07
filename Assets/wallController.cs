using UnityEngine;
using System.Collections;

public class wallController : MonoBehaviour {

	static Vector3 m_wallStart;
	static Vector3 m_wallEnd;

	static GameObject m_wallTool;

	// Use this for initialization
	void Start () {
		m_wallTool = gameObject;
	}

	void Update(){
		
	}

	//fonction pour démarrer un mur à partir d'une position initiale
	static public void createWall(Vector3 p_pos){
		m_wallStart = p_pos;
		m_wallTool.transform.position = m_wallStart;
		Debug.Log ("create wall at position " + p_pos);
	}

	//fonction pour dessiner un mur entre la position initiale et la position finale
	//on ne dessine des murs que selon l'axe x ou z, donc on cale les positions selon x ou z
	static public void drawWall(Vector3 p_pos){
		//on positionne m_wallEnd
		//si le point actuel est plus proche en z du point initial qu'en x, la valeur
		//du point actuel en x devient la valeur du point initial en x, et inversement
		//et on arrondit les valeurs pour discrétiser les futures dimensions du mur
		if (Mathf.Abs (p_pos.x - m_wallStart.x) > Mathf.Abs (p_pos.z - m_wallStart.z)) {
			m_wallEnd.z = m_wallStart.z;
			m_wallEnd.x = Mathf.Round (p_pos.x - m_wallStart.x) + m_wallStart.x;
		}
		else {
			m_wallEnd.x = m_wallStart.x;
			m_wallEnd.z = Mathf.Round (p_pos.z - m_wallStart.z) + m_wallStart.z;
		}
		m_wallEnd.y = Mathf.Round (p_pos.y - m_wallStart.y) + m_wallStart.y;

		//on définit la taille du mur en fonction de m_wallEnd et m_wallStart
		m_wallTool.transform.localScale = m_wallEnd - m_wallStart;
		//on passe les valeurs en absolu pour garder des dimensions positives
		//on rajoute les deux moitiés de bloc non prises en compte aux extrémités à cause du 
		//fait que m_wallStart et m_wallEnd définissent les centres des positions
		m_wallTool.transform.localScale = new Vector3 (
			Mathf.Abs (m_wallTool.transform.localScale.x) + 1.0f,
			Mathf.Abs (m_wallTool.transform.localScale.y) + 1.0f,
			Mathf.Abs (m_wallTool.transform.localScale.z) + 1.0f);

		//on place le mur en fonction des dimensions du bloc
		m_wallTool.transform.position = new Vector3 (
			m_wallStart.x + 0.5f * (m_wallTool.transform.localScale.x - 1) * Mathf.Sign(m_wallEnd.x - m_wallStart.x),
			m_wallStart.y + 0.5f * (m_wallTool.transform.localScale.y - 1) * Mathf.Sign(m_wallEnd.y - m_wallStart.y),
			m_wallStart.z + 0.5f * (m_wallTool.transform.localScale.z - 1) * Mathf.Sign(m_wallEnd.z - m_wallStart.z));
		

	}

	//fonction pour placer le mur entre la position initiale et la position finale
	static public void placeWall(){

		Debug.Log ("place wall between " + m_wallStart + " and " + m_wallEnd);
		Debug.Log ("current wall size : " + m_wallTool.transform.localScale);
		Debug.Log ("current wall position : " + m_wallTool.transform.position);
		Vector3 currentPos = m_wallStart;

		//défintion des offset pour savoir s'il faut avancer ou reculer dans chaque composante du mur
		//en fonction de la position du départ et de l'arrivée
		int xDir = (int) Mathf.Sign (m_wallEnd.x - m_wallStart.x);
		int yDir = (int) Mathf.Sign (m_wallEnd.y - m_wallStart.y);
		int zDir = (int) Mathf.Sign (m_wallEnd.z - m_wallStart.z);

		//nombre de blocs en x
		int nbCubesX = (int)m_wallTool.transform.localScale.x;

		//nombre de blocs en y
		int nbCubesY = (int)m_wallTool.transform.localScale.y;

		//nombre de blocs en z
		int nbCubesZ = (int)m_wallTool.transform.localScale.z;

		for(int i = 0; i < nbCubesX; i++){
			for(int j = 0; j < nbCubesY; j++){
				for(int k = 0; k < nbCubesZ; k++){
					singleCubeController.createCube (currentPos);
					singleCubeController.placeCube ();
					currentPos.z += zDir;
				}
				currentPos.z = m_wallStart.z;
				currentPos.y += yDir;
			}
			currentPos.y = m_wallStart.y;
			currentPos.x += xDir;
		}

		resetCurrentWall ();

	}

	static public void setActive(bool p_isActive){
		m_wallTool.SetActive (p_isActive);
	}

	static public void moveWallStart(Vector3 p_pos){
		m_wallTool.GetComponent<Rigidbody> ().MovePosition (p_pos);
	}

	static void resetCurrentWall(){
		m_wallTool.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
	}
}
