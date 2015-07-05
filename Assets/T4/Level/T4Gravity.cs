using UnityEngine;
using System.Collections;

public class T4Gravity : MonoBehaviour {
    private Vector3 size = new Vector3(50, 50, 50);
    private float grav_size = 500f;
    private bool ship_init = false;
    private GameObject ship;
    private Rigidbody rb;
    private float distance;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (!ship_init) {
            if (Level.ActiveShips.Length > 0) {
                ship = Level.ActiveShips[0].gameObject;
                rb = ship.GetComponent<Rigidbody>();
                ship_init = true;
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color32(255, 41, 212, 255);
        Gizmos.DrawCube(this.transform.position, size);
        Gizmos.DrawWireSphere(transform.position, grav_size);
    }

    //private Vector3 acceleration = Vector3.zero, lastVelocity = Vector3.zero;
    void FixedUpdate() {
        /*
        acceleration = (rb.velocity - lastVelocity) / Time.fixedDeltaTime;
        lastVelocity = rb.velocity;
         * */
        if (ship_init) {
            // is in gravity area?
            distance = Vector3.Distance(ship.transform.position, transform.position);
            if (distance <= grav_size) {
                
                Vector3 dirdist = (transform.position - ship.transform.position);
                Debug.Log("direction=" + dirdist.ToString());
                Debug.Log("veL=" + rb.velocity.ToString());
                Debug.Log("xdist=" + dirdist.x);

                Vector3 push_f = (transform.position - ship.transform.position).normalized * Mathf.Pow(distance, 2);
                push_f.z = 0;
                //float dx = distance;
                rb.AddForce(push_f);
                /*
                Vector3 pull_force = new Vector3(-dirdist.x*100,
                                                 0,
                                                 0);
                Debug.Log("force=" + pull_force.ToString());
                ship.GetComponent<Rigidbody>().AddForce(pull_force.x, pull_force.y, pull_force.z);
                 */

                /*
                float push_force = (distance/grav_size)*800f;
                Debug.Log("distance="+distance+" pushforce=" + push_force);
                Vector3 forceVec = -rb.velocity.normalized * push_force;
                rb.AddForce(forceVec,ForceMode.Impulse);
                 * */
            }
        }
    }
}