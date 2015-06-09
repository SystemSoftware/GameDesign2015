using UnityEngine;
using System.Collections;

public class T6TestRotateMapCamera : MonoBehaviour {

	float degree;
	// Use this for initialization
	void Start () {
		degree = 0;
	}
	
	// Update is called once per frame
	void Update () {
		degree++;
		//move along a xy circle around 000
		float alpha = Mathf.Deg2Rad * degree;
		float x = Mathf.Cos (alpha) * 100000;
		float y = Mathf.Sin(alpha) * 100000;
		this.transform.position = new Vector3 (x,y,0);
		this.transform.LookAt (new Vector3 (0, 0, 0));

	}
}
