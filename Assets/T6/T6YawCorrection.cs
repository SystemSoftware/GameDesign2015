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
            Debug.Log("OH HI");
            angle= 360 - angles.y;
        }
       if (rotation == 0 || Mathf.Sign(rotation) == Mathf.Sign(angle))
        {

            pitchCorrectionAngle = angle * 10;
            //apply force towards target
        }
        else if (Mathf.Abs(rotation) < 5 && Mathf.Abs(angle) > 20 && Mathf.Sign(rotation)!=Mathf.Sign(angle))
        {
            pitchCorrectionAngle = angle * 2;
            //apply MOAR FORCE
        }
        else
        {
            pitchCorrectionAngle = angle * -5;
            //decrease rotationspeed
        }

        setForce(pitchCorrectionAngle*10);

	}
    void setForce(float f)
    {
        this.GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, f);
        this.GetComponent<ParticleSystem>().startSpeed = f / -20;
    }
}
