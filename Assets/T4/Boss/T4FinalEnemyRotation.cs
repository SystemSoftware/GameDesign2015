using UnityEngine;
using System.Collections;

public class T4FinalEnemyRotation : MonoBehaviour {
    public GameObject ship;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (ship != null){
            Vector3 relativePos = ship.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            // prevent to rotate around the x axis
            Vector3 tmp = rotation.eulerAngles;
            tmp.x = 0;
            rotation = Quaternion.Euler(tmp);
            // rotate
            transform.rotation = rotation;
        }
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Ship") && (other.gameObject.layer == this.gameObject.layer)) { // ship entered > begin shooting
            ship = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other) {
    }
}
