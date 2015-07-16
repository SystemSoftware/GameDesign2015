﻿using UnityEngine;
using System.Collections;

public class T6RCSWings : MonoBehaviour {

    T6Controller controller;
    float roll;
    Rigidbody ship;
    float flag;
	// Use this for initialization
	void Start () {
        controller = this.GetComponentInParent<T6Controller>();
        ship = this.transform.parent.parent.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        roll = Input.GetAxis(controller.ctrlAxisHorizontal);
        flag = 1;
        if (this.transform.parent.name == "LRCS") {flag = -1; }
        Vector3 localAngularVelocity = ship.transform.InverseTransformDirection(ship.angularVelocity);

        if (roll == 0)
        {
            setForce(localAngularVelocity.z * 500 * flag);
        }
        else
        {
            setForce(roll * 400 * flag);
        }

	}

    void setForce(float f)
    {
        this.GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, f);
        this.GetComponent<ParticleSystem>().startSpeed = f/-6;
    }


}
