using UnityEngine;
using System.Collections;

/**
 * Base controller class, foundation for every ship controller class
 **/
public class Controller : MonoBehaviour {

	public Camera myCamera;


    public int      controlIndex;   //!< Index [0,3] in control of the local ship instance.
    public string	accelerate,     //!< Axis name used to accelerate (or brake) the ship
                    custom,         //!< Axis name used to issue a custom command
					horizontalAxis, //!< Axis name used to query the horizontal axis of the local joystick
                    verticalAxis,   //!< Axis name used to query the vertical axis of the local joystick
                    otherAxis;      //!< Axis name used to query the rotational axis of the local joystick (if supported)




    /**
     * Called automatically to assemble all joystick axes from the specified control index
     **/
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


    /**
     * Called once a new camera and control index have been assigned to the local ship
     * All local member variables have been initialized when this method is called
     **/
	protected virtual void OnAssignCameraAndControl()
	{}


	/**
     * Local Start() method. Make sure that any inheriting class calls this method inside its own Start() method
     **/
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


        //ParticleSystem[] systems = GetComponentsInChildren<ParticleSystem>();
        //if (systems != null)
        //{
        //    foreach (ParticleSystem sys in systems)
        //    {
        //        sys.

        //    }

        //}

	}
	
}
