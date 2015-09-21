using UnityEngine;
using System.Collections;

public class T4FinalEnemyHealth : MonoBehaviour {
    public int bossHealth = 20;
    private T4Sound3DLogic soundLogic;
    private T4GUICamEndHandler camEnd;
    private T4Logic logic;
	// Use this for initialization
	void Start () {
        soundLogic = GameObject.Find("SoundContainer").GetComponent<T4Sound3DLogic>();
        logic = GameObject.Find("Logic").GetComponent<T4Logic>();
	}

    public void applyDamage(int amount) {
        bossHealth -= amount;
        soundLogic.playBossHit();

        if (bossHealth <= 0) { // boss dead
            bossHealth = 20;
            // play endscreen, port away, stop sounds
            Debug.Log("BOSS"+(this.gameObject.layer-28)+" DEAD!");
            //camEnd.playEnd();
            logic.playerFinished(this.gameObject.layer);
        }
    }
}
