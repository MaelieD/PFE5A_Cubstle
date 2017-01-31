using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Wacki {

    public class DemoMenu : MonoBehaviour {

        //public Transform dirLight;
        public Transform cubeSpawn;
		public Transform play;
		public Transform load;
		public Transform quit;

		string[] files;

		[SerializeField]
		GameObject flagScene;

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
			SceneManager.LoadScene ("main");
			//SceneManager.CreateScene ("main");
		}

		public void Load() { // DOESNT WORK !!!
			
			//Scene sceneMain = SceneManager.GetSceneByName ("main");
			//Debug.Log ("SCENE : " + sceneMain.name);
			//SceneManager.MoveGameObjectToScene (flagScene, sceneMain);
			//SceneManager.LoadScene ("main");
			// load every scene.unity in _scenes folder --> need a scrollbar
			//LoadData();
			Debug.Log (Application.persistentDataPath + "/Saves");
			if (Directory.Exists (Application.persistentDataPath + "/Saves")) {
				files = Directory.GetFiles (Application.persistentDataPath + "/Saves");
				foreach(string f in files){
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file = File.Open (f, FileMode.Open);
					CubeListData data = (CubeListData)bf.Deserialize (file);
					file.Close ();
					int index = SceneUtility.GetBuildIndexByScenePath("Assets/_scenes/" + data.sceneName + ".unity");
					SceneManager.LoadScene (index, LoadSceneMode.Single); // need to load the scene before finding it
					Scene sceneToLoad = SceneManager.GetSceneByBuildIndex (index);
					Debug.Log (flagScene.name);
					SceneManager.MoveGameObjectToScene (flagScene, sceneToLoad);


					//Debug.Log(data.playerName);
					// list file name text on button to click to load the good scene
				}
			}
		}

		public void Quit() {
			Application.Quit();
		}
    }

}