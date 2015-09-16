using UnityEngine;
using System.Collections;

public class T6Controller : Controller {

    public bool cameraLock = true;
    public Vector3 lookAt; //world space
    public Vector3 lookAtLocal; //local space
    public Vector3 target;
    public Vector3 targetLocal;
    public float acceleration;
    public float pitch;
    public float yaw;
    public float roll;
    public float strafeVertical;
    public float strafeHorizontal;
    private float pitchSum;
    private float yawSum;
    private float rollSum;
    GameObject g;
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
        g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(g.GetComponent<SphereCollider>());
        g.transform.position = transform.position + target;
        pitchSum = 0;
        yawSum = 0;
        rollSum = 0;
    }

	// Update is called once per frame
	void Update () {
        acceleration = Mathf.Max(0,Input.GetAxis(ctrlAxisAccelerate));
        roll = Input.GetAxis(ctrlAxisHorizontal);
        pitch = Input.GetAxis(ctrlAxisVertical);
        yaw = Input.GetAxis(ctrlAxisOther);
        strafeHorizontal = Input.GetAxis("T6StrafeHorizontal");
        strafeVertical = Input.GetAxis("T6StrafeVertical");
        pitchSum += pitch/50 % (2 * Mathf.PI);
        yawSum += yaw/100 % (2 * Mathf.PI);
        rollSum += -roll % 360;
        lookAt = transform.TransformPoint(new Vector3(0, 0, 200));
        Vector3 pitchPoint = new Vector3(0,200* Mathf.Sin(pitchSum),200* Mathf.Cos(pitchSum));
        Vector3 yawPoint = new Vector3(200*Mathf.Sin(yawSum), 0, 200*Mathf.Cos(yawSum));
        
        targetLocal = new Vector3(yawPoint.x, pitchPoint.y,Mathf.Min(pitchPoint.z,yawPoint.z));
        targetLocal = Quaternion.Euler(new Vector3(0, 0, rollSum)) * targetLocal;
        target = transform.position + targetLocal;
        g.transform.position = target;
        Debug.DrawLine(transform.position, g.transform.position);
        Debug.DrawLine(transform.position, lookAt);
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
