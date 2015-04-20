using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public Camera myCamera;

    public int      controlIndex;
    public string	accelerate, custom, 
					horizontalAxis, verticalAxis, otherAxis;

    protected Transform     shipTransform;


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
        Debug.Log("Setting ship transform");
        shipTransform = null;
        foreach (Transform child in transform)
        {
            if (shipTransform == null)
                shipTransform = child;
            else
            {
                Debug.LogWarning("Additional GameObject instance '"+child.name+"' found as child of '"+transform.name+"'");
            }
        }
        if (shipTransform != null)
        {
            Rigidbody body = shipTransform.GetComponent<Rigidbody>();
            if (body)
            {
                body.angularDrag = Level.angularDrag;
                body.drag = Level.drag;
                body.useGravity = true;
                body.mass = 10000f;
            }
            else
                Debug.LogWarning("Ship body '" + shipTransform.name + "' does not have a RigidBody component attached");
        }
        else
            Debug.LogWarning("Unable to find ship-child of '" + transform.name + "'. This WILL cause shit to hit the fan");
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
