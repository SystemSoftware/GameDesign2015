using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class T4Logic : MonoBehaviour {
    public Camera cam;
    public GameObject path;
    bool once = false;
    GameObject fog;
    GameObject gui;
    GameObject countdown;
    private Maximize m;

    void OnGUI() {
        if (!Level.AllowMotion) {
            // no motion
            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2, 250, 40), "Start")) {
                foreach (var ship in Level.ActiveShips) {
					ship.transform.position = path.transform.position;
                    //ship.transform.position = new Vector3(0, 141.7f, -2029);
                    ship.ctrlAttachedCamera.farClipPlane = 6000f;
					//ship.gameObject.AddComponent<T4HelloWorldYeller>();
                    ship.gameObject.AddComponent<T4GUICrosshairHandler>();

                    // add fog to the camera
                    GameObject fog = Resources.Load("T4Fog") as GameObject;
                    GameObject g = Instantiate(fog, new Vector3(0, 0, 15), Quaternion.identity) as GameObject;
                    // make it a child of the playercamera
                    g.transform.SetParent(ship.ctrlAttachedCamera.transform, false);

                }
                var ship_objects = GameObject.FindGameObjectsWithTag("Ship"); //get all ship-objects
                foreach (var ship_object in ship_objects) { //add the following 2 Scripts to each of them
                    ship_object.AddComponent<T4ShootBullet>(); //lets the ship shoot
                    ship_object.AddComponent<T4PlayerCollision>(); //lets them behave acordingly when colliding with special objects
                }
				// destroy level preview camera
                DestroyImmediate(cam.gameObject);
                // enable motion finally (makes startpanel dispear!)
                Level.EnableMotion(true);
                
                // tricky: BUT dont let ships moves before countdown!
                foreach (var ship in Level.ActiveShips) {
                    var bodies = ship.transform.GetComponentsInChildren<Rigidbody>();
                    foreach (var body in bodies) {
                        body.useGravity = false;
                    }
                    var forces = ship.transform.GetComponentsInChildren<ConstantForce>();
                    foreach (var force in forces) {
                        force.enabled = false;
                    }
                }
                countdown.active = true;
                
                // enable gui
                gui.active = true;
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

        countdown = GameObject.Find("Countdown").gameObject;
        countdown.active = false;

        gui = GameObject.Find("GUI").gameObject;
        gui.active = false;
        m = GameObject.Find("GameLogic").GetComponent<Maximize>();
        lastMState = m.maximized;
	}

    float timePassed = 0;
    bool lastMState;
	// Update is called once per frame
	void Update () {
        // kinda a hack but works
        // changed from max to split OR the other way around?
        if (lastMState != m.maximized) {
            // causes unity to call the OnFillVBO function again
            gui.active = !gui.active;
            lastMState = m.maximized;
        }
        // count the time for handling the countdown
        if (timePassed < 14) {
            timePassed += Time.deltaTime;

            // 3 > 2 (wait 3 sec at begin)
            if ((timePassed >= 3) && (timePassed < 4)) {
                Sprite cd_2 = Resources.Load<Sprite>("cd_2");
                countdown.GetComponent<Image>().sprite = cd_2;
            }
            // 2 > 1
            if ((timePassed >= 4.5f) && (timePassed < 5.5f)) {
                Sprite cd_1 = Resources.Load<Sprite>("cd_1");
                countdown.GetComponent<Image>().sprite = cd_1;
            }
            // 1 > GO!
            if ((timePassed >= 6) && (timePassed < 7)) {
                Sprite cd_go = Resources.Load<Sprite>("cd_go");
                countdown.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 200); 
                countdown.GetComponent<Image>().sprite = cd_go;
                // enable motion (now really)
                foreach (var ship in Level.ActiveShips) {
                    var bodies = ship.transform.GetComponentsInChildren<Rigidbody>();
                    foreach (var body in bodies) {
                        body.useGravity = true;
                    }
                    var forces = ship.transform.GetComponentsInChildren<ConstantForce>();
                    foreach (var force in forces) {
                        force.enabled = true;
                    }
                }
            }
            // remove GO!
            if ((timePassed >= 7.5f) && (timePassed < 8.5f)) {
                countdown.active = false;
            }
        }
	}
}
