using UnityEngine;
using System.Collections;

//This is actually the Yaw Correction. I tried renaming/moving a LOT but unity still messes these 2 up -.-
public class T6PitchCorrection : MonoBehaviour
{

    T6Controller controller;
    float yaw;
    Vector3 lookAt;
    Vector3 target;
    Rigidbody ship;

    // Use this for initialization
    void Start()
    {
        controller = this.GetComponentInParent<T6Controller>();
        ship = this.transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lookAt = controller.lookAtLocal;
        target = controller.transform.InverseTransformPoint(controller.target);
        Vector3 angles = target - lookAt;
        float angle = 0;
        float rotation = controller.transform.InverseTransformVector(ship.angularVelocity).y;
        float yawCorrection = 0;
        if (angles.x < 180)
        {
            angle = angles.x;
        }
        else
        {
            angle = 360 - angles.x;
        }
        //If ship has no rotation towards target or negative rotation: apply force towards target
        //If ship has slow rotation towards targetand is further away than 20°: speed rotation up
        //If ship has fast rotation towards target and is closer than 20°: reduce rotation
        if (rotation == 0 || Mathf.Sign(rotation) != Mathf.Sign(angle))
        {

            yawCorrection = angle * 10;
            //apply force towards target
        }
        else if (Mathf.Abs(rotation) < 5 && Mathf.Abs(angle) > 20 && Mathf.Sign(rotation)==Mathf.Sign(angle))
        {
            yawCorrection = angle * 2;
            //apply MOAR FORCE
        }
        else
        {
            yawCorrection = angle * -5;
            //decrease rotationspeed
        }
        setForce(yawCorrection * 10);
    }
    void setForce(float f)
    {
        this.GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, f);
        this.GetComponent<ParticleSystem>().startSpeed = f / -20;
    }
}
