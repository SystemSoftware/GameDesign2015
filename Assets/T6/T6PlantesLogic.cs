using UnityEngine;
using System.Collections;

public class T6PlantesLogic : MonoBehaviour {
    /*
     *  How it works: 
     *  UI:
     *  There are 2 Cameras per player in this level, 1 is the custom chasecam, 1 is a fixed orthocam as map view.
     *  The chasecam is beeing altered to have a long far-pane to make the planets visible.
     *  Each ship-controller is getting a script attached, that controls the change of map and chase view.
     *  For each map camera and thus each player there is a canvas generated on that the GUI elements will be. This canvas is enabled by the viewController that is attached to each Controller.
     *      Each canvas shows the position of all players as well as a approximation of the own trajectory
     *  
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * */

    public static  GameObject planet;
    static Vector3 gravity;
    public static long planetMass;

    void OnGUI()
    {
        if (!Level.AllowMotion)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2, 250, 40), "Start"))
            {
                init();
                Level.EnableMotion(true);
            }
        }
        else
        {
            foreach (var ship in Level.ActiveShips)
            {
                
            }

        }
    }

    void init()
    {
        //Camera mapCameraProto = GameObject.Find("MapCamera").GetComponent<Camera>();
       
        foreach (var ship in Level.ActiveShips)
        {
            ship.ctrlAttachedCamera.farClipPlane = 100000;
            ship.ctrlAttachedCamera.cullingMask &= ~(1 << 5);
			GameObject mapCamera = Instantiate(Resources.Load<GameObject>("T6Additional/T6MapCamera"));
            mapCamera.name = "MapCamera" + ship.ctrlControlIndex;
            //Faster than bitsniffing
            switch (ship.ctrlControlIndex)
            {
                case 0:
                    mapCamera.GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                    break;
                case 1:
                    mapCamera.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                    break;
                case 2:
                    mapCamera.GetComponent<Camera>().rect = new Rect(0,0, 0.5f, 0.5f);
                    break;
                case 3:
                    mapCamera.GetComponent<Camera>().rect = new Rect(0.5f,0, 0.5f, 0.5f);
                    break;                    
            }
            // GameObject mapCamera = new GameObject("MapCamera" + ship.ctrlControlIndex);
            mapCamera.GetComponent<Camera>().enabled = false;
            mapCamera.AddComponent<T6PlantesRotateMapCamera>().ship = ship;
            ship.gameObject.AddComponent<LineRenderer>();
            ship.gameObject.AddComponent<T6ViewController>().enabled = false;
            ship.gameObject.GetComponent<T6ViewController>().setCameras(mapCamera.GetComponent<Camera>(), ship.ctrlAttachedCamera);
            ship.gameObject.AddComponent<T6Trajectory>().enabled = true;
            GameObject positionOrb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            positionOrb.transform.position = ship.transform.position;
            positionOrb.transform.localScale = new Vector3(1000, 1000, 1000);
            positionOrb.transform.parent = ship.transform;
            Destroy(positionOrb.GetComponent<SphereCollider>());
            positionOrb.layer = 5;
            planetMass = 5000000000;
        }
        planet = GameObject.Find("Planet1");
    }

	// Use this for initialization
	void Start () {
        GameObject[] spawnObjects = GameObject.FindGameObjectsWithTag("Spawn");
        Transform[] spawns = new Transform[spawnObjects.Length];
        for (int i = 0; i < spawnObjects.Length; i++)
        {
            spawns[i] = spawnObjects[i].transform;
        }
        Level.DefineStartPoints(spawns);

        Level.InitializationDone();   
	}

	// Update is called once per frame
	void FixedUpdate () {
        if (Level.AllowMotion)
        {
            foreach (var ship in Level.ActiveShips)
            {

                Vector3 direction = planet.transform.position - ship.transform.position;
                long r = (long)direction.magnitude;
                direction.Normalize();
                double G = 6.674;
                gravity = direction * (float)(G * ship.GetComponent<Rigidbody>().mass * planetMass / (r * r));
                ship.GetComponent<Rigidbody>().AddForce(gravity);

            }
        }
	}
}
