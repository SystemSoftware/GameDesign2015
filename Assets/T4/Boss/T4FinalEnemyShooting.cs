using UnityEngine;
using System.Collections;

public class T4FinalEnemyShooting : MonoBehaviour {
    public Transform fireball;
    public Vector3 direction;
    public int fireballSpeed = 20000;
    public int aimVelocityInfluence = 200;

    private GameObject ship;
    // delay between shots
    private float nextFireball = 0.0f;
    // 1f = 1sec
    public float fireballCD = 5.0f;

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
                direction = ((ship.transform.position+Vector3.Normalize(ship.GetComponent<Rigidbody>().velocity)*aimVelocityInfluence) - this.transform.position).normalized;
                /*
                // add spread
                direction = new Vector3(direction.x + Random.Range(-accuarcy_spread, accuarcy_spread), 
                                        direction.y + Random.Range(-accuarcy_spread, accuarcy_spread), 
                                        direction.z + Random.Range(-accuarcy_spread, accuarcy_spread));
*/
                // let the fireball face the direction of the target
                spawned_fireball.transform.rotation = Quaternion.LookRotation(direction);
                // turn by 180 degrees
                Vector3 tmp2 = spawned_fireball.transform.rotation.eulerAngles;
                tmp2.y = tmp2.y+ 180;
                spawned_fireball.transform.rotation = Quaternion.Euler(tmp2);
                // finally shoot into the direction
                spawned_fireball.GetComponent<Rigidbody>().AddForce(direction * fireballSpeed);
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
