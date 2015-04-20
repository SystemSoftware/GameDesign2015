using UnityEngine;
using System.Collections;

public class T1LevelLogic_Water : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Level.drag = 1f;
        Level.angularDrag = 2f;
        Level.overrideDriveColor = true;
        Level.driveColor = new Color(0.6f, 0.9f, 1f);
        Level.allowMotion = true;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
