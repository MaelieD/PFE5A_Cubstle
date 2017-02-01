using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.Collections;

namespace Wacki {

    public class DemoMenu : MonoBehaviour {

		[SerializeField]
		GameObject flagScene;
		[SerializeField]
		InputField player;
		[SerializeField]
		GameObject scrollView;
		[SerializeField]
		GameObject itemButton;

        //public Transform dirLight;
        public Transform cubeSpawn;
		public Transform play;
		public Transform load;
		public Transform quit;

		string[] files;


        /*public void RotateLight(float amount)
        {
            dirLight.rotation = Quaternion.AngleAxis(amount, Vector3.right);
        }*/

        public void SpawnCube()
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            go.transform.position = cubeSpawn.position;
            go.transform.rotation = cubeSpawn.rotation;
            go.AddComponent<Rigidbody>();
        }

		public void Play() {
			player.gameObject.SetActive (true);
			//StartCoroutine (WaitAndLoad ());
		}

		public void Edit() {
			flagScene.GetComponent<DataLoad> ().playerName = player.textComponent.text;
			DontDestroyOnLoad(flagScene);
			SceneManager.LoadScene ("main", LoadSceneMode.Single);
			Scene sceneToLoad = SceneManager.GetSceneByName ("main");
			SceneManager.MoveGameObjectToScene (flagScene, sceneToLoad);
			Debug.Log (player.onEndEdit);
		}

		public void Load() { // DOESNT WORK !!!
			
			//Scene sceneMain = SceneManager.GetSceneByName ("main");
			//Debug.Log ("SCENE : " + sceneMain.name);
			//SceneManager.MoveGameObjectToScene (flagScene, sceneMain);
			//SceneManager.LoadScene ("main");
			// load every scene.unity in _scenes folder --> need a scrollbar
			//LoadData();
			int i = 0;
			Debug.Log (Application.persistentDataPath + "/Saves");
			if (Directory.Exists (Application.persistentDataPath + "/Saves")) {
				files = Directory.GetFiles (Application.persistentDataPath + "/Saves");
				scrollView.SetActive(true);
				foreach(string f in files){
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file = File.Open (f, FileMode.Open);
					CubeListData data = (CubeListData)bf.Deserialize (file);
					file.Close ();
					Instantiate (itemButton);
					itemButton.GetComponentInChildren<Text> ().text = data.playerName;
					Debug.Log (i++);
					// afficher list button for each save
					//scrollView.
					// when click on one, load scene
						// que si save choisie
						


					//Debug.Log(data.playerName);
					// list file name text on button to click to load the good scene
				}
			}
		}

		public void LoadSelectedScene(string playerName) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/Saves"+ playerName +".dat", FileMode.Open);
			CubeListData data = (CubeListData)bf.Deserialize (file);
			file.Close ();

			int index = SceneUtility.GetBuildIndexByScenePath("Assets/_scenes/" + data.sceneName + ".unity");
			flagScene.GetComponent<DataLoad> ().playerName = data.playerName;
			DontDestroyOnLoad(flagScene);
			SceneManager.LoadScene (index, LoadSceneMode.Single); // need to load the scene before finding it
			Scene sceneToLoad = SceneManager.GetSceneByBuildIndex (index);
			Debug.Log (flagScene.name);
			SceneManager.MoveGameObjectToScene (flagScene, sceneToLoad);
		}

		public void Quit() {
			Application.Quit();
		}
    }

}