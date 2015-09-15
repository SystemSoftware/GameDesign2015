using UnityEngine;
using System.Collections;

public class T4FinalEnemyShooting : MonoBehaviour {
    public Transform fireball;
    private GameObject ship;
    private Vector3 direction;
    private int fireball_speed = 2000;

    // delay between shots
    private float nextFireball = 0.0f;
    // 1f = 1sec
    private float fireballCD = 5.0f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (ship != null) {
            if (Time.time > nextFireball) {
                nextFireball += fireballCD;

                Debug.Log("Rex shoot fire.");
                // shoot fireball
                Transform tmp = Instantiate(fireball, transform.position, Quaternion.identity) as Transform;
                GameObject spawned_fireball = tmp.gameObject;
                spawned_fireball.layer = this.gameObject.layer;


                direction = (ship.transform.position - this.transform.position).normalized;
                /*
                // add spread
                direction = new Vector3(direction.x + Random.Range(-accuarcy_spread, accuarcy_spread), 
                                        direction.y + Random.Range(-accuarcy_spread, accuarcy_spread), 
                                        direction.z + Random.Range(-accuarcy_spread, accuarcy_spread));
*/
                spawned_fireball.transform.rotation = Quaternion.LookRotation(direction);
                spawned_fireball.GetComponent<Rigidbody>().AddForce(direction * fireball_speed);
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Ship") && (other.gameObject.layer == this.gameObject.layer)) { // ship entered > begin shooting
            UnityEngine.Debug.Log("entered TREXRADIUS[shooting]" + other.gameObject);

            ship = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Ship") && (other.gameObject.layer == this.gameObject.layer)) { // ship left > stop shooting
            UnityEngine.Debug.Log("left TREXRADIUS[shooting]" + other.gameObject);
            ship = null;
        }
    }
}
