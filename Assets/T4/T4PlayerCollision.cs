using UnityEngine;
using System.Collections;

public class T4PlayerCollision : MonoBehaviour {

	private Renderer rend;
	private Rigidbody rig;
	private bool justHit=false;

	void OnTriggerEnter(Collider other){
		
		//if a target is hit
		if (other.tag == "Rock") {

			if(!justHit){
				justHit=true;
				StartCoroutine(Blinking()); //start blinking
				StartCoroutine(SlowDown()); //slow down
				StartCoroutine(ResetJustHit()); //disable effects of new hits for some time
			}
		}
	}

	//waits 6 seconds before allowing new Hits to interfere with the ship
	//so that the player does not get stuck in the same place by reaccuring hits
	//and has the chance to react
	IEnumerator ResetJustHit(){
		yield return new WaitForSeconds(6f);
		justHit = false;
	}

	//sets the ships velocity to zero and prohibits any forces on it for 3 seconds
	IEnumerator SlowDown(){
		rig=GetComponentInChildren<Rigidbody>();
		yield return new WaitForSeconds(0.5f);
		rig.velocity = Vector3.zero;
		rig.angularVelocity = Vector3.zero;
		rig.isKinematic = true;
		yield return new WaitForSeconds(3f);
		rig.isKinematic = false;
	}

	//disables and reenables all Renderers of the ship 6 times in 6 seconds to make his appearance "blinking"
	IEnumerator Blinking(){
		Renderer[] allRend = GetComponentsInChildren<Renderer>();
		for (int i=0; i<12; i++) {
			foreach( Renderer rend in allRend){
				if(rend!=null) //Bullets are children of the ship and can be destroyed which would result in errors without this test
				rend.enabled = !rend.enabled;
			}
			yield return new WaitForSeconds(0.5f);
		}

		//make sure all Renderers are enabled again
		foreach (Renderer rend in allRend) {
			if(rend!=null)
			rend.enabled = true;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
