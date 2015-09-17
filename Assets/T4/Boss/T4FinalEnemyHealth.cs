using UnityEngine;
using System.Collections;

public class T4FinalEnemyHealth : MonoBehaviour {
    public int bossHealth = 20;
    private T4Sound3DLogic soundLogic;

	// Use this for initialization
	void Start () {
        soundLogic = GameObject.Find("SoundContainer").GetComponent<T4Sound3DLogic>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void applyDamage(int amount) {
        bossHealth -= amount;
        soundLogic.playBossHit();

        if (bossHealth <= 0) { // boss dead
            // play endscreen, port away, stop sounds
            Debug.Log("BOSS"+(this.gameObject.layer-28)+" DEAD!");
            soundLogic.playEndTheme();
        }
    }
}
