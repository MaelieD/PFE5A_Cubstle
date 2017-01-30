using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class catapultController : MonoBehaviour {

	public GameObject projectile;
	public Text projectileText; 
	public Text collisionText;

	List<GameObject> projectileList = new List<GameObject>();
	float lastProjectileLaunchTime;
	bool isLaunched;

	int launchedProjectiles;

	static public int contacts = 0;

	// Use this for initialization
	void Start () {
		isLaunched = false;
		launchedProjectiles = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (isLaunched && Time.time - lastProjectileLaunchTime > 3.0f) {
			Vector3 projectilePos = new Vector3 (Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
			Vector3 projectileDir = Vector3.Normalize(projectilePos);
			projectilePos = projectileDir * 50.0f;

			GameObject currentProjectile = GameObject.Instantiate (projectile);
			Debug.Log ("projectile : " + currentProjectile.name);
			projectileList.Add (currentProjectile);
			currentProjectile.transform.position = projectilePos;
			currentProjectile.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			currentProjectile.GetComponent<Rigidbody> ().velocity =  new Vector3 (- projectileDir.x, 1.0f, - projectileDir.z) * Random.Range(15.0f, 20.0f);
			currentProjectile.name = "Projectile";

			lastProjectileLaunchTime = Time.time;
			launchedProjectiles++;
		}

		projectileText.text = "Projectiles lancés : " + launchedProjectiles;
		collisionText.text = "Nombre de collisions : " + contacts;

	}

	public void startProjectileLaunch(){
		isLaunched = true;
		lastProjectileLaunchTime = Time.time;
	}

	public void stopProjectileLaunch(){
		isLaunched = false;
	}

	public void flushProjectileList(){
		foreach (GameObject projectile in projectileList.ToArray()) {
			projectileList.Remove (projectile);
			Destroy (projectile);
		}
	}
}
