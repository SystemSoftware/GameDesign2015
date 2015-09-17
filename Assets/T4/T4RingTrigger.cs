using UnityEngine;
using System.Collections;

public class T4RingTrigger : MonoBehaviour {

	private bool[] unhandled_ships=new bool[4]{true,true,true,true};

	void OnTriggerEnter(Collider other){
		Transform parent = other.transform.parent;

		//ignore Bullets
		if (other.tag != "Bullet") { 

			//traverse through parents hierachy to check if Collider is part of a ship
			while (parent!=null) { 

				//if parent is a ship and it is its first collision with the trigger
				if (parent.tag == "Ship" && checkHandledShips(parent.gameObject.layer)) {	

					//speed it up
					Rigidbody rigid = parent.GetComponent<Rigidbody> ();
					rigid.AddForce (rigid.transform.forward * 500000);

					//break, as nothing interesting can happen now
					break;
				}else {
					//go one step higher in the hierachy and check again for ship
					parent=parent.parent;
				}
			}
		}
	}

	//check if this is the first Collision of the ship with the Trigger
	//if it is the first collision it returns true and sets the
	// relevant entry in bool[] unhandled_ships to false
	private bool checkHandledShips(int lay){
		bool retVal;
		switch (lay) {
		case(8):
			retVal = unhandled_ships[0];
			if(retVal){
				unhandled_ships[0]=false;
			}
			return retVal;
		case(9):
			retVal = unhandled_ships[1];
			if(retVal){
				unhandled_ships[1]=false;
			}
			return retVal;
		case(10):
			retVal = unhandled_ships[2];
			if(retVal){
				unhandled_ships[2]=false;
			}
			return retVal;
		case(11):
			retVal = unhandled_ships[3];
			if(retVal){
				unhandled_ships[3]=false;
			}
			return retVal;
		default:
			return false;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
