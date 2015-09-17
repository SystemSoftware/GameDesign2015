using UnityEngine;
using System.Collections;

//This is actually the Pitch Correction. I tried renaming/moving a LOT but unity still messes these 2 up -.-
public class T6YawCorrection : MonoBehaviour {

    T6Controller controller;
    Vector3 lookAt;
    Vector3 target;
    Rigidbody ship;

    // Use this for initialization
	void Start () {
        controller = this.GetComponentInParent<T6Controller>();
        ship = this.transform.parent.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
    //rotation upward = negative
    //angle upward = positive
	void FixedUpdate () {
        lookAt = controller.lookAtLocal;
        target = controller.transform.InverseTransformPoint(controller.target);
        Vector3 angles = target-lookAt;
        float angle = 0;
        float rotation = controller.transform.InverseTransformVector(ship.angularVelocity).x;
        float pitchCorrectionAngle = 0;
        if (angles.y < 180)
        {
            angle = angles.y;
        }
        else
        {
            angle= 360 - angles.y;
        }
        //Try to stop current rotation
        if (((angle < 5 && angle > 0) || angle > 355) || (controller.pitch ==0 && Mathf.Sign(angle)==Mathf.Sign(rotation) && Mathf.Abs(angle)>20))
        {
            if (controller.pitch > -0.05 && controller.pitch < 0.05)
            {
                pitchCorrectionAngle = rotation * 1000;
            }

        }
        //Generate rotation towards target
        else
        {
            if ((controller.pitch > 0 && angle < 0) || (controller.pitch < 0 && angle > 0))
            {
                pitchCorrectionAngle = rotation * 25;
            }
            else
            {
                pitchCorrectionAngle = angle * 2;
            }

        }
        setForce(pitchCorrectionAngle*10);

	}
    void setForce(float f)
    {
        this.GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, f);
        this.GetComponent<ParticleSystem>().startSpeed = f / -20;
    }
}
