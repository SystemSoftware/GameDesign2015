using UnityEngine;
using System.Collections;

public class T2ViperDriver0 : DirectEngineDriver {

	// Use this for initialization
	new void Start () {
        base.Start();
	}

    public string upAxis = null,rightAxis = null;

    //protected override float Filter(float f)
    //{
    //    float rs = Mathf.Max(f, 0f);
    //    if (upAxis == null || upAxis.Length == 0)
    //    {
    //        upAxis = GetComponentInParent<Controller>().ctrlAxisVertical;
    //    }
    //    if (upAxis != null && upAxis.Length != 0)
    //    {
    //        //Debug.Log(Input.GetAxis(upAxis));
    //        rs = Mathf.Clamp(f + Input.GetAxis(upAxis) * 0.5f, 0f, 2f);
    //    }
    //    return rs;


    //}


    protected new void Update()
    {
        base.Update();

        if (rightAxis == null || rightAxis.Length == 0)
        {
            rightAxis = GetComponentInParent<Controller>().ctrlAxisHorizontal;
        }
        if (rightAxis != null && rightAxis.Length != 0)
        {
         //   float f = Input.GetAxis(rightAxis);
           // this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, f * 15f, this.transform.localEulerAngles.z);
        }


    }


}
