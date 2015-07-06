using UnityEngine;
using System.Collections;

public class T2ViperDriver1 : DirectEngineDriver
{
    public int sign = 1;


    public string upAxis = null, rightAxis = null;

    float minForce = 0f;

    protected override float Filter(float f)
    {
        f = base.Filter(f);
        f = Mathf.Max(f, minForce);
        return f;
    }


	// Update is called once per frame
	protected new void Update () {
        base.Update();
        if (rightAxis == null || rightAxis.Length == 0)
        {
            rightAxis = GetComponentInParent<Controller>().ctrlAxisHorizontal;
        }
        if (rightAxis != null && rightAxis.Length != 0)
        {
            float f = Input.GetAxis(rightAxis);
            minForce = Mathf.Abs(f);
            this.transform.localEulerAngles = new Vector3(f * 15f * sign, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
        }



	}
}
