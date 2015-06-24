using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class T6RayTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float pitchValue = -1;
        // this.transform.Rotate(0, pitchValue *0.25f, 0);
        Vector3 direction = new Vector3(0, -Mathf.Sin(pitchValue / 2), -Mathf.Cos(pitchValue / 2));
        Debug.DrawRay(this.transform.position, direction * 100, Color.red);
        this.transform.eulerAngles = direction;
	}
}
