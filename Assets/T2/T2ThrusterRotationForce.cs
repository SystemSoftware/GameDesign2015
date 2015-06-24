using UnityEngine;
using System.Collections;

public class T2ThrusterRotationForce : MonoBehaviour {

    float baseAngle;
	// Use this for initialization
	void Start () {
        baseAngle = this.transform.localEulerAngles.x;
	}


    string hAxis, vAxis;

	// Update is called once per frame
	void Update () {
        if (hAxis == null || hAxis.Length == 0)
        {
            hAxis = GetComponentInParent<Controller>().ctrlAxisHorizontal;
            vAxis = GetComponentInParent<Controller>().ctrlAxisVertical;
        }

        this.transform.localEulerAngles = (new Vector3(baseAngle - Input.GetAxis(vAxis) * 15f, 0f, 0f));

      //  this.GetComponent<ConstantForce>().relativeTorque = new Vector3(Input.GetAxis(vAxis), 0,-Input.GetAxis(hAxis)) * 200f;

	}
}
