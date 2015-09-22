using UnityEngine;
using System.Collections;

public class T3Laser : MonoBehaviour {
    
    public int timeoffset;
    public float power;

    private ParticleSystem sys;
    private System.DateTime start;
    private System.DateTime end;
    private Vector3 pos;
    private Vector3 dir;
	// Use this for initialization
	void Start () {
        start = System.DateTime.Now;
        sys = this.GetComponent<ParticleSystem>();
        pos = sys.gameObject.transform.position;
        dir = sys.gameObject.transform.forward;
        if (timeoffset == 0) timeoffset = 5;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    end = System.DateTime.Now;
        if (end.Subtract(start).Seconds > timeoffset + (new System.Random().Next(1,10))/10)
        {
            start = System.DateTime.Now;

            
            RaycastHit hitinfo;
            Physics.Raycast(new Ray(pos, dir), out hitinfo, sys.startLifetime*sys.startSpeed);

            Collider col = hitinfo.collider;
            Rigidbody rb;

            sys.Play();
            if (col == null) return;
            rb = col.GetComponent<Rigidbody>();
            if (rb == null && col.gameObject.transform.parent != null) rb = col.gameObject.transform.parent.GetComponent<Rigidbody>();

            if (rb != null && !col.gameObject.name.Contains("Asteroid"))
            {
                try
                {
                    rb.AddExplosionForce(power, pos, sys.startLifetime * sys.startSpeed);
                }
                catch (MissingComponentException e)
                {
                }
            }
        }
	}
}
