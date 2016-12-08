using UnityEngine;
using System.Collections;

public class teleportationController : MonoBehaviour {

	public GameObject leftController;
	enum m_pressStates {PRESSED, PRESSING, UNPRESSED, IDLE};
	int m_triggerState;
	ReticlePoser reticlePoser;
	wandController wC;

	// Use this for initialization
	void Start () {
		reticlePoser = GetComponent<ReticlePoser> ();
		wC = leftController.GetComponent<wandController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (reticlePoser.hitTarget.name == "Game Zone Play") {
			
		}
	
	}

	int setContinuousPressState(int buttonState){
		if (buttonState == (int)m_pressStates.PRESSED) {
			buttonState = (int)m_pressStates.PRESSING;
		} else if (buttonState == (int)m_pressStates.UNPRESSED) {

			buttonState = (int)m_pressStates.IDLE;
		}

		return buttonState;
	}

	public void setTriggerClicked(bool isClicked){
		if (isClicked) {
			m_triggerState = (int)m_pressStates.PRESSED;
		} else {
			m_triggerState = (int)m_pressStates.UNPRESSED;
		}
	}
		
}
