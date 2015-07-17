using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class T4TurretTrigger : MonoBehaviour {
    public Transform bullet;

    private bool should_shoot, delay_active;
    private GameObject ship;
    private Vector3 direction;
    private int bullet_speed;
    private Stopwatch stopwatch;
    private int delay;
    private float accuarcy_spread = 0.02f;

	// Use this for initialization
	void Start () {
        should_shoot = false;
        delay_active = false;
        bullet_speed = 20000;
        delay = 500;

        stopwatch = new Stopwatch();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (should_shoot) {
            // did shoot before?
            if (delay_active){
                // enough time in between passed?
                if (stopwatch.ElapsedMilliseconds > delay) {
                    stopwatch.Reset();
                    delay_active = false;
                }
            }else{
                // shoot
                Transform tmp = Instantiate(bullet, transform.position, Quaternion.identity) as Transform;
                GameObject spawned_bullet = tmp.gameObject;
                spawned_bullet.layer = this.gameObject.layer;
                

                direction = (ship.transform.position - this.transform.position).normalized;
                /*
                // add spread
                direction = new Vector3(direction.x + Random.Range(-accuarcy_spread, accuarcy_spread), 
                                        direction.y + Random.Range(-accuarcy_spread, accuarcy_spread), 
                                        direction.z + Random.Range(-accuarcy_spread, accuarcy_spread));
*/
                spawned_bullet.transform.rotation = Quaternion.LookRotation(direction);
                spawned_bullet.GetComponent<Rigidbody>().AddForce(direction * bullet_speed);

                stopwatch.Start();
                delay_active = true;
            }
        }
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Ship") && (other.gameObject.layer == this.gameObject.layer)) { // ship entered > begin shooting
            UnityEngine.Debug.Log("entered TURRETRADIUS" + other.gameObject);
            
            ship = other.gameObject;
            direction = (ship.transform.position - this.transform.position).normalized;
            should_shoot = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Ship") && (other.gameObject.layer == this.gameObject.layer)) { // ship left > stop shooting
            should_shoot = false;
            UnityEngine.Debug.Log("leave TURRETRADIUS" + other.gameObject);
            
            ship = null;
        }
    }
}
