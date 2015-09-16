using UnityEngine;
using System.Collections;

public class T4CommonFunctions : MonoBehaviour {

    public bool isShip(int callerLayer, Collider other) {
        if (other == null || other.gameObject == null || other.gameObject.tag == null) {
            return false;
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

        if ((objHasShipTag || objParHasShipTag) && (other.gameObject.layer == callerLayer)) {
            // is a ship
            return true;
        }
        return false;
    }

    public GameObject getShip(Collider other) {
        // find the rigidbody
        if (other.GetComponent<Rigidbody>() != null) {
            // trigger object has the rigidbody?
            return other.gameObject;
        } else if (other.transform.parent.GetComponent<Rigidbody>() != null) {
            return other.transform.parent.gameObject;
        }
        return null;
    }
}
