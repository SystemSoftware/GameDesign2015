using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class T4EnemyAI : MonoBehaviour {
    private Transform bullet;

    private bool launched = false;
    private GameObject ship, target_ship;
    private T4EnPath path;
    private int path_i = 0;
    private Vector3[] path_points;
    private Vector3 velocity = Vector3.zero;
    private float lerp_pos = 0.0f;

    // vars for shooting
    private GameObject A, B;
    private Vector3 direction;
    private float bullet_speed;
    private bool should_shoot, delay_active;
    private int delay;
    private Stopwatch stopwatch;

	private T4Sound3DLogic soundLogic;
    public int aimVelocityInfluence = 20;

    void Start() {
		GameObject soundContainer = GameObject.Find ("SoundContainer");
		soundLogic=soundContainer.GetComponent<T4Sound3DLogic>();

        // get A and B
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).name.Equals("A")) {
                A = transform.GetChild(i).gameObject;
            }
            if (transform.GetChild(i).name.Equals("B")) {
                B = transform.GetChild(i).gameObject;
            }
        }
        bullet_speed = 25000;
        should_shoot = false;
        delay_active = false;
        delay = 250;

        stopwatch = new Stopwatch();
        bullet = (Resources.Load("TurretBullet") as GameObject).transform;
    }

    // Update is called once per frame
    void Update () {
        if (launched) {

            // shooting
            if (should_shoot) {
                // did shoot before?
                if (delay_active) {
                    // enough time in between passed?
                    if (stopwatch.ElapsedMilliseconds > delay) {
                        stopwatch.Reset();
                        delay_active = false;
                    }
                } else {
                    // easier by moving the collider
                    /*
                    if((A == null) && (B == null)){ return; }
                    // if orientation -1, the enemy is behind the ship > DONT SHOOT
                    if (orientation(new Vector2(A.transform.position.x, A.transform.position.z),
                                   new Vector2(B.transform.position.x, B.transform.position.z),
                                   new Vector2(target_ship.transform.position.x, target_ship.transform.position.z)) >= 0) {

                        
                    }
                     * */
                    Transform tmp = Instantiate(bullet, transform.position, Quaternion.identity) as Transform;
                    GameObject spawned_bullet = tmp.gameObject;
                    spawned_bullet.layer = this.gameObject.layer;
                    direction = (target_ship.transform.position - this.transform.position).normalized;

                    spawned_bullet.transform.rotation = Quaternion.LookRotation(direction);
                    spawned_bullet.GetComponent<Rigidbody>().AddForce(direction * bullet_speed);

                    soundLogic.playEnemyPlaneShoot(transform.position, target_ship.transform.position);

                    stopwatch.Start();
                    delay_active = true;
                }
            }

            // travel on path
            Vector3 prev = path_points[path_i];
            Vector3 pos  = path_points[path_i + 1];

            this.transform.position = Vector3.Lerp(prev, pos, lerp_pos);
            
            lerp_pos += (Time.deltaTime * 8f);
            if ((lerp_pos >= 1) && (path_i + 1 < path_points.Length - 1)) {
                lerp_pos = 0;
                path_i++;
            } else if (path_i + 1 >= path_points.Length - 1) {
                // end of path reached
                deactivate();
            }

            // adjust the lookat direction of the ship
            Quaternion targetRotation = Quaternion.LookRotation(transform.position - pos);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }
	}

    public void launch() {
        if (!launched) {
            path = transform.parent.GetChild(1).gameObject.GetComponent<T4EnPath>();

            path.collectPathObjects();
            path_points = path.getPathPoints();
            path_i = 1;
            this.transform.position = path_points[path_i];
            this.gameObject.SetActive(true);

            launched = true;
        }
    }

    private void deactivate() {
        // hide/deactivate the ship after reaching the end of the path
        launched = false;
        this.gameObject.SetActive(false);
    }

    private float orientation(Vector2 p, Vector2 q, Vector2 k) {
        float result = ((q.x-p.x)*(k.y-p.y) - (k.x-p.x)*(q.y-p.y));

        if (result > 0) {
            return 1;
        }
        if (result < 0) {
            return -1;
        }
        return 0;
    }

    // shooting radius trigger
    void OnTriggerEnter(Collider other) {
		if (other.tag == "Ship" && other.gameObject.layer == this.gameObject.layer) {
			target_ship = other.gameObject;
			direction = (target_ship.transform.position - this.transform.position).normalized;
			should_shoot = true;
		}
		//ignore Bullets
		else if (other.tag != "Bullet" && other.gameObject.layer == this.gameObject.layer) { 
			Transform parent = other.transform.parent;
			//traverse through parents hierachy to check if Collider is part of a ship
			while (parent!=null) { 
				
				//if parent is a ship and it is its first collision with the trigger
				if (parent.tag == "Ship") {	
					
					target_ship = parent.gameObject;
					direction = (target_ship.transform.position - this.transform.position).normalized;
					should_shoot = true;
					
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
			target_ship = null;
		}
		//ignore Bullets
		else if (other.tag != "Bullet" && other.gameObject.layer == this.gameObject.layer) { 
			Transform parent = other.transform.parent;
			//traverse through parents hierachy to check if Collider is part of a ship
			while (parent!=null) { 				
				//if parent is a ship and it is its first collision with the trigger
				if (parent.tag == "Ship") {	
					should_shoot = false;			
					target_ship = null;					
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
