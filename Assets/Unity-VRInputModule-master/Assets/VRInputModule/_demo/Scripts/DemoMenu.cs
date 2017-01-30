using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wacki {

    public class DemoMenu : MonoBehaviour {

        //public Transform dirLight;
        public Transform cubeSpawn;
		public Transform play;
		public Transform load;
		public Transform quit;

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
		} 

		public void Load() {
			//SceneManager.LoadScene ("menu");
			// load every scene.unity in _scenes folder --> need a scrollbar
		}

		public void Quit() {
			Application.Quit();
		}
    }

}