using UnityEngine;
using System.Collections;

public class T6YawCorrection : MonoBehaviour
{

    T6Controller controller;
    float yaw;
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
        Vector3 localAngularVelocity = ship.transform.InverseTransformDirection(ship.angularVelocity);
        yaw = controller.yaw;
        if (yaw ==0)
        {
            setForce(localAngularVelocity.y * -5000);
        }
        else
        {
            setForce(yaw * 1800);
        }

    }
    void setForce(float f)
    {
        this.GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, f);
        this.GetComponent<ParticleSystem>().startSpeed = f / -20;
    }
}
