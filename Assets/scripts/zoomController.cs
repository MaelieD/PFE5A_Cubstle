using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class zoomController : MonoBehaviour {

	public float scaleStep = 0.01f;
	public float maxScale = 10.0f;
	public float minScale = 1.0f;
	public GameObject cameraRig;
	public float scale = 1.0f;

	public Text scaleText;


	// Use this for initialization
	void Start () {		
		cameraRig.transform.localScale = new Vector3(scale, scale, scale);
	}


	void Update () {
			

	}

	public void zoom(int zoomCoef){

//		Debug.Log("scale : " + scale + " min scale : " + minScale + " maxScale : " + maxScale + " zoomCoef : " + zoomCoef);

		if((scale < maxScale && zoomCoef == 1) || ( scale > minScale && zoomCoef == -1)){
			
			scale += zoomCoef * scaleStep;
			float zoomFactor = scale / minScale;
			cameraRig.transform.localScale = new Vector3(minScale, minScale, minScale) * zoomFactor;
//			cameraRig.transform.position = new Vector3 (cameraRig.transform.position.x, cameraRig.transform.localScale.y, cameraRig.transform.position.z);

			builderController.g_currentCubeDistanceMin = builderController.g_cubeDistanceMin * zoomFactor;
			builderController.g_currentCubeDistanceMax = builderController.g_cubeDistanceMax * zoomFactor;
			builderController.g_currentCubeDistance = builderController.g_cubeDistance * zoomFactor;

//			Debug.Log ("scale : " + scale + " zoomFactor : " + zoomFactor + " distance : " + builderController.g_currentCubeDistance + " distanceMin : " + builderController.g_currentCubeDistanceMin + " distanceMax : " + builderController.g_currentCubeDistanceMax);

			updateScaleText ();
		}
	}

	void updateScaleText(){
		scaleText.text = "Taille : " + scale;
	}
}
