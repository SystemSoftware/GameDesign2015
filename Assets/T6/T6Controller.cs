using UnityEngine;
using System.Collections;

public class T6Controller : Controller {

    public bool cameraLock = true;

	// Update is called once per frame
	void Update () {
        float acceleration = Input.GetAxis(ctrlAxisAccelerate)+1;
        float horizontal = Input.GetAxis(ctrlAxisHorizontal);
        float vertical = Input.GetAxis(ctrlAxisVertical);
        float yaw = Input.GetAxis(ctrlAxisOther);
        foreach (RotateThrustFlaps s in this.GetComponentsInChildren<RotateThrustFlaps>())
        {
            s.Rotate(acceleration, vertical, yaw);
        }
	}


    //LateUpdate for smooth custom camera follow
   void LateUpdate()
    {
        Transform target = this.transform;
        Transform camera = ctrlAttachedCamera.transform;
        Vector3 forward = target.forward * 400.0f;
        Vector3 needPos = target.position - forward + (target.up * 50);

        if (cameraLock) { 
        camera.LookAt(target.transform);
        camera.position = needPos;
        camera.rotation = target.rotation;
   }
    }
}
