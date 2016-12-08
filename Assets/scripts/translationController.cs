using UnityEngine;
using System.Collections;

public class translationController : MonoBehaviour {

	public GameObject cameraRig;
	int m_padPressState;
	enum m_pressStates {PRESSED, PRESSING, UNPRESSED, IDLE};
	wandController m_wandController;

	// Use this for initialization
	void Start () {
		m_padPressState = (int)m_pressStates.IDLE;
		m_wandController = GetComponent<wandController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (m_padPressState == (int)m_pressStates.PRESSING) {
			Vector2 touchPadAxis = m_wandController.GetTouchpadAxis ();
			cameraRig.transform.Translate (- 0.1f * touchPadAxis.x, 0, - 0.1f * touchPadAxis.y);
		}
	
		m_padPressState = setContinuousPressState (m_padPressState);

	}

	public void setPadClicked(bool isClicked){
		if (isClicked) {
			m_padPressState = (int)m_pressStates.PRESSED;
		} else {
			m_padPressState = (int)m_pressStates.UNPRESSED;
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

}
