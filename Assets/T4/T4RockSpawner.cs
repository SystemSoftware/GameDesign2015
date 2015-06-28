using UnityEngine;
using System.Collections;

public class T4RockSpawner : MonoBehaviour {
	public Transform spawners; //destination
	private Transform[] spawnsArray;
	public GameObject rock;
	public float spawnIntervall = 0.5f;
	private bool spawn=false;
	private float nextSpawn;

	void OnTriggerEnter(Collider other){
		Transform parent = other.transform.parent;
		//if parent of collider is a ship but isn't a bullet (bullets have a ship as parent to attribute kills to the right player)
		if (!spawn && other.tag != "Bullet") { 
			
			//traverse through parents hierachy to check if Collider is part of a ship
			while (parent!=null) { 
				
				//if parent is a ship start spawning Rocks
				if (parent.tag == "Ship") {
					spawn = true;
					StartCoroutine (SpawnRocksDuration ());
					StartCoroutine (SpawnRocks ());
					break;
				}else {
				//go one step higher in the hierachy and check again for ship
				parent=parent.parent;
			}
			}
		}
	}

	IEnumerator SpawnRocks(){
		while(spawn){
			foreach (Transform spawner in spawnsArray) {
				GameObject rockClone = Instantiate (rock, spawner.position, spawner.rotation) as GameObject;
				Destroy (rockClone, 10);
				rockClone.GetComponent<Rigidbody> ().velocity = transform.TransformDirection (-Vector3.forward * 50);
			}
			yield return new WaitForSeconds (2.5f);
		}
	}

	IEnumerator SpawnRocksDuration(){
		yield return new WaitForSeconds (20f);
		spawn = false;
	}

	// Use this for initialization
	void Start () {
		Vector3 posi = spawners.transform.position;
		int numberOfSpawns = 10;
		spawnsArray = new Transform[numberOfSpawns];
		for (int i=0; i<numberOfSpawns; i++) {
			spawnsArray[i] = Instantiate (spawners, spawners.transform.position+new Vector3(i*50,0,0), spawners.transform.rotation) as Transform;
			Debug.Log(spawnsArray[0].ToString());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
