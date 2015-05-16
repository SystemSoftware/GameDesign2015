using UnityEngine;
using System.Collections;

public class T7Controller : Controller {

	protected new void Start()
	{
		base.Start();
	}

	//Pro Frame
	void LateUpdate () {
		if (ctrlAttachedCamera != null)
		{
			//Vector3 delta = target.position - this.transform.position;
			
			Vector3 idealLocation =
				transform.position - transform.forward * cameraIdealDistance + transform.up * cameraIdealYOffset;
			//target.position - delta.normalized * idealDistance;
			//(delta - target.up * Vector3.Dot(delta, target.up)).normalized * idealDistance + target.up * idealYOffset;
			Vector3 deltaToIdeal = idealLocation - ctrlAttachedCamera.transform.position;
			ctrlAttachedCamera.transform.transform.position += deltaToIdeal * (1.0f - Mathf.Pow(0.02f, Time.deltaTime));
			ctrlAttachedCamera.transform.rotation = Quaternion.Lerp(ctrlAttachedCamera.transform.rotation, transform.rotation, (1.0f - Mathf.Pow(0.02f, Time.deltaTime)));
			;
			ctrlAttachedCamera.transform.LookAt(transform.position);

		}
	}
	protected override void OnAssignCameraAndControl(){
	}
}
