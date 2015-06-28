using UnityEngine;
using System.Collections;

public class T4Logic : MonoBehaviour {
    public Camera cam;
    public GameObject path;
    bool once = false;
    void OnGUI() {
        if (!Level.AllowMotion) {
            // no motion
            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2, 250, 40), "Start")) {
                foreach (var ship in Level.ActiveShips) {
                    //ship.transform.position = path.transform.position;
                    ship.transform.position = new Vector3(0, 141.7f, -2029);
                    ship.ctrlAttachedCamera.farClipPlane = 6000f;
					//ship.gameObject.AddComponent<T4HelloWorldYeller>();
                    ship.gameObject.AddComponent<T4GUICrosshairHandler>();
                }
                var ship_objects = GameObject.FindGameObjectsWithTag("Ship"); //get all ship-objects
                foreach (var ship_object in ship_objects) { //add the following 2 Scripts to each of them
                    ship_object.AddComponent<T4ShootBullet>(); //lets the ship shoot
                    ship_object.AddComponent<T4PlayerCollision>(); //lets them behave acordingly when colliding with special objects
                }
				// destroy level preview camera
                DestroyImmediate(cam.gameObject);
                Level.EnableMotion(true);
            }
        } else {
            // motion allowed
            if (!once) {
                // delete all the tiny cams for each player
                foreach (var ship in Level.ActiveShips) {
                    Transform min_cam = ship.transform.Find("Camera");
                    if (min_cam != null) {
                        DestroyImmediate(min_cam.gameObject);
                    }
                }
                once = true;
            }

        }
    }
	// Use this for initialization
	void Start () {
        // add render settings
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(255, 255, 152, 255); // rgb 0-255 NOT 0-1
        RenderSettings.ambientIntensity = 0.0028f;
        // set level settings
        //Level.drag = 0.3f;
        Level.drag = 1;
        Level.angularDrag = 0.4f;
        Level.InitializationDone();

        // set gui at start pos
        GameObject.Find("Crosshair0").transform.Find("Inner").gameObject.transform.position = new Vector3(-200, -200, 0);
        GameObject.Find("Crosshair0").transform.Find("Outer").gameObject.transform.position = new Vector3(-200, -200, 0);
        GameObject.Find("Crosshair1").transform.Find("Inner").gameObject.transform.position = new Vector3(-200, -200, 0);
        GameObject.Find("Crosshair1").transform.Find("Outer").gameObject.transform.position = new Vector3(-200, -200, 0);
        GameObject.Find("Crosshair2").transform.Find("Inner").gameObject.transform.position = new Vector3(-200, -200, 0);
        GameObject.Find("Crosshair2").transform.Find("Outer").gameObject.transform.position = new Vector3(-200, -200, 0);
        GameObject.Find("Crosshair3").transform.Find("Inner").gameObject.transform.position = new Vector3(-200, -200, 0);
        GameObject.Find("Crosshair3").transform.Find("Outer").gameObject.transform.position = new Vector3(-200, -200, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
