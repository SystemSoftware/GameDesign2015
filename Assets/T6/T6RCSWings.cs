using UnityEngine;
using System.Collections;

public class T6RCSWings : MonoBehaviour {

    T6Controller controller;
    float roll;
    float strafeUp;
    float strafeLeft;
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
        strafeUp = Input.GetAxis("T6StrafeVertical");
        strafeLeft = Input.GetAxis("T6StrafeHorizontal");
        flag = 1;
        if (this.transform.parent.name == "LRCS") {flag = -1; }
        Vector3 localAngularVelocity = ship.transform.InverseTransformDirection(ship.angularVelocity);
        Vector3 localVelocity = ship.transform.InverseTransformDirection(ship.velocity);
        float vertical;
        float horizontal;
        if (roll == 0)
        {
            vertical = localAngularVelocity.z * 500 * flag;
        }
        else
        {
            vertical = roll * 400 * flag;
        }
        if (strafeUp == 0)
        {
            vertical += localVelocity.y * -200;
        }
        else
        {
            vertical += strafeUp * 2000;
        }
        if (strafeLeft == 0)
        {
            horizontal = localVelocity.x * -200;
        }
        else
        {
            horizontal = strafeLeft * 2000 ;
        }

        setForce(vertical, horizontal);

	}

    void setForce(float f, float h)
    {
        this.GetComponent<ConstantForce>().relativeForce = new Vector3(h, 0, f);
        Debug.DrawLine(this.transform.position, this.transform.position + transform.TransformVector( new Vector3(h, 0, f)));
        this.GetComponent<ParticleSystem>().startSpeed = f/-6;
        Debug.Log(h);
    }


}
