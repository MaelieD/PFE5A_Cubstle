using UnityEngine;
using System.Collections;

public class catapultController : MonoBehaviour {

	public GameObject projectile;

	float lastProjectileLaunchTime;
	bool isLaunched;

	// Use this for initialization
	void Start () {
		isLaunched = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (isLaunched && Time.time - lastProjectileLaunchTime > 1.0f) {
			Vector3 projectilePos = new Vector3 (Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
			Vector3 projectileDir = Vector3.Normalize(projectilePos);
			projectilePos = projectileDir * 50.0f;

			GameObject currentProjectile = GameObject.Instantiate (projectile);
			currentProjectile.transform.position = projectilePos;
			currentProjectile.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			currentProjectile.GetComponent<Rigidbody> ().velocity =  new Vector3 (- projectileDir.x, 1.0f, - projectileDir.z) * Random.Range(15.0f, 20.0f);
			currentProjectile.name = "Projectile";

			lastProjectileLaunchTime = Time.time;
		}
	}

	public void startProjectileLaunch(){
		isLaunched = true;
		lastProjectileLaunchTime = Time.time;
	}
}
