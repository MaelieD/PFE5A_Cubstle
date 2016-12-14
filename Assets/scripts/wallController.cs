using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class wallController : MonoBehaviour {

	[SerializeField]
	public Renderer rend;

	Vector3 m_wallStart;
	Vector3 m_wallEnd;
	bool m_isColliding;
	bool isValid;
	int maxCubeNumber;
	int maxCubeInABlocNumber;
	int nbCubesX;
	int nbCubesY;
	int nbCubesZ;

	public bool g_isEmpty;
	public GameObject g_unitCube;
	public List<Material> g_materialList;
	public Text cubeNumberText;

	public int disabledCubesNumber;

	// Use this for initialization
	void Start () {
		isValid = true;
		maxCubeNumber = 1500;
		maxCubeInABlocNumber = 500;
		disabledCubesNumber = 0;
		m_isColliding = false;
		g_isEmpty = true;

	}

	void createCube(Vector3 p_pos){

		GameObject currentCube = Instantiate (g_unitCube);

		currentCube.name = "Placed Cube";
		currentCube.transform.position = p_pos;

		if(gameController.g_currentMode == (int)gameController.modes.PLAY){
//			Debug.Log ("play mode");
			Rigidbody currentRigidbody = currentCube.GetComponent<Rigidbody> ();
			currentRigidbody.constraints = RigidbodyConstraints.None;
			currentRigidbody.useGravity = true;
		}

		builderController.g_cubeList.Add (currentCube);
		updateCubeNumberText ();
	}

	//fonction pour démarrer un mur à partir d'une position initiale
	public void createWall(Vector3 p_pos){
		m_wallStart = p_pos;
		transform.position = m_wallStart;
//		Debug.Log ("create wall at position " + p_pos);
	}

	//fonction pour dessiner un mur entre la position initiale et la position finale
	//on ne dessine des murs que selon l'axe x ou z, donc on cale les positions selon x ou z
	public void drawWall(Vector3 p_pos){


		m_wallEnd.x = Mathf.Round (p_pos.x - m_wallStart.x) + m_wallStart.x;
		m_wallEnd.y = Mathf.Round (p_pos.y - m_wallStart.y) + m_wallStart.y;
		m_wallEnd.z = Mathf.Round (p_pos.z - m_wallStart.z) + m_wallStart.z;

		//on définit la taille du mur en fonction de m_wallEnd et m_wallStart
		transform.localScale = m_wallEnd - m_wallStart;
		//on passe les valeurs en absolu pour garder des dimensions positives
		//on rajoute les deux moitiés de bloc non prises en compte aux extrémités à cause du 
		//fait que m_wallStart et m_wallEnd définissent les centres des positions
		transform.localScale = new Vector3 (
			Mathf.Abs (transform.localScale.x) + 1.0f,
			Mathf.Abs (transform.localScale.y) + 1.0f,
			Mathf.Abs (transform.localScale.z) + 1.0f);

		//on place le mur en fonction des dimensions du bloc
		transform.position = new Vector3 (
			m_wallStart.x + 0.5f * (transform.localScale.x - 1) * Mathf.Sign(m_wallEnd.x - m_wallStart.x),
			m_wallStart.y + 0.5f * (transform.localScale.y - 1) * Mathf.Sign(m_wallEnd.y - m_wallStart.y),
			m_wallStart.z + 0.5f * (transform.localScale.z - 1) * Mathf.Sign(m_wallEnd.z - m_wallStart.z));


		//nombre de blocs en x
		nbCubesX = (int)transform.localScale.x;

		//nombre de blocs en y
		nbCubesY = (int)transform.localScale.y;

		//nombre de blocs en z
		nbCubesZ = (int)transform.localScale.z;

		int nbCubesToCreate = nbCubesX * nbCubesY * nbCubesZ;
		int emptySpaceX = 0;
		int emptySpaceZ = 0;

		if (g_isEmpty) {
			if (nbCubesX > 2) {
				emptySpaceX = nbCubesX - 2;
			}
			if (nbCubesZ > 2) {
				emptySpaceZ = nbCubesZ - 2;
			}

			int emptySpace = emptySpaceX * nbCubesY * emptySpaceZ;
			nbCubesToCreate -= emptySpace;
		}

		if (builderController.g_cubeList.Count - disabledCubesNumber + nbCubesToCreate > maxCubeNumber || nbCubesToCreate > maxCubeInABlocNumber) {
			InvalidateWall ();
		} else {

			ValidateWall ();
		}

	}

	//fonction pour placer le mur entre la position initiale et la position finale
	public void placeWall(){

		if(!m_isColliding && isValid){
//			Debug.Log ("place wall between " + m_wallStart + " and " + m_wallEnd);
//			Debug.Log ("current wall size : " + transform.localScale);
//			Debug.Log ("current wall position : " + transform.position);
			Vector3 currentPos = m_wallStart;

			//défintion des offset pour savoir s'il faut avancer ou reculer dans chaque composante du mur
			//en fonction de la position du départ et de l'arrivée
			int xDir = (int) Mathf.Sign (m_wallEnd.x - m_wallStart.x);
			int yDir = (int) Mathf.Sign (m_wallEnd.y - m_wallStart.y);
			int zDir = (int) Mathf.Sign (m_wallEnd.z - m_wallStart.z);

			//nombre de blocs en x
			nbCubesX = (int)transform.localScale.x;

			//nombre de blocs en y
			nbCubesY = (int)transform.localScale.y;

			//nombre de blocs en z
			nbCubesZ = (int)transform.localScale.z;


			for(int i = 0; i < nbCubesX; i++){
				for(int j = 0; j < nbCubesY; j++){
					for(int k = 0; k < nbCubesZ; k++){
						if(!g_isEmpty){
							createCube (currentPos);
						}
						else{

							if(i == 0 || i == nbCubesX - 1 || k == 0 || k == nbCubesZ - 1){
								createCube (currentPos);
							}
						}


						currentPos.z += zDir;
					}
					currentPos.z = m_wallStart.z;
					currentPos.y += yDir;
				}
				currentPos.y = m_wallStart.y;
				currentPos.x += xDir;
			}
			

		}



		resetWallTool ();

	}

	public void setActive(bool p_isActive){
		gameObject.SetActive (p_isActive);
	}

	public void moveWallTool(Vector3 p_pos){
		transform.position = p_pos;
	}

	void resetWallTool(){
		transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		isValid = true;
		rend.material = g_materialList [0];
	}

	void OnCollisionStay(Collision col){
		rend.material = g_materialList [1];
		m_isColliding = true;
		
	}

	void OnCollisionExit(Collision col){
		if (isValid) {
			rend.material = g_materialList [0];
		}

		m_isColliding = false;

	}

	void InvalidateWall(){
		isValid = false;
		rend.material = g_materialList [1];
	}

	void ValidateWall(){
		isValid = true;
		rend.material = g_materialList [0];
	}

	public void updateCubeNumberText(){
		int cubeNumber = builderController.g_cubeList.Count - disabledCubesNumber;
		cubeNumberText.text = "Nombre de cubes : " + cubeNumber;
	}
}
