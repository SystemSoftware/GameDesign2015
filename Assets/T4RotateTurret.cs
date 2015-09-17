using UnityEngine;
using System.Collections;

public class T4RotateTurret : MonoBehaviour {
	public bool face_target=false;
	public GameObject ship;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (face_target) {
			if (ship != null){
				transform.LookAt (ship.transform);
			}
		}
	}
}
