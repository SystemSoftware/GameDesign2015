using UnityEngine;
using System.Collections;

public class VEngineDriverT3 : MonoBehaviour {

	public int sign = 0;
    Transform t;
    void start()
    {
        t = this.GetComponent<Transform>();
    }

    //protected override float Filter(float f)
    //{
    //    return sign == (int)Mathf.Sign(f) ? Mathf.Abs(f) : 0f;
    //}


    //protected override string FetchAxis(Controller ctrl)
    //{
    //    return ctrl.verticalAxis;
    //}

    void FixedUpdate()
    {
        t.Rotate(new Vector3(0,90,0));
    }
}
