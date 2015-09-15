using UnityEngine;
using System.Collections;

public class T4FinalEnemyShooting : MonoBehaviour {
    private GameObject ship;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Ship") && (other.gameObject.layer == this.gameObject.layer)) { // ship entered > begin shooting
            UnityEngine.Debug.Log("entered TREXRADIUS[shooting]" + other.gameObject);


            ship = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other) {
    }
}
