using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using UnityEngine.UI;

public class T4Logic : MonoBehaviour {
    public Camera cam;
    public GameObject path;
    bool once = false;
    GameObject fog;
    GameObject gui;
    GameObject countdown;
    private Maximize m;
    public GameObject countdownSound, startSound;
    public bool countdownOver = false;
    private Stopwatch stopwatch;
	T4Sound3DLogic soundLogic;
	bool sound1Played = false;
	bool sound2Played = false;
	bool mainThemePlayed = false;
    int numOfPlayers = 0;
    int numOfFinishedPlayers = 0;
    T4GUIGlobalEndHandler gend;
    GameObject p1Ship, p2Ship, p3Ship, p4Ship;
	int PointDifferenceForBoss =60;

    void OnGUI() {
        if (!Level.AllowMotion) {
            // no motion
            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2, 250, 40), "Start")) {
                foreach (var ship in Level.ActiveShips) {
                    numOfPlayers++;
					ship.transform.position = path.transform.position;
                    //ship.ctrlAttachedCamera.farClipPlane = 6000f;
                    ship.ctrlAttachedCamera.farClipPlane = 3300f;
                    // add scripts
                    ship.gameObject.AddComponent<T4GUICrosshairHandler>();
                    ship.gameObject.AddComponent<T4GUIHealthbarHandler>();
                    ship.gameObject.AddComponent<T4GUISpeedbarHandler>();
                    ship.gameObject.AddComponent<T4GUIShotHandler>();
                    ship.gameObject.AddComponent<T4GUIScoreHandler>();
                    ship.gameObject.AddComponent<T4PathHandler>();
                    ship.gameObject.AddComponent<T4CullingMask>();
					ship.gameObject.AddComponent<T4ZeroHealthHandler>();
                    ship.gameObject.AddComponent<T4GUICamEndHandler>();

                    switch(ship.ctrlControlIndex) {
                        case 0:
                            p1Ship = ship.gameObject;
                            break;
                        case 1:
                            p2Ship = ship.gameObject;
                            break;
                        case 2:
                            p3Ship = ship.gameObject;
                            break;
                        case 3:
                            p4Ship = ship.gameObject;
                            break;
                    }

                    int new_layer = (28 + ship.ctrlControlIndex);
                    // set layer of the ship to their worlds
                    ship.gameObject.layer = new_layer;
                    Transform[] childs = ship.gameObject.GetComponentsInChildren<Transform>();
                    for (int i = 0; i < childs.Length; i++) {
                        childs[i].gameObject.layer = new_layer;
                    }

                    // add fog to the camera
                    GameObject fog = Resources.Load("T4Fog") as GameObject;
                    GameObject g = Instantiate(fog, new Vector3(0, 0, 15), Quaternion.identity) as GameObject;
                    g.layer = new_layer;
                    // switch layer of fog childs
                    Transform[] fogChilds = g.GetComponentsInChildren<Transform>();
                    for (int i = 0; i < fogChilds.Length; i++) {
                        fogChilds[i].gameObject.layer = new_layer;
                    }
                    ship.ctrlAttachedCamera.gameObject.layer = new_layer;

                    // make it a child of the playercamera
                    g.transform.SetParent(ship.ctrlAttachedCamera.transform, false);


                    /*-------------------------------Ship Fixes-------------------------------------------*/
                    /* T1 Simple */
                    if (ship.gameObject.name.StartsWith("Team 1/Simple")) {
                        // fix here
                        ship.GetComponent<Rigidbody>().mass = 100;
                        /** Todo-List
                         * iterate over childs and manipulate the scripts to a higher force influence cause we set the mass to 100
                         * but the ships forces of thrusters and engine are adjusted to a mass of 1 */
                        Transform[] t1SimpleChilds = ship.gameObject.GetComponentsInChildren<Transform>();
                        for (int i = 0; i < t1SimpleChilds.Length; i++) {
                            EngineDriver simpleTmp1;
                            VEngineDriver simpleTmp2;
                            HEngineDriver simpleTmp3;

                            int enhanceForcesBy = 100;
                            // Accel > reconfig EngineDriver
                            if(t1SimpleChilds[i].gameObject.name.Equals("Accel")){
                                t1SimpleChilds[i].gameObject.GetComponent<EngineDriver>().maxForce = t1SimpleChilds[i].gameObject.GetComponent<EngineDriver>().maxForce * (enhanceForcesBy+20);
                            }
                            // DirectionalThruster Down > reconfig VEngineDriver
                            if (t1SimpleChilds[i].gameObject.name.Equals("DirectionalThruster Down")) {
                                t1SimpleChilds[i].gameObject.GetComponent<VEngineDriver>().maxForce = t1SimpleChilds[i].gameObject.GetComponent<VEngineDriver>().maxForce * enhanceForcesBy;
                            }
                            // DirectionalThruster Up > reconfig VEngineDriver
                            if (t1SimpleChilds[i].gameObject.name.Equals("DirectionalThruster Up")) {
                                t1SimpleChilds[i].gameObject.GetComponent<VEngineDriver>().maxForce = t1SimpleChilds[i].gameObject.GetComponent<VEngineDriver>().maxForce * enhanceForcesBy;
                            }
                            // DirectionalThruster Left Left > reconfig HEngineDriver
                            if (t1SimpleChilds[i].gameObject.name.Equals("DirectionalThruster Left Left")) {
                                t1SimpleChilds[i].gameObject.GetComponent<HEngineDriver>().maxForce = t1SimpleChilds[i].gameObject.GetComponent<HEngineDriver>().maxForce * enhanceForcesBy;
                            }
                            // DirectionalThruster Left Right > reconfig HEngineDriver
                            if (t1SimpleChilds[i].gameObject.name.Equals("DirectionalThruster Left Right")) {
                                t1SimpleChilds[i].gameObject.GetComponent<HEngineDriver>().maxForce = t1SimpleChilds[i].gameObject.GetComponent<HEngineDriver>().maxForce * enhanceForcesBy;
                            }
                            // DirectionalThruster Right Right > reconfig HEngineDriver
                            if (t1SimpleChilds[i].gameObject.name.Equals("DirectionalThruster Right Right")) {
                                t1SimpleChilds[i].gameObject.GetComponent<HEngineDriver>().maxForce = t1SimpleChilds[i].gameObject.GetComponent<HEngineDriver>().maxForce * enhanceForcesBy;
                            }
                            // DirectionalThruster Right Left > reconfig HEngineDriver
                            if (t1SimpleChilds[i].gameObject.name.Equals("DirectionalThruster Right Left")) {
                                t1SimpleChilds[i].gameObject.GetComponent<HEngineDriver>().maxForce = t1SimpleChilds[i].gameObject.GetComponent<HEngineDriver>().maxForce * enhanceForcesBy;
                            }
                        }
                    }
                    /* T2 Delusion */
                    if (ship.gameObject.name.Equals("Team 2/Delusion")) {
                        // slightly increase the moveability by 20%
                        Transform[] t2DelusionChilds = ship.gameObject.GetComponentsInChildren<Transform>();
                        for (int i = 0; i < t2DelusionChilds.Length; i++) {
                            if (t2DelusionChilds[i].gameObject.name.Equals("Accel")) {
                                t2DelusionChilds[i].gameObject.GetComponent<EngineDriver>().maxForce = t2DelusionChilds[i].gameObject.GetComponent<EngineDriver>().maxForce * 1.2f;
                            }
                            if (t2DelusionChilds[i].gameObject.name.Equals("Accel 3")) {
                                t2DelusionChilds[i].gameObject.GetComponent<T2EngineDriver>().maxForce = t2DelusionChilds[i].gameObject.GetComponent<T2EngineDriver>().maxForce * 1.2f;
                            }
                            if (t2DelusionChilds[i].gameObject.name.Equals("Accel 4")) {
                                t2DelusionChilds[i].gameObject.GetComponent<T2EngineDriver>().maxForce = t2DelusionChilds[i].gameObject.GetComponent<T2EngineDriver>().maxForce * 1.2f;
                            } 
                        }
                    }
                    /* T3 BountyOne */
                    if (ship.gameObject.name.StartsWith("Team 3/BountyOne")) {
						CapsuleCollider bountyCol = ship.gameObject.AddComponent<CapsuleCollider>() as CapsuleCollider;
						bountyCol.height=90;
						bountyCol.radius=30;
						bountyCol.center=new Vector3(0,0,10);
						bountyCol.direction=2;
						bountyCol.isTrigger=true;
                        // resize the ship 
                        ship.gameObject.transform.localScale = new Vector3((ship.gameObject.transform.localScale.x / 3), 
                                                                            (ship.gameObject.transform.localScale.y / 3), 
                                                                            (ship.gameObject.transform.localScale.z / 3));
                        // lower max speed
                        Transform[] t3BountyOneChilds = ship.gameObject.GetComponentsInChildren<Transform>();
                        for (int i = 0; i < t3BountyOneChilds.Length; i++) {
                            if (t3BountyOneChilds[i].gameObject.name.Equals("Thruster")) {
                                t3BountyOneChilds[i].gameObject.GetComponent<EngineDriver>().maxForce = 6500;
                            }
                            if (t3BountyOneChilds[i].gameObject.name.Equals("Thruster1")) {
                                // remove the particlesys
                                Destroy(t3BountyOneChilds[i].gameObject.GetComponent<ParticleSystem>());
                            }
                            if (t3BountyOneChilds[i].gameObject.name.Equals("Thruster2")) {
                                // remove the particlesys
                                Destroy(t3BountyOneChilds[i].gameObject.GetComponent<ParticleSystem>());
                            }
                            // adjust the forces
                            if (t3BountyOneChilds[i].gameObject.name.Equals("HThrusterDown")) {
                                t3BountyOneChilds[i].gameObject.GetComponent<VEngineDriver>().maxForce = 15;
                            }
                            if (t3BountyOneChilds[i].gameObject.name.Equals("HThrusterUp")) {
                                t3BountyOneChilds[i].gameObject.GetComponent<VEngineDriver>().maxForce = 15;
                            }
                            if (t3BountyOneChilds[i].gameObject.name.Equals("DirectionalThruster")) {
                                t3BountyOneChilds[i].gameObject.GetComponent<HEngineDriver>().maxForce = 28;
                            }
                            if (t3BountyOneChilds[i].gameObject.name.Equals("DirectionalThruster 1")) {
                                t3BountyOneChilds[i].gameObject.GetComponent<HEngineDriver>().maxForce = 28;
                            }
                        }
                    }
					/* T6 SpaceCat */
					if (ship.gameObject.name.StartsWith("Team 6/SpaceCat")) {
						T6Horn hornscript = ship.GetComponentsInChildren<T6Horn>()[0];
						hornscript.enabled=false;
					}
                    /* T7 DOSE */
                    if (ship.gameObject.name.StartsWith("Team 7/DOSE")) {
                        // freeze position cause this ship doesnt react to Level.EnableMotion
                        ship.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;

                        // lower max speed
                        Transform[] t7DOSEChilds = ship.gameObject.GetComponentsInChildren<Transform>();
                        for (int i = 0; i < t7DOSEChilds.Length; i++) {
                            if(t7DOSEChilds[i].gameObject.name.Equals("Thruster")){
                                t7DOSEChilds[i].gameObject.GetComponent<T7SignaledDirectEngineDriver>().maxForce = 300000;
                            }
                            if (t7DOSEChilds[i].gameObject.name.Equals("SideThrusterRightTH")) {
                                t7DOSEChilds[i].gameObject.GetComponent<T7HEDrive>().maxForce = 20000;
                            }
                            if (t7DOSEChilds[i].gameObject.name.Equals("SideThrusterLeftTH")) {
                                t7DOSEChilds[i].gameObject.GetComponent<T7HEDrive>().maxForce = 20000;
                            }
                            if (t7DOSEChilds[i].gameObject.name.Equals("SideThrusterUpRight")) {
                                t7DOSEChilds[i].gameObject.GetComponent<T7VEDrive>().maxForce = -100000;
                            }
                            if (t7DOSEChilds[i].gameObject.name.Equals("SideThrusterDownRight")) {
                                t7DOSEChilds[i].gameObject.GetComponent<T7VEDrive>().maxForce = 100000;
                            }
                            if (t7DOSEChilds[i].gameObject.name.Equals("SideThrusterUpLeft")) {
                                t7DOSEChilds[i].gameObject.GetComponent<T7VEDrive>().maxForce = -100000;
                            }
                            if (t7DOSEChilds[i].gameObject.name.Equals("SideThrusterDownLeft")) {
                                t7DOSEChilds[i].gameObject.GetComponent<T7VEDrive>().maxForce = 100000;
                            }
                        }
                    }
                }
                var ship_objects = GameObject.FindGameObjectsWithTag("Ship"); //get all ship-objects
                foreach (var ship_object in ship_objects) { //add the following 2 Scripts to each of them
                    ship_object.AddComponent<T4ShootBullet>(); //lets the ship shoot
                    ship_object.AddComponent<T4PlayerCollision>(); //lets them behave acordingly when colliding with special objects
                }
				// destroy level preview camera
                DestroyImmediate(cam.gameObject);

                // collect the path objects
                GameObject.Find("Path").GetComponent<T4PathCollector>().collectPathObjects();

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
		// get SoundLogic
		GameObject soundContainer = GameObject.Find ("SoundContainer");
		soundLogic=soundContainer.GetComponent<T4Sound3DLogic>();
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

        // deactivate the countdown
        countdown = GameObject.Find("Countdown").gameObject;
        countdown.active = false;

        // deactivate the gui
        gui = GameObject.Find("GUI").gameObject;
        gui.active = false;
        m = GameObject.Find("GameLogic").GetComponent<Maximize>();
        lastMState = m.maximized;

        gend = GameObject.Find("Logic").GetComponent<T4GUIGlobalEndHandler>();
	}

    bool lastMState;
	// Update is called once per frame
	void Update () {
        // kinda a hack but works
        // changed from max to split OR the other way around
        if (lastMState != m.maximized) {
            // causes unity to call the OnFillVBO function again
            gui.active = !gui.active;
            lastMState = m.maximized;
        }

        handleCountdown();

	}


    float timePassed = 0;
    void handleCountdown() {
        if (!Level.AllowMotion) { return;  }

        // count the time for handling the countdown
        if (timePassed < 14) {
            timePassed += Time.deltaTime;
			if ((timePassed > 0) && (timePassed < 1)) {
				// PLAY COUNTDOWN SOUND
				if(!sound2Played){
					soundLogic.playCountDownBeep();
					sound2Played=true;
				}
			}
            // 3 > 2 (wait 3 sec at begin)
            if ((timePassed >= 1) && (timePassed < 2)) {
                // PLAY COUNTDOWN SOUND
				if(!sound1Played){
				soundLogic.playCountDownBeep();
					sound1Played=true;
						sound2Played=false;
				}

                Sprite cd_2 = Resources.Load<Sprite>("cd_2");
                countdown.GetComponent<Image>().sprite = cd_2;
            }
            // 2 > 1
            if ((timePassed >= 2) && (timePassed < 3)) {
                // PLAY COUNTDOWN SOUND
				if(!sound2Played){
					soundLogic.playCountDownBeep();
					sound2Played=true;
					sound1Played=false;
				}

                Sprite cd_1 = Resources.Load<Sprite>("cd_1");
                countdown.GetComponent<Image>().sprite = cd_1;
            }
            // 1 > GO!
            if ((timePassed >= 3) && (timePassed < 4)) {
                // PLAY START SOUND!
				if(!sound1Played){
					soundLogic.playStartBeep();
					sound1Played=true;
				}
				//PLAY MAIN THEME!
				if(!mainThemePlayed){
					soundLogic.playMainTheme();
					mainThemePlayed=true;
				}
                
                Sprite cd_go = Resources.Load<Sprite>("cd_go");
                countdown.GetComponent<RectTransform>().sizeDelta = new Vector2(1024, 256);
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

                    if (ship.gameObject.name.StartsWith("Team 7/DOSE")) {
                        ship.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    }
                }
                countdownOver = true;

                stopwatch = new Stopwatch();
                stopwatch.Start();
            }
            // remove GO!
            if ((timePassed >= 4) && (timePassed < 5f)) {
                countdown.active = false;
			}
        }
    }

    public int getPassedTimeInSeconds() {
        if (stopwatch != null) {
            return stopwatch.Elapsed.Seconds + stopwatch.Elapsed.Minutes * 60;
        }
        return 0;
    }

    public void playerFinished(int layer) {
		int points = PointDifferenceForBoss * (numOfPlayers-numOfFinishedPlayers); //points for killing the boss
		UnityEngine.Debug.Log("points =" + points );
        switch ((layer - 28)) {
            case 0:
                p1Ship.GetComponent<T4GUIScoreHandler>().addScore(points);
                p1Ship.GetComponent<T4GUICamEndHandler>().playEnd();
                gend.setRow1(p1Ship.name.Substring(Math.Max(0, p1Ship.name.Length - 5)), p1Ship.GetComponent<T4GUIScoreHandler>().getScore());
                break;
            case 1:
                p1Ship.GetComponent<T4GUIScoreHandler>().addScore(points);
                p2Ship.GetComponent<T4GUICamEndHandler>().playEnd();
                gend.setRow2(p2Ship.name.Substring(Math.Max(0, p2Ship.name.Length - 5)), p2Ship.GetComponent<T4GUIScoreHandler>().getScore());
                break;
            case 2:
                p1Ship.GetComponent<T4GUIScoreHandler>().addScore(points);
                p3Ship.GetComponent<T4GUICamEndHandler>().playEnd();
                gend.setRow3(p3Ship.name.Substring(Math.Max(0, p3Ship.name.Length - 5)), p3Ship.GetComponent<T4GUIScoreHandler>().getScore());
                break;
            case 3:
                p1Ship.GetComponent<T4GUIScoreHandler>().addScore(points);
                p4Ship.GetComponent<T4GUICamEndHandler>().playEnd();
                gend.setRow4(p4Ship.name.Substring(Math.Max(0, p4Ship.name.Length - 5)), p4Ship.GetComponent<T4GUIScoreHandler>().getScore());
                break;
        }

        numOfFinishedPlayers++;
        if (numOfPlayers == numOfFinishedPlayers) {
            gend.playEnd();
        }

    }
}
