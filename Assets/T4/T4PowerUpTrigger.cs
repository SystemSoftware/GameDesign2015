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
		// did the object hit a ship on the same layer?
		if (other.gameObject.tag.Equals("Ship") && (other.gameObject.layer == this.gameObject.layer)){
			ship = other.gameObject;
			score = ship.GetComponent<T4GUIScoreHandler>();
			// play PowerUp sound
			soundLogic.playPowerUp();
			// apply score
			score.addScore(scoreGain);
			Destroy(this.gameObject);
		}
	}
}
