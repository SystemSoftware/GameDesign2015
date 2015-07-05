using UnityEngine;
using System.Collections;

public class T6EngineDriver : EngineDriver {

    string pitch;
    string yaw;

    void Start()
    {
        base.Start();
    }

	// Override EngineDriver Update function to scale the input without a backward range
    void Update()
    {

        
        //bool enabled = Input.GetKey(key);

        if (axis == null || axis.Length == 0)
            axis = GetComponentInParent<T6Controller>().ctrlAxisAccelerate;
        {
            float f = 0f;
            if (axis != null)
            {
                f = (Input.GetAxis(axis)+1)/2f;
            }
            if (sys != null)
            {
                sys.enableEmission = f != 0f;
                sys.startLifetime = f * maxLifetime;
            }

            if (force != null)
            {
               force.relativeForce = new Vector3(0, 0, maxForce * f);
               Debug.DrawRay(this.force.transform.position, this.force.transform.forward * 50);

            }
        }


    }
}
