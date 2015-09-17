using UnityEngine;
using System.Collections;

public class T6Controller : Controller {

    public bool cameraLock = true;
    public Vector3 lookAt; //world space
    public Vector3 lookAtLocal;
    public Vector3 target;
    public Vector3 targetLocal;
    public float acceleration;
    public float pitch;
    public float yaw;
    public float roll;
    public float strafeVertical;
    public float strafeHorizontal;
    private GameObject targetObject;
    private float yawSum;
    private float pitchSum;
    public bool decoupled;
    private int timeout;
    /*
     * New Control system:
     * The players input controls the position of a point on a spere around the ship. 
     * The ship automatically tries to line up with this point as forward.
     * On strafing, the point is moved directly, the ship is getting pushed by the force of the RCS thrusters
     * 
     * */


    void Start()
    {
        base.Start();
        base.cameraIdealDistance = 150f;
        lookAtLocal = new Vector3(0, 0, 200);
        lookAt = transform.TransformPoint(lookAtLocal);
        
        target = lookAt;
        targetLocal = lookAtLocal;
        targetObject = new GameObject();
        targetObject.transform.position = target;
        yawSum = 0;
        pitchSum = 0;
        decoupled = false;
    }

	// Update is called once per frame
	void Update () {
        if (GetComponentsInParent<T6Controller>().Length == 1)
        {
            if (Input.GetAxis("T6StrafeHorizontal") == -1 && Input.GetButton("T6Fire") && timeout == 0)
            {
                decoupled = !decoupled;
                timeout = 100;
                Debug.Log("Switched Decoupled");
            }
            strafeHorizontal = Input.GetAxis("T6StrafeHorizontal");
            strafeVertical = Input.GetAxis("T6StrafeVertical");
        }
        timeout = Mathf.Max(timeout - 1, 0);
        acceleration = Mathf.Max(0,Input.GetAxis(ctrlAxisAccelerate));
        roll = Input.GetAxis(ctrlAxisHorizontal);
        pitch = -Input.GetAxis(ctrlAxisVertical);
        yaw = Input.GetAxis(ctrlAxisOther);
        
        lookAt = transform.TransformPoint(new Vector3(0, 0, 200));
        Vector3 rotation = transform.InverseTransformVector(GetComponent<Rigidbody>().angularVelocity);
        yawSum += (yaw - rotation.y)% 360;
        pitchSum += (pitch*1.5f+rotation.x) % 360;
        targetObject.transform.position = lookAt;
        targetObject.transform.RotateAround(transform.position, transform.up, yawSum);
        targetObject.transform.RotateAround(transform.position, transform.right, -pitchSum);
        target = targetObject.transform.position;
        Debug.DrawLine(transform.position, targetObject.transform.position);
        Debug.DrawLine(transform.position, lookAt);
        if (target.magnitude < 100 && ((target - lookAt).x+(target - lookAt).y)<20) { target = lookAt; targetObject.transform.position = target; }
        foreach (T6RotateThrustFlaps s in this.GetComponentsInChildren<T6RotateThrustFlaps>())
        {
            s.Rotate(acceleration, pitch, yaw);
        }
       
	}


    //LateUpdate for smooth custom camera follow
   /*void LateUpdate()
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
    }*/
}
