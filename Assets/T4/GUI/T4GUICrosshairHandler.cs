using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class T4GUICrosshairHandler : MonoBehaviour {
    private int ship_i = 0;
    private GameObject inner_crosshair = null;
    private GameObject outer_crosshair = null;
    private Camera c;
    private float cam_width, cam_height, mx, my, fx, fy, offx, offy;
    private Controller ctrl;
    private bool maximized = false;

	// Use this for initialization
	void Start () {
        ctrl  = this.GetComponent<Controller>();
        inner_crosshair = GameObject.Find("Crosshair"+ctrl.ctrlControlIndex).transform.Find("Inner").gameObject;
        outer_crosshair = GameObject.Find("Crosshair"+ctrl.ctrlControlIndex).transform.Find("Outer").gameObject;


        c = ctrl.ctrlAttachedCamera;
        cam_width = Screen.width / 2;
        cam_height = Screen.height / 2;

        // set the middle of the crosshair
        switch(ctrl.ctrlControlIndex){
            case 0: // player 1
                mx = cam_width/2;
                my = Screen.height - cam_height/2;
                break;
            case 1: // player 2
                mx = Screen.width - cam_width / 2;
                my = Screen.height - cam_height / 2;
                break;
            case 2: // player 3
                mx = cam_width / 2;
                my = cam_height / 2;
                break;
            case 3: // player 4
                mx = Screen.width - cam_width / 2;
                my = cam_height / 2;
                break;
        }
        // values for maximized
        fx = Screen.width / 2;
        fy = Screen.height / 2;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Return)) {
            maximized = !maximized;
        }


        offx = transform.forward.x;
        offy = transform.forward.y;
        if (!maximized) {
            // just one cam
            outer_crosshair.transform.position = new Vector3(mx + offx * 10, my + offy * 10, 0);
            inner_crosshair.transform.position = new Vector3(mx + offx * 20, my + offy * 20, 0);
        } else { 
            // more cams
            if (ctrl.ctrlControlIndex != 0) { // not player 1? -> hide crosshair
                outer_crosshair.transform.position = new Vector3(-200, -200, 0);
                inner_crosshair.transform.position = new Vector3(-200, -200, 0);
            } else {
                // maximzed
                outer_crosshair.transform.position = new Vector3(fx + offx * 10, fy + offy * 10, 0);
                inner_crosshair.transform.position = new Vector3(fx + offx * 20, fy + offy * 20, 0);
            }
        }
	}
}
