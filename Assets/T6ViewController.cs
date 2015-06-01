using UnityEngine;
using System.Collections;

public class T6ViewController : MonoBehaviour {

   Camera mapCamera;
   Camera gameCamera;

	// Use this for initialization
	void Start () {
        Controller ctrl = GetComponent<Controller>();
        mapCamera = GameObject.Find("MapCamera" + ctrl.ctrlControlIndex).GetComponent<Camera>();
        gameCamera = ctrl.ctrlAttachedCamera;
        Debug.Log(gameCamera);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("MAPCAM: " + mapCamera  + "GAME"+gameCamera);
        if (Input.GetKey(KeyCode.K))
        {
            mapCamera.enabled = !mapCamera.enabled;
            gameCamera.enabled = !gameCamera.enabled;
        }
	}

}
