using UnityEngine;
using System.Collections;

public class T4EnemyShipHitTrigger : MonoBehaviour {
	GameObject ship;
	T4GUIScoreHandler score;
	int scoreGain = 50;
	public ParticleSystem explosion;
	T4Sound3DLogic soundLogic;
	
	// Use this for initialization
	void Start () {
		GameObject soundContainer = GameObject.Find ("SoundContainer");
		soundLogic=soundContainer.GetComponent<T4Sound3DLogic>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		
		//Check if collider is a Bullet of the same Layer as the Turret
		if (other.gameObject.tag.Equals("Bullet") && (other.gameObject.layer == this.gameObject.layer)) {
			ship = other.transform.parent.gameObject;
			score = ship.GetComponent<T4GUIScoreHandler>();
			// play Explosion Sound?
			soundLogic.playExplosion();
			// apply score
			score.addScore(scoreGain);
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy (other.gameObject);
			Destroy(transform.parent.gameObject);
		}
	}
}
