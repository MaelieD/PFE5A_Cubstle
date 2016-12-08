using UnityEngine;
using System.Collections;

public class wandController : SteamVR_TrackedController {

	public SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int) controllerIndex); } }
	public Vector3 velocity { get { return controller.velocity; } }
	public Vector3 angularVelocity { get { return controller.angularVelocity; } }

	public bool isTeleportationController;


	builderController m_builderController;
	translationController m_translationController;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if (isTeleportationController) {
			m_translationController = GetComponent<translationController> ();

		} else {
			m_builderController = GetComponent<builderController> ();
		}


	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	
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
		if (isTeleportationController) {
			
		} else {
			
			m_builderController.setTriggerClicked (true);
		}

	}

	public override void OnTriggerUnclicked(ClickedEventArgs e)
	{
		base.OnTriggerUnclicked(e);
		if (isTeleportationController) {

		} else {
			m_builderController.setTriggerClicked (false);
		}
	}

	public override void OnMenuClicked(ClickedEventArgs e)
	{
		base.OnMenuClicked(e);
		if (isTeleportationController) {
		} else {
			m_builderController.setMenuClicked (true);
		}
	}

	public override void OnMenuUnclicked(ClickedEventArgs e)
	{
		base.OnMenuUnclicked(e);
		if (isTeleportationController) {
		} else {
			
			m_builderController.setMenuClicked (false);
		}
	}

	public override void OnSteamClicked(ClickedEventArgs e)
	{
		base.OnSteamClicked(e);
	}

	public override void OnPadClicked(ClickedEventArgs e)
	{
		base.OnPadClicked(e);
		if (isTeleportationController) {
			m_translationController.setPadClicked (true);
		} else {
			m_builderController.setPadClicked (true);
		}

	}

	public override void OnPadUnclicked(ClickedEventArgs e)
	{
		base.OnPadUnclicked(e);
		if (isTeleportationController) {
			m_translationController.setPadClicked (false);
		} else {
			m_builderController.setPadClicked (false);
		}
	}

	public override void OnPadTouched(ClickedEventArgs e)
	{
		base.OnPadTouched(e);
		if (isTeleportationController) {
		} else {
			m_builderController.setPadTouched (true);
		}
	}

	public override void OnPadUntouched(ClickedEventArgs e)
	{
		base.OnPadUntouched(e);
		if (isTeleportationController) {
		} else {
			m_builderController.setPadTouched (false);
		}
	}

	public override void OnGripped(ClickedEventArgs e)
	{
		base.OnGripped(e);
		if (isTeleportationController) {
		} else {
			
			m_builderController.setGripClicked (true);
		}
	}

	public override void OnUngripped(ClickedEventArgs e)
	{
		base.OnUngripped(e);
		if (isTeleportationController) {
		} else {
			m_builderController.setGripClicked (false);
		}
	}
}
