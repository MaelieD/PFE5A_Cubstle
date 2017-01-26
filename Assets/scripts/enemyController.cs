using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {
	
	public int life; 
	public float speed;
	public bool attackBool;
	public bool walkBool;
	private Animator animator;   
	public Transform target;
	public enemySpawner g_enemySpawner;
	public bool start;

	float timeSinceLastAttack;
	float timeSinceLastYell;

	AudioSource[] audioSources;
	AudioSource swordSound;
	AudioSource yellingSound;

	enum states {IDLE, WALK, ATTACK};
	int currentState;

	Rigidbody m_rigidBody;
	//public builderController builderController;
	void Start () {
		currentState = (int)states.WALK;
		animator = GetComponent<Animator> ();
		attackBool = false;
		walkBool = true;
		m_rigidBody = GetComponent<Rigidbody> ();
		audioSources = GetComponents<AudioSource> ();
		swordSound = audioSources [0];
		yellingSound = audioSources [1];

		//target = GameObject.FindGameObjectWithTag ("mainTarget").transform; // King or flag 
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			float step = speed * Time.deltaTime;

			if (currentState == (int)states.IDLE) {
				idle ();
			}

			if (currentState == (int)states.WALK) {
				walk (step);
			}

			if (currentState == (int)states.ATTACK) {
				attack ();
			}
				
		}
		randomYell ();
			
	}

	void idle(){
		animator.SetBool ("walk", false);
		animator.SetBool ("attack", false);


	}

	void randomYell(){
		float randValue = Random.value;

		if (Time.time - timeSinceLastYell > 5.0f + Random.Range(0.0f, 5.0f)) {
			yellingSound.Play ();

			timeSinceLastYell = Time.time;
		}
	}

	void walk (float step)
	{
//		transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, 0.0f, target.position.z), step);
		m_rigidBody.velocity = (target.position - transform.position).normalized * 2.0f;
		transform.LookAt(new Vector3(target.position.x, 0.0f, target.position.z));

		animator.SetBool ("walk", true);
		animator.SetBool ("attack", false);
	

	}

	void attack ()
	{
		animator.SetBool ("walk", false);
		animator.SetBool ("attack", true);

		if (Time.time - timeSinceLastAttack > 1.0f) {
			swordSound.Play ();
			timeSinceLastAttack = Time.time;
		}


	}

	void death ()
	{
		g_enemySpawner.skeletorList.Remove (gameObject);
		Destroy (gameObject);
	}

	void OnCollisionStay (Collision col)
	{
		GameObject collidedTarget = col.gameObject;
		if (col.gameObject.name == "Placed Cube") {
			currentState = (int)states.ATTACK;
			transform.LookAt(new Vector3(collidedTarget.transform.position.x, 0, collidedTarget.transform.position.z ));
		}
			
		if (col.gameObject.name == "polest") {
			m_rigidBody.velocity = new Vector3 (0.0f, 0.0f, 0.0f);
			currentState = (int)states.ATTACK;
		}

		m_rigidBody.angularVelocity = new Vector3 (0.0f, 0.0f, 0.0f);
			
	}

	void OnCollisionExit(Collision col){
		currentState = (int)states.WALK;
	}

	public void startEnemy(){
		start = true;
	}

	public void stopEnemy(){
		start = false;
	}

}
