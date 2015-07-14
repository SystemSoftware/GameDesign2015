using UnityEngine;
using System.Collections;

public class T6Controller : Controller {

    public bool cameraLock = true;

    void Start()
    {
        base.Start();
    }

	// Update is called once per frame
	void Update () {
        float acceleration = Mathf.Max(0,Input.GetAxis(ctrlAxisAccelerate));
        float horizontal = Input.GetAxis(ctrlAxisHorizontal);
        float vertical = Input.GetAxis(ctrlAxisVertical);
        float yaw = Input.GetAxis(ctrlAxisOther);
        foreach (T6RotateThrustFlaps s in this.GetComponentsInChildren<T6RotateThrustFlaps>())
        {
            s.Rotate(acceleration, vertical, yaw);
        }
	}


    //LateUpdate for smooth custom camera follow
   void LateUpdate()
    {
        Transform target = this.transform;
        Transform camera = ctrlAttachedCamera.transform;
        Vector3 forward = target.forward * 200.0f;
        Vector3 needPos = target.position - forward + (target.up * 30);

        if (cameraLock) { 
        camera.LookAt(target.transform);
        camera.position = needPos;
        camera.rotation = target.rotation;
   }
    }
}
