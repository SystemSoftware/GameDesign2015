using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class VThrusterAutoAlign : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!Application.isPlaying)
        {

            Rigidbody rb = this.transform.parent.GetComponentInParent<Rigidbody>();
            if (rb)
            {
                Vector3 center = rb.worldCenterOfMass;
                Vector3 delta = this.transform.position - center;
                Debug.Log(center);
                Debug.Log(delta);
                delta.x = 0;

                Vector4 dir = rb.transform.worldToLocalMatrix * new Vector4(delta.x,delta.y,delta.z, 0f);

                float ax = 180f - Mathf.Atan2(dir.y, dir.z) * 180f / Mathf.PI;
                Debug.Log(ax);
                this.transform.rotation = Quaternion.Euler(ax, 0, 0);

                //this.transform.LookAt(this.transform.position- delta);


            }


        }
        else
            this.enabled = false;

	}
}
