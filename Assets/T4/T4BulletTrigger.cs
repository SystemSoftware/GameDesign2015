using UnityEngine;
using System.Collections;

/*
 * T4BulletTrigger  governs the behaviour of the bullet shot by the player
 * a bullet destroys itself after 5 seconds
 * if a bullet hits a object tagged with "Target" it will
 * destroy the target
 * speed up the player who shot the bullet 
 */
public class T4BulletTrigger : MonoBehaviour {

	private bool destro=false;
	private GameObject t;

	void OnTriggerEnter(Collider other){

		//if a target is hit
		if (other.tag == "Target") {

			//save reference of the target, so it can be deleted elsewhere. Deletions inside the trigger causes errors
			t = other.gameObject;

			//speedup the parent (the player) of the bullet
			SpeedUp(transform.parent);

			//set destro to true to indicate the destruction of the target and bullet
			destro=true;
		}
	}

	//speeds up the player
	void SpeedUp(Transform t){
		Rigidbody rigid = t.gameObject.GetComponent<Rigidbody>();
		rigid.AddForce (rigid.transform.forward * 500000);
	}

	// Use this for initialization
	void Start () {
		//bullets have a lifetime of 5 seconds
		Destroy (gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {

		//target was hit, destroy bullet and target
		if (destro == true) {
			Destroy (t);
			Destroy (gameObject);
		}
	}
}
