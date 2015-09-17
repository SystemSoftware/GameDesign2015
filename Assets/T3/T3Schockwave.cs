using UnityEngine;
using System.Collections;

public class T3Schockwave : MonoBehaviour {


    public int timeoffset;
    public float r;
    public float power;
    private Vector3 pos;
    public bool randomized;

    private System.DateTime start;
    private System.DateTime end;
    // Use this for initialization
    void Start()
    {
        pos = transform.position;
        start = System.DateTime.Now;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        end = System.DateTime.Now;
        if (end.Subtract(start).Seconds > 1) this.GetComponent<ParticleSystem>().enableEmission = false;
        if (end.Subtract(start).Seconds > timeoffset)
        {
            int mod = (randomized) ? new System.Random().Next(0, 2) : 1;
            Collider[] colliders = Physics.OverlapSphere(pos, r);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb;

                rb = hit.GetComponent<Rigidbody>();
                if (rb == null && hit.gameObject.transform.parent != null) rb = hit.gameObject.transform.parent.GetComponent<Rigidbody>();

                if (rb != null && !hit.gameObject.name.Contains("Asteroid"))
                {
                    try
                    {
                        if (mod != 0)
                        {
                            rb.AddExplosionForce(power, pos, r, 0, ForceMode.Impulse);
                            this.GetComponent<ParticleSystem>().enableEmission = true;
                        }
                    }
                    catch (MissingComponentException e)
                    {
                        continue;
                    }
                }
            }
            start = System.DateTime.Now;
        }
    }
}
