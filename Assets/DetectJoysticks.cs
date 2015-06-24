using UnityEngine;
using System.Collections;

public class DetectJoysticks : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var joysticks = Input.GetJoystickNames ();
		foreach (var joy in joysticks)
			Debug.Log (joy);
		if (joysticks.Length == 0)
			Debug.Log ( "no joysticks" );

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
