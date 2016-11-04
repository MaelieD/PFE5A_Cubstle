using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class builderController : MonoBehaviour {
	
	List<GameObject> m_cubeList;
	singleCubeController m_sCC;
	wallController m_wC;


	public float g_cubeDistance;
	public bool g_isWallMode;
	public bool g_isDrawingWall;

	static public float g_cubeHeight;

	// Use this for initialization
	void Start () {

		//liste de tous les cubes posés
		m_cubeList = new List<GameObject> ();


		//hauteur d'un bloc
		g_cubeHeight = 1.0f;

		//distance entre le joueur et le cube
		g_cubeDistance = 3.0f;

		//détermine si on est en mode construction de mur ou non
		g_isWallMode = false;

		//détermine si l'on est en train de dessiner un mur
		g_isDrawingWall = false;

		singleCubeController.init ();
		wallController.init ();
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 mousePos = getMousePos ();

		//Au clic souris, on crée un cube
		if(Input.GetMouseButtonDown(0)){

			if(g_isWallMode){
				g_isDrawingWall = true;
				wallController.createWall (mousePos);
			}
			else{
				singleCubeController.createCube (mousePos);
			}

		}

		//pour déplacer un bloc, en déplaçant la souris avec le clic enfoncé
		if(Input.GetMouseButton(0)){

			if(g_isWallMode){
				wallController.drawWall (mousePos);
			}
			else{
				singleCubeController.moveCube (mousePos);
			}

		}

		//au relâchement de la souris, on place le bloc
		if(Input.GetMouseButtonUp(0)){

			if(g_isWallMode){
				g_isDrawingWall = false;
				wallController.placeWall ();
			}
			else{
				singleCubeController.placeCube ();
				m_cubeList.Add (singleCubeController.g_currentCube);
			}

		}

		//entrée dans le mode play grâce à la touche p
		if(Input.GetKeyDown("p")){
			enterPlayMode ();
		}

		//entrée ou sortie du mode mur grâce à la touche w
		if(Input.GetKeyDown("w")){
			g_isWallMode = !g_isWallMode;

			if(g_isWallMode){
				Debug.Log ("entered wall mode");
				wallController.enterWallMode ();
			}
			else{
				Debug.Log ("exited wall mode");
				wallController.exitWallMode ();
			}
				
		}

		if(g_isWallMode && !g_isDrawingWall){
			wallController.moveWallStart (mousePos);
		}

		//Avec la molette, modification de la distance joueur/cube
		if(Input.GetAxis("Mouse ScrollWheel") > 0.0f){
			g_cubeDistance++;
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0.0f){
			g_cubeDistance--;
		}

		//on clampe la distance pour éviter de perdre le cube ou de trop le rapprocher de nous
		g_cubeDistance = Mathf.Clamp (g_cubeDistance, 2.0f, 15.0f);
	
	}



	//Renvoie ce que pointe la souris dans les coordonnées du monde
	//on définit manuellement la distance joueur/bloc
	//si la hauteur est trop basse, on a rehausse pour éviter de déplacer ou créer un bloc en intersection avec le sol
	Vector3 getMousePos(){
		var mousePos = Input.mousePosition;
		mousePos.z = g_cubeDistance;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		if(mousePos.y < g_cubeHeight / 2){
			mousePos.y = g_cubeHeight / 2;
		}
		return mousePos;
	}
		

	//fonction pour entrer dans le mode play
	//on annule toutes les contraintes sur les blocs
	void enterPlayMode(){
		Debug.Log ("entered Play Mode");

		foreach(GameObject gameObject in m_cubeList){
			gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		}
	}


		
		
}
