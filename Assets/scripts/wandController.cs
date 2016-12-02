using UnityEngine;
using System.Collections;

public class wandController : SteamVR_TrackedController {

	public SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int) controllerIndex); } }
	public Vector3 velocity { get { return controller.velocity; } }
	public Vector3 angularVelocity { get { return controller.angularVelocity; } }
	builderController m_builderController;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		m_builderController = GetComponent<builderController> ();
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
		base.OnTriggerClicked(e);
		m_builderController.setTriggerClicked (true);

	}

	public override void OnTriggerUnclicked(ClickedEventArgs e)
	{
		base.OnTriggerUnclicked(e);
		m_builderController.setTriggerClicked (false);
	}

	public override void OnMenuClicked(ClickedEventArgs e)
	{
		base.OnMenuClicked(e);
		m_builderController.setMenuClicked (true);
	}

	public override void OnMenuUnclicked(ClickedEventArgs e)
	{
		base.OnMenuUnclicked(e);
		m_builderController.setMenuClicked (false);
	}

	public override void OnSteamClicked(ClickedEventArgs e)
	{
		base.OnSteamClicked(e);
	}

	public override void OnPadClicked(ClickedEventArgs e)
	{
		base.OnPadClicked(e);
		m_builderController.setPadClicked (true);
	}

	public override void OnPadUnclicked(ClickedEventArgs e)
	{
		base.OnPadUnclicked(e);
		m_builderController.setPadClicked (false);
	}

	public override void OnPadTouched(ClickedEventArgs e)
	{
		base.OnPadTouched(e);
		m_builderController.setPadTouched (true);
	}

	public override void OnPadUntouched(ClickedEventArgs e)
	{
		base.OnPadUntouched(e);
		m_builderController.setPadTouched (false);
	}

	public override void OnGripped(ClickedEventArgs e)
	{
		base.OnGripped(e);
		m_builderController.setGripClicked (true);
	}

	public override void OnUngripped(ClickedEventArgs e)
	{
		base.OnUngripped(e);
		m_builderController.setGripClicked (false);
	}
}
