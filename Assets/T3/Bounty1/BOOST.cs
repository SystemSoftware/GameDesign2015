using UnityEngine;
using System.Collections;

public class BOOST : MonoBehaviour {
    public float force = 1000;
    ConstantForce f;
    ParticleSystem ps;
	// Use this for initialization
	void Start () {
	    f = this.GetComponent<ConstantForce>();
        ps = this.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
      
        if (Input.GetKey(KeyCode.LeftShift))
        {
            f.relativeForce = new Vector3(0, 0, force);
            ps.startLifetime = 1;
            ps.enableEmission = true;
                
        }
        else if (f.force.z != 0)
        {
            f.force = new Vector3(0, 0, 0);
            ps.startLifetime = 0;
            ps.enableEmission = true;
        }
	}
}
