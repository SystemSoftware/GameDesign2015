using UnityEngine;
using System.Collections;

public class T4FinalEnemyHitTrigger : MonoBehaviour {
    private GameObject Rex;
    private T4FinalEnemyHealth hp;

	// Use this for initialization
	void Start () {
        Rex = GameObject.Find("World" + (this.gameObject.layer - 28) + "/FinalEnemy/TRexCharlY93");
        hp = Rex.GetComponent<T4FinalEnemyHealth>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if(other.name.StartsWith("LaserBullet") && other.gameObject.layer == this.gameObject.layer){
            // hit the boss -> lower his hp
            hp.applyDamage(2); // also plays the sound

            // delete the bullet
            DestroyObject(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other) {
    }
}
