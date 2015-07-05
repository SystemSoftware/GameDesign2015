using UnityEngine;
using System.Collections;

public class T6YawCorrection : MonoBehaviour {

    T6Controller controller;
    float yaw;
    float lastForce;
    Rigidbody ship;

    // Use this for initialization
	void Start () {
        controller = this.GetComponentInParent<T6Controller>();
        ship = this.transform.parent.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 localAngularVelocity = ship.transform.InverseTransformDirection(ship.angularVelocity);
        yaw = Input.GetAxis(controller.ctrlAxisVertical);
        if (yaw == 0)
        {
            setForce(localAngularVelocity.x * 5000);
        }
        else
        {
            setForce(-yaw*3000);
        }
        
	}
    void setForce(float f)
    {
        if (this.name == "YawDownCorrection") { f = -f; }
        if (f >= 0)
        {
            this.GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, f);
            this.GetComponent<ParticleSystem>().startSpeed = f / -20;
        }
        
    }
}
