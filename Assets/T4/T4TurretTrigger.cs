using UnityEngine;
using System.Collections;

public class T4TurretTrigger : MonoBehaviour {
    private bool should_shoot;
    private GameObject ship;
    private Vector3 direction;
    public Transform bullet;

	// Use this for initialization
	void Start () {
        should_shoot = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (should_shoot) {
            Transform tmp = Instantiate(bullet, transform.position, Quaternion.identity) as Transform;
            GameObject spawned_bullet = tmp.gameObject;

            direction = (ship.transform.position - this.transform.position).normalized;
            spawned_bullet.transform.rotation = Quaternion.LookRotation(direction);
            spawned_bullet.GetComponent<Rigidbody>().AddForce(direction*8000);
        }
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Ship") && (other.gameObject.layer == this.gameObject.layer)) { // ship entered > begin shooting
            Debug.Log("entered TURRETRADIUS" + other.gameObject);
            
            ship = other.gameObject;
            direction = (ship.transform.position - this.transform.position).normalized;
            should_shoot = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Ship") && (other.gameObject.layer == this.gameObject.layer)) { // ship left > stop shooting
            should_shoot = false;
            Debug.Log("leave TURRETRADIUS" + other.gameObject);
            
            ship = null;
        }
    }
}
