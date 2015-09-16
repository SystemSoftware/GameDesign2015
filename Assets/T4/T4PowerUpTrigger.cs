using UnityEngine;
using System.Collections;

public class T4PowerUpTrigger : MonoBehaviour {
	private T4GUIScoreHandler score;
	private int ControlID;
	private GameObject ship;
	public int scoreGain = 5;
	T4Sound3DLogic soundLogic;
	
	// Use this for initialization
	void Start(){
		GameObject soundContainer = GameObject.Find ("SoundContainer");
		soundLogic=soundContainer.GetComponent<T4Sound3DLogic>();
	}
	
	void OnTriggerEnter(Collider other){
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
        // did the object hit a ship on the same layer?
        if ((objHasShipTag || objParHasShipTag) && (other.gameObject.layer == this.gameObject.layer)) {
            // is a ship
            if (other.GetComponent<Rigidbody>() != null) {
                // trigger object has the rigidbody?
                ship = other.gameObject;
            } else if (other.transform.parent.GetComponent<Rigidbody>() != null) {
                ship = other.transform.parent.gameObject;
            }

            if (ship != null) {
                score = ship.GetComponent<T4GUIScoreHandler>();
                // play PowerUp sound
                soundLogic.playPowerUp();
                // apply score
                score.addScore(scoreGain);
                Destroy(this.gameObject);
            }
		}
	}
}
