using UnityEngine;
using System.Collections;

public class wandController : SteamVR_TrackedController {

	public SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int) controllerIndex); } }
	public Vector3 velocity { get { return controller.velocity; } }
	public Vector3 angularVelocity { get { return controller.angularVelocity; } }

	public enum m_pressStates {PRESSED, PRESSING, UNPRESSED, IDLE};
	public enum m_touchStates {TOUCHED, TOUCHING, UNTOUCHED, IDLE};

	public bool isReady;

	public int m_triggerState;
	public int m_menuState;
	public int m_padPressState;
	public int m_padTouchState;
	public int m_gripState;

	public float m_triggerAxis;
	public Vector2 m_padAxis;

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		isReady = false;

		m_triggerState = (int)m_pressStates.IDLE;
		m_menuState = (int)m_pressStates.IDLE;
		m_padPressState = (int)m_pressStates.IDLE;
		m_padTouchState = (int)m_touchStates.IDLE;
		m_gripState = (int)m_pressStates.IDLE;

		m_triggerAxis = 0.0f;
		m_padAxis = new Vector2 (0.0f, 0.0f);

		isReady = true;

	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

//		if (m_triggerState == (int)m_pressStates.PRESSED) {
//			Debug.Log ("wand controller : trigger pressed");
//		}

		Debug.Log ("wand controller : trigger state : " + m_triggerState);

		m_triggerAxis = GetTriggerAxis ();
		m_padAxis = GetTouchpadAxis ();
	}

	int setContinuousPressState(int buttonState){
		if (buttonState == (int)m_pressStates.PRESSED) {
			buttonState = (int)m_pressStates.PRESSING;
		} else if (buttonState == (int)m_pressStates.UNPRESSED) {

			buttonState = (int)m_pressStates.IDLE;
		}

		return buttonState;
	}

	int setContinuousTouchState(int buttonState){
		if (buttonState == (int)m_touchStates.TOUCHED) {
			buttonState = (int)m_touchStates.TOUCHING;
		} else if (buttonState == (int)m_touchStates.UNTOUCHED) {
			buttonState = (int)m_touchStates.IDLE;
		}

		return buttonState;
	}

	public void setContinuousMode(){
		m_triggerState = setContinuousPressState (m_triggerState);
		m_menuState = setContinuousPressState (m_menuState);
		m_padPressState = setContinuousPressState (m_padPressState);
		m_padTouchState = setContinuousTouchState (m_padTouchState);
		m_gripState = setContinuousPressState (m_gripState);
	}

	public float GetTriggerAxis()
	{
		// If the controller isn't valid, return 0
		if (controller == null)
			return 0;

		// Use SteamVR_Controller.Device's GetAxis() method (mentioned earlier) to get the trigger's axis value
		return controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x;
	}

	public Vector2 GetTouchpadAxis()
	{
		// If the controller isn't valid, return (basically) 0
		if (controller == null)
			return new Vector2();

		// Use SteamVR_Controller.Device's GetAxis() method (mentioned earlier) to get the touchpad's axis value
		return controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
	}

	public override void OnTriggerClicked(ClickedEventArgs e)
	{
		base.OnTriggerClicked (e);
		setTriggerClicked (true);

	}

	public override void OnTriggerUnclicked(ClickedEventArgs e)
	{
		base.OnTriggerUnclicked(e);
		setTriggerClicked (false);
	}

	public override void OnMenuClicked(ClickedEventArgs e)
	{
		base.OnMenuClicked(e);
		setMenuClicked (true);
	}

	public override void OnMenuUnclicked(ClickedEventArgs e)
	{
		base.OnMenuUnclicked(e);
		setMenuClicked (false);
	}

	public override void OnSteamClicked(ClickedEventArgs e)
	{
		base.OnSteamClicked(e);
	}

	public override void OnPadClicked(ClickedEventArgs e)
	{
		base.OnPadClicked(e);
		setPadClicked (true);
	}

	public override void OnPadUnclicked(ClickedEventArgs e)
	{
		base.OnPadUnclicked(e);
		setPadClicked (false);
	}

	public override void OnPadTouched(ClickedEventArgs e)
	{
		base.OnPadTouched(e);
		setPadTouched (true);
	}

	public override void OnPadUntouched(ClickedEventArgs e)
	{
		base.OnPadUntouched(e);
		setPadTouched (false);
	}

	public override void OnGripped(ClickedEventArgs e)
	{
		base.OnGripped(e);
		setGripClicked (true);
	}

	public override void OnUngripped(ClickedEventArgs e)
	{
		base.OnUngripped(e);
		setGripClicked (false);
	}

	public void setTriggerClicked(bool isClicked){
		if (isClicked) {
			m_triggerState = (int)m_pressStates.PRESSED;
		} else {
			m_triggerState = (int)m_pressStates.UNPRESSED;
		}
	}

	public void setMenuClicked(bool isClicked){
		if (isClicked) {
			m_menuState = (int)m_pressStates.PRESSED;
		} else {
			m_menuState = (int)m_pressStates.UNPRESSED;
		}
	}

	public void setPadClicked(bool isClicked){
		if (isClicked) {
			m_padPressState = (int)m_pressStates.PRESSED;
		} else {
			m_padPressState = (int)m_pressStates.UNPRESSED;
		}
	}

	public void setPadTouched(bool isTouched){
		if (isTouched) {
			m_padTouchState = (int)m_touchStates.TOUCHED;
		} else {
			m_padTouchState = (int)m_touchStates.UNTOUCHED;
		}
	}

	public void setGripClicked(bool isClicked){
		if (isClicked) {
			m_gripState = (int)m_pressStates.PRESSED;
		} else {
			m_gripState = (int)m_pressStates.UNPRESSED;
		}
	}
}
