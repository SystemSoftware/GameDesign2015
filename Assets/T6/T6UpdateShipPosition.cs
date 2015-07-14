using UnityEngine;
using System.Collections;

public class T6UpdateShipPosition : MonoBehaviour {

	public Transform ship;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = ship.position;
	}
}
