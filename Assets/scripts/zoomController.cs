using UnityEngine;
using System.Collections;

public class zoomController : MonoBehaviour {

	public float scaleStep = 0.05f;
	public float maxScale = 100.0f;
	public float minScale = 1.0f;
	private Vector3 unitarScaleVector;


	// Use this for initialization
	void Start () {		
		unitarScaleVector = new Vector3(scaleStep,scaleStep,scaleStep);
	}


	void Update () {
			
		//Zoom out
		if (Input.GetKey ("i")) {
			Debug.Log ("Zoom Out");
			if (transform.localScale.x < maxScale) {
				
				transform.localScale +=unitarScaleVector;
				transform.position = new Vector3(transform.position.x ,transform.localScale.y,transform.position.z);
		
				builderController.g_cubeDistanceMin += scaleStep;
				builderController.g_cubeDistanceMax += scaleStep;
				builderController.g_cubeDistance += scaleStep;

			}
		}
		//Zoom in
		else if (Input.GetKey ("k")) {
			Debug.Log ("Zoom int");
			if (transform.localScale.x > minScale) {
				transform.localScale -= unitarScaleVector;
				transform.position = new Vector3(transform.position.x ,transform.localScale.y,transform.position.z);

				builderController.g_cubeDistanceMin -= scaleStep;
				builderController.g_cubeDistanceMax -= scaleStep;
				builderController.g_cubeDistance -= scaleStep;
			}
		}


	}

	public void zoomIn(){
		if (transform.localScale.x > minScale) {
			transform.localScale -= unitarScaleVector;
			transform.position = new Vector3(transform.position.x ,transform.localScale.y,transform.position.z);

			builderController.g_cubeDistanceMin -= scaleStep;
			builderController.g_cubeDistanceMax -= scaleStep;
			builderController.g_cubeDistance -= scaleStep;
		}
	}

	public void zoomOut(){
		if (transform.localScale.x < maxScale) {

			transform.localScale += unitarScaleVector;
			transform.position = new Vector3 (transform.position.x, transform.localScale.y, transform.position.z);

			builderController.g_cubeDistanceMin += scaleStep;
			builderController.g_cubeDistanceMax += scaleStep;
			builderController.g_cubeDistance += scaleStep;

		}
	}
}
