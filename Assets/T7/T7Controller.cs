using UnityEngine;
using System.Collections;

public class T7Controller : Controller {

	protected new void Start()
	{
		base.Start();
	}

	public Texture img;
	public int layer = 1 << 20;
	float maxSpeed = 1500;
    void OnGUI()
    {
        if (ctrlAttachedCamera != null && ctrlAttachedCamera.farClipPlane != 150000)
        {
            ctrlAttachedCamera.farClipPlane = 150000f;
        }
    }
	//Pro Frame
    void LateUpdate()
	{
        
		if (ctrlAttachedCamera != null) {
			Rigidbody comp = GetComponentInParent<Rigidbody> ();
            Vector3 idealLocation =
				transform.position - transform.forward * cameraIdealDistance + transform.up * cameraIdealYOffset;
			Vector3 deltaToIdeal = idealLocation - ctrlAttachedCamera.transform.position;
			ctrlAttachedCamera.transform.transform.position += deltaToIdeal;
			ctrlAttachedCamera.transform.RotateAround (transform.position, transform.right, (-77 * (Vector3.Dot (comp.velocity, transform.up)) / maxSpeed));
			idealLocation = ctrlAttachedCamera.transform.transform.position;
			RaycastHit hitinfo;
			float dist = Vector3.Distance (transform.position, idealLocation);
			if (Physics.Raycast (new Ray (transform.position, (idealLocation - transform.position).normalized), out hitinfo, dist, layer)) {
				ctrlAttachedCamera.transform.transform.position = Vector3.MoveTowards (hitinfo.point, transform.position, 2f);
			}
			ctrlAttachedCamera.transform.LookAt (transform.position);
		}
	}
	protected override void OnAssignCameraAndControl(){
	}
}