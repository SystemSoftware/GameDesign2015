﻿using UnityEngine;
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
        //Try to stop current rotation
    if (((angle < 5 && angle > 0) || angle > 355) || (controller.yaw ==0 && Mathf.Sign(angle)==Mathf.Sign(rotation) && Mathf.Abs(angle)>20))
        {
            if (controller.yaw > -0.05 && controller.yaw < 0.05)
            {
                yawCorrection = rotation * -700;
            }

        }
        //Generate rotation towards target
        else
        {
            if ((controller.yaw > 0 && angle < 0) || (controller.yaw < 0 && angle > 0))
            {
                yawCorrection = rotation * -20;
            }
            else
            {
                yawCorrection = angle*2;
            }

        }
        setForce(yawCorrection * 10);
    }
    void setForce(float f)
    {
        this.GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, f);
        this.GetComponent<ParticleSystem>().startSpeed = f / -20;
    }
}
