using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class AlignCenterOfGravity : MonoBehaviour {


    public DirectEngineDriver[] engines;

    public Vector3 currentCenter,
                    calculatedCenter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (engines != null && engines.Length > 0)
        {
            Rigidbody body = GetComponent<Rigidbody>();
            Vector3 sum = new Vector3(0,0,0);
            currentCenter = body.worldCenterOfMass;
            foreach (var force in engines)
            {
                Vector3 dir = force.transform.forward;
                    //new Vector3(0, 0, 1);   //lazy
                float d = Vector3.Dot(dir, currentCenter - force.transform.position);
                Vector3 c = force.transform.position + dir * d;
                sum += c;
            }
            calculatedCenter = sum / engines.Length;
            Vector3 offset = body.worldCenterOfMass - calculatedCenter;
            foreach (var force in engines)
                force.offset = transform.worldToLocalMatrix * new Vector4(offset.x, offset.y, offset.z, 0f);
            this.enabled = false;
                    //offset;
            //calculatedCenter.z = 0f;
            //calculatedCenter.x = 0f;
//            body.centerOfMass = calculatedCenter;
  //          if (Application.isPlaying)
    //            enabled = false;

        }


	}
}
