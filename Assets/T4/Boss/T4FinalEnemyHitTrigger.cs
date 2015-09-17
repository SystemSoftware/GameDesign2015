using UnityEngine;
using System.Collections;

public class T4FinalEnemyHitTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        Debug.Log("hitscript>"+other.name);
    }

    void OnTriggerExit(Collider other) {
    }
}
