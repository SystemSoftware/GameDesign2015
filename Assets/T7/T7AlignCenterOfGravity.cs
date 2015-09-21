using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class T7AlignCenterOfGravity : MonoBehaviour {


    public DirectEngineDriver[] sources;

    public Vector3 currentCenter,
                    calculatedCenter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sources != null && sources.Length > 0)
        {
            Rigidbody body = GetComponent<Rigidbody>();
            Vector3 sum = new Vector3(0,0,0);
            currentCenter = body.centerOfMass;
            foreach (var force in sources)
            {
                Vector3 dir = new Vector3(0, 0, 1);   //lazy
                float d = Vector3.Dot(dir, currentCenter - force.transform.localPosition);
                Vector3 c = force.transform.localPosition + dir * d;
                sum += c;
            }
            calculatedCenter = sum / sources.Length;
            Vector3 offset = body.centerOfMass - calculatedCenter;
            foreach (var force in sources)
                force.offset = offset;
            //calculatedCenter.z = 0f;
            //calculatedCenter.x = 0f;
//            body.centerOfMass = calculatedCenter;
  //          if (Application.isPlaying)
    //            enabled = false;

        }


	}
}
