using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SavingSystem : MonoBehaviour {

	GameObject[] cubes;
	GameObject flagScene;

	[SerializeField]
	wallController m_wallController;

	private Scene scene;
	int i;

	// Use this for initialization
	void Start () {
		flagScene = GameObject.Find ("FlagScene");
		if(flagScene)
			LoadData ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SaveData() {
		if (!Directory.Exists (Application.persistentDataPath + "/Saves"))
			Directory.CreateDirectory (Application.persistentDataPath + "/Saves");
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/Saves/" + flagScene.GetComponent<DataLoad>().playerName + ".dat");
		CubeListData data = new CubeListData ();

		cubes = GameObject.FindGameObjectsWithTag ("Placed Cube");
		data.cubes = new CubeData[cubes.Length];
		i = 0;
		foreach(GameObject cube in cubes){
			CubeData cubeData = new CubeData ();
			cubeData.posx = cube.transform.position.x;
			cubeData.posy = cube.transform.position.y;
			cubeData.posz = cube.transform.position.z;
			cubeData.r = cube.GetComponent<MeshRenderer> ().material.color.r;
			cubeData.g = cube.GetComponent<MeshRenderer> ().material.color.g;
			cubeData.b = cube.GetComponent<MeshRenderer> ().material.color.b;
			cubeData.a = cube.GetComponent<MeshRenderer> ().material.color.a;
			Debug.Log ("CubeData : " + cubeData.posx);
			data.cubes[i] = cubeData;
			i++;
		}

		scene = SceneManager.GetActiveScene ();
		data.sceneName = scene.name;
		data.playerName = flagScene.GetComponent<DataLoad> ().playerName;

		Debug.Log ("Data : " + data);

		bf.Serialize (file, data);
		file.Close ();
	}

	public void LoadData() {
		if (File.Exists (Application.persistentDataPath + "/Saves/" + flagScene.GetComponent<DataLoad>().playerName + ".dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/Saves" + "/Data.dat", FileMode.Open);
			CubeListData data = (CubeListData)bf.Deserialize (file);
			file.Close ();
			//SceneManager.LoadScene (data.sceneName, LoadSceneMode.Single);
			cubes = GameObject.FindGameObjectsWithTag ("Placed Cube");

			foreach (GameObject cube in cubes) {
				Destroy (cube);
			}

			foreach (CubeData cube in data.cubes) {
				m_wallController.createCube (new Vector3(cube.posx, cube.posy, cube.posz), new Vector4(cube.r, cube.g, cube.b, cube.a));
			}
		}
	}
}

[System.Serializable]
public class CubeListData
{
	public string sceneName;
	public string playerName;
	public CubeData[] cubes;
}

[System.Serializable]
public class CubeData
{
	public float posx;
	public float posy;
	public float posz;

	public float r;
	public float g;
	public float b;
	public float a;
}

