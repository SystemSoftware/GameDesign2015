using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public Camera myCamera;

    public int      controlIndex;
    public string	accelerate, custom, 
					horizontalAxis, verticalAxis, otherAxis;



	public void AssignCameraAndControl(Camera camera, int controlIndex)
	{
        horizontalAxis = "Horizontal" + controlIndex;
        verticalAxis = "Vertical" + controlIndex;
        accelerate = "Accelerate" + controlIndex;
        otherAxis = "Other" + controlIndex;

		myCamera = camera;
        this.controlIndex = controlIndex;
		OnAssignCameraAndControl();
	}



	protected virtual void OnAssignCameraAndControl()
	{}


	// Use this for initialization
	protected void Start ()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body)
        {
            body.angularDrag = Level.angularDrag;
            body.drag = Level.drag;
            body.useGravity = true;
            body.mass = 10000f;
            //Debug.Log("Set drag to " + body.angularDrag);
        }
        else
            Debug.LogWarning("Ship body '" + name + "' does not have a RigidBody component attached");
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
