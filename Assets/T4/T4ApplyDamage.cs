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

    // Use this for initialization
    void Start(){
		GameObject soundContainer = GameObject.Find ("SoundContainer");
		soundLogic=soundContainer.GetComponent<T4Sound3DLogic>();

        layer = gameObject.layer;
    }

    void OnTriggerEnter(Collider other){
        // did the object hit a ship on the same layer?
        if (other == null || other.gameObject == null || other.gameObject.tag == null) {
            return;
        }

        // has the object the ship tag?
        bool objHasShipTag = false;
        if (other.gameObject.tag.Equals("Ship")) {
            objHasShipTag = true;
        }

        // has parent object the ship tag?
        bool objParHasShipTag = false;
        if (other.transform.parent != null && other.transform.parent.gameObject.tag != null && !other.gameObject.tag.Equals("Bullet") && other.transform.parent.gameObject.tag.Equals("Ship")) {
            objParHasShipTag = true;
        }

        if ((objHasShipTag || objParHasShipTag) && (other.gameObject.layer == this.layer)) {
            // find the rigidbody
            if (other.GetComponent<Rigidbody>() != null) {
                // trigger object has the rigidbody?
                ship = other.gameObject;
            } else if (other.transform.parent.GetComponent<Rigidbody>() != null) {
                ship = other.transform.parent.gameObject;
            }

            // is a ship
            if (ship != null) {
                healthbar = ship.GetComponent<T4GUIHealthbarHandler>();
                score = ship.GetComponent<T4GUIScoreHandler>();
                // apply damage
                shipRoutine = ship.GetComponent<T4ZeroHealthHandler>();
                if (healthbar.getHealth() - damage <= 0) {
                    if (shipRoutine.startHealthRoutines()) {
                        healthbar.setHealth(100);
                        score.subScore(5);
                        soundLogic.playExplosionBullet();
                        Instantiate(explosion, transform.position, transform.rotation);
                    }
                } else {
                    if (!shipRoutine.justHit) {
                        healthbar.setHealth(healthbar.getHealth() - damage);
                        soundLogic.playExplosionBullet();
                        Instantiate(explosion, transform.position, transform.rotation);
                    }
                }

                Destroy(gameObject);
            }
        }
    }
}
