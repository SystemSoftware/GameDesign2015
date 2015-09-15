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
    // Use this for initialization
    void Start(){
		GameObject soundContainer = GameObject.Find ("SoundContainer");
		soundLogic=soundContainer.GetComponent<T4Sound3DLogic>();
    }

    void OnTriggerEnter(Collider other){
        // did the object hit a ship on the same layer?
        if (other.gameObject.tag.Equals("Ship") && (other.gameObject.layer == this.gameObject.layer)){
            ship = other.gameObject;
            healthbar = ship.GetComponent<T4GUIHealthbarHandler>();
			score = ship.GetComponent<T4GUIScoreHandler>();
            // apply damage
			shipRoutine = ship.GetComponent<T4ZeroHealthHandler>();
			if (healthbar.getHealth() - damage <= 0)
            {
				if(shipRoutine.startHealthRoutines()){
					healthbar.setHealth(100);
					score.subScore(5);
					soundLogic.playExplosionBullet();
					Instantiate(explosion, transform.position, transform.rotation);
				}
            }else{
				if(!shipRoutine.justHit){
					healthbar.setHealth(healthbar.getHealth()-damage);
					soundLogic.playExplosionBullet();
					Instantiate(explosion, transform.position, transform.rotation);
				}
			}

			Destroy(gameObject);
        }
    }
}
