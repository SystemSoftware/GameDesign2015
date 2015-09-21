using UnityEngine;
using System.Collections;

public class T4FinalEnemyRotation : MonoBehaviour {
    public GameObject ship;
    private GameObject Rex;

	// Use this for initialization
	void Start () {
        Rex = GameObject.Find("World" + (this.gameObject.layer - 28) + "/FinalEnemy/TRexCharlY93");
	}
	
	// Update is called once per frame
	void Update () {
	    if (ship != null){
            Vector3 relativePos = ship.transform.position - Rex.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            // prevent to rotate around the x axis
            Vector3 tmp = rotation.eulerAngles;
            tmp.x = 0;
            rotation = Quaternion.Euler(tmp);
            // rotate
            Rex.transform.rotation = rotation;
        }
	}

    void OnTriggerEnter(Collider other) {
		if (other.tag == "Ship" && other.gameObject.layer == this.gameObject.layer) {
			ship = other.gameObject;
		}
		//ignore Bullets
		else if (other.tag != "Bullet" && other.gameObject.layer == this.gameObject.layer) { 
			Transform parent = other.transform.parent;
			//traverse through parents hierachy to check if Collider is part of a ship
			while (parent!=null) { 
				
				//if parent is a ship and it is its first collision with the trigger
				if (parent.tag == "Ship") {	
					
					ship = parent.gameObject;
					
					//break, as nothing interesting can happen now
					break;
				}else {
					//go one step higher in the hierachy and check again for ship
					parent=parent.parent;
				}
			}
		}


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

        if ((objHasShipTag || objParHasShipTag) && (other.gameObject.layer == Rex.gameObject.layer)) {
            // is a ship
            if (other.GetComponent<Rigidbody>() != null) {
                // trigger object has the rigidbody?
                ship = other.gameObject;
            } else if (other.transform.parent.GetComponent<Rigidbody>() != null) {
                ship = other.transform.parent.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider other) {
    }
}
