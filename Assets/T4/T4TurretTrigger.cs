using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class T4TurretTrigger : MonoBehaviour {
    public int aimVelocityInfluence = 80;

    private Transform bullet;

    private bool should_shoot, delay_active;
    private GameObject ship;
    private Vector3 direction;
    private int bullet_speed;
    private Stopwatch stopwatch;
    private int delay;
    private float accuarcy_spread = 0.02f;

	private T4RotateTurret rotate;
	private T4Sound3DLogic soundLogic;
    private T4CommonFunctions cf;


	// Use this for initialization
	void Start () {
        cf = GameObject.Find("CommonFunctions").GetComponent<T4CommonFunctions>();

		GameObject soundContainer = GameObject.Find ("SoundContainer");
		soundLogic=soundContainer.GetComponent<T4Sound3DLogic>();

        should_shoot = false;
        delay_active = false;
        bullet_speed = 15000;
        delay = 500;

        stopwatch = new Stopwatch();
        bullet = (Resources.Load("TurretBullet") as GameObject).transform;

		rotate = transform.parent.GetComponent<T4RotateTurret>();
	}


	// Update is called once per frame
	void FixedUpdate () {

		//rotate Turret to face the players ship

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
				soundLogic.playTurretShoot(transform.position, ship.transform.position);

                Transform tmp = Instantiate(bullet, transform.position, Quaternion.identity) as Transform;
                GameObject spawned_bullet = tmp.gameObject;
                spawned_bullet.layer = this.gameObject.layer;
                foreach (Transform child in spawned_bullet.transform) {
                    child.gameObject.layer = this.gameObject.layer;
                }

                direction = ((ship.transform.position + Vector3.Normalize(ship.GetComponent<Rigidbody>().velocity) * aimVelocityInfluence) - this.transform.position).normalized;
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

		if (other.tag == "Ship" && other.gameObject.layer == this.gameObject.layer) {
			ship=other.gameObject;
			direction = (ship.transform.position - this.transform.position).normalized;
			should_shoot = true;
			rotate.face_target = true;
			rotate.ship = ship;
		}
		//ignore Bullets
		else if (other.tag != "Bullet" && other.gameObject.layer == this.gameObject.layer) { 
			Transform parent = other.transform.parent;
			//traverse through parents hierachy to check if Collider is part of a ship
			while (parent!=null) { 
				
				//if parent is a ship and it is its first collision with the trigger
				if (parent.tag == "Ship") {	
					
					ship=parent.gameObject;
					direction = (ship.transform.position - this.transform.position).normalized;
					should_shoot = true;
					rotate.face_target = true;
					rotate.ship = ship;
					
					//break, as nothing interesting can happen now
					break;
				}else {
					//go one step higher in the hierachy and check again for ship
					parent=parent.parent;
				}
			}
		}
    }

    void OnTriggerExit(Collider other) {
		if (other.tag == "Ship" && other.gameObject.layer == this.gameObject.layer) {
			should_shoot = false;
			rotate.face_target=false;
			UnityEngine.Debug.Log("leave TURRETRADIUS" + other.gameObject);
			
			ship = null;
		}
		//ignore Bullets
		else if (other.tag != "Bullet" && other.gameObject.layer == this.gameObject.layer) { 
			Transform parent = other.transform.parent;
			//traverse through parents hierachy to check if Collider is part of a ship
			while (parent!=null) { 
				
				//if parent is a ship and it is its first collision with the trigger
				if (parent.tag == "Ship") {						
					should_shoot = false;
					rotate.face_target=false;
					UnityEngine.Debug.Log("leave TURRETRADIUS" + other.gameObject);
					
					ship = null;
					
					//break, as nothing interesting can happen now
					break;
				}else {
					//go one step higher in the hierachy and check again for ship
					parent=parent.parent;
				}
			}
		}

    }
}
