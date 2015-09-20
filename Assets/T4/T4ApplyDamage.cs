using UnityEngine;
using System.Collections;

public class T4ApplyDamage : MonoBehaviour{
    private T4GUIHealthbarHandler healthbar;
	private T4GUIScoreHandler score;
	private T4ZeroHealthHandler shipRoutine;
	private T4Sound3DLogic soundLogic;
    private int ControlID;
    private GameObject ship;
    public int damage = 5;
	private bool justHit =false;
	public ParticleSystem explosion;
    private int layer;
	bool alrdyHit;

    // Use this for initialization
    void Start(){
		GameObject soundContainer = GameObject.Find ("SoundContainer");
		soundLogic=soundContainer.GetComponent<T4Sound3DLogic>();
		alrdyHit = false;
        layer = gameObject.layer;
    }

    void OnTriggerEnter(Collider other){
		//sometimes a bullet seems to hit multiple colliders of ship delusion and too much damage is taken.
		//ugly multiple tests if alrdyhit  should stop this behaviour
		if(!alrdyHit){
			if (other.tag == "Ship" && other.gameObject.layer == this.gameObject.layer && !alrdyHit) {
				alrdyHit=true;
				ship = other.gameObject;
				healthbar = ship.GetComponent<T4GUIHealthbarHandler> ();
				score = ship.GetComponent<T4GUIScoreHandler> ();
				// apply damage
				shipRoutine = ship.GetComponent<T4ZeroHealthHandler> ();
				if (healthbar.getHealth () - damage <= 0) {
					if (shipRoutine.startHealthRoutines ()) {
						healthbar.setHealth (100);
						score.subScore (5);
						soundLogic.playExplosionBullet ();
						Instantiate (explosion, transform.position, transform.rotation);
					}
				} else {
					if (!shipRoutine.justHit) {
						healthbar.setHealth (healthbar.getHealth () - damage);
						soundLogic.playExplosionBullet ();
						Instantiate (explosion, transform.position, transform.rotation);
					}
				}
			
				Destroy (gameObject);
			}
		//ignore Bullets
			else if (other.tag != "Bullet" && other.gameObject.layer == this.gameObject.layer && !alrdyHit) { 
				Transform parent = other.transform.parent;
				//traverse through parents hierachy to check if Collider is part of a ship
				while (parent!=null) { 				
					//if parent is a ship and it is its first collision with the trigger
					if (parent.tag == "Ship" && !alrdyHit) {	
						alrdyHit=true;
						ship = parent.gameObject;
					
						healthbar = ship.GetComponent<T4GUIHealthbarHandler> ();
						score = ship.GetComponent<T4GUIScoreHandler> ();
						// apply damage
						shipRoutine = ship.GetComponent<T4ZeroHealthHandler> ();
						if (healthbar.getHealth () - damage <= 0) {
							if (shipRoutine.startHealthRoutines ()) {
								healthbar.setHealth (100);
								score.subScore (5);
								soundLogic.playExplosionBullet ();
								Instantiate (explosion, transform.position, transform.rotation);
							}
						} else {
							if (!shipRoutine.justHit) {
								healthbar.setHealth (healthbar.getHealth () - damage);
								soundLogic.playExplosionBullet ();
								Instantiate (explosion, transform.position, transform.rotation);
							}
						}
					
						Destroy (gameObject);			
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
}
