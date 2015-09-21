using UnityEngine;
using System.Collections;

public class T4FinalEnemyShooting : MonoBehaviour {
    public Transform fireball;
    public Vector3 direction;
    public int fireballSpeed = 22000;
    public int aimVelocityInfluence = 150;

    private GameObject ship;
    // delay between shots
    private float nextFireball = 0.0f;
    // 1f = 1sec
    public float fireballCD = 5.0f;
    public SphereCollider sc;
    private GameObject Rex, attackSpawn;
    private T4Sound3DLogic soundLogic;

    // Use this for initialization
    void Start() {
        Rex = GameObject.Find("World" + (this.gameObject.layer - 28) + "/FinalEnemy/TRexCharlY93");
        attackSpawn = GameObject.Find("World" + (this.gameObject.layer - 28) + "/FinalEnemy/TRexCharlY93/AttackSpawn");
        soundLogic = GameObject.Find("SoundContainer").GetComponent<T4Sound3DLogic>();
    }

    // Update is called once per frame
    void Update() {
        if (ship != null) {
            if (Time.time > nextFireball) {
                nextFireball += fireballCD;

                //Debug.Log("Rex shoot fire.");
                // shoot fireball
                Transform tmp = Instantiate(fireball, transform.position, Quaternion.identity) as Transform;
                GameObject spawned_fireball = tmp.gameObject;
                spawned_fireball.layer = Rex.layer;
                foreach (Transform child in spawned_fireball.transform) {
                    child.gameObject.layer = Rex.layer;
                }
                // set position of spawned fireball
                spawned_fireball.transform.position = attackSpawn.transform.position;

                //direction = (ship.transform.position - RexHead.transform.position).normalized;
                direction = ((ship.transform.position + Vector3.Normalize(ship.GetComponent<Rigidbody>().velocity) * aimVelocityInfluence) - attackSpawn.transform.position).normalized;
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
                soundLogic.playBossAttack();
            }
        }
    }

    void OnTriggerEnter(Collider other) {
		if (other.tag == "Ship" && other.gameObject.layer == this.gameObject.layer) {
			ship = other.gameObject;
		}
		//ignore Bullets
		else if (other.tag != "Bullet" && other.gameObject.layer == this.gameObject.layer) { 
			Transform parent = other.transform.parent;
			//traverse through parents hierachy to check if Collider is part of a ship
			while (parent!=null) { 				
				//if parent is a ship and it is its first collision with the trigger
				if (parent.tag == "Ship") {	
					ship = parent.gameObject;				
					//break, as nothing interesting can happen now
					break;
				} else {
					//go one step higher in the hierachy and check again for ship
					parent = parent.parent;
				}
			}
		}
    }

    void OnTriggerExit(Collider other) {
		if (other.tag == "Ship" && other.gameObject.layer == this.gameObject.layer) {
			ship = null;
		}
		//ignore Bullets
		else if (other.tag != "Bullet" && other.gameObject.layer == this.gameObject.layer) { 
			Transform parent = other.transform.parent;
			//traverse through parents hierachy to check if Collider is part of a ship
			while (parent!=null) { 				
				//if parent is a ship and it is its first collision with the trigger
				if (parent.tag == "Ship") {	
					ship = null;				
					//break, as nothing interesting can happen now
					break;
				} else {
					//go one step higher in the hierachy and check again for ship
					parent = parent.parent;
				}
			}
		}
    }
}
