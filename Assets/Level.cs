using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {

    private static bool allowMotion = false;

    /**
     * Queries whether or not ships should be allowed motion. Must be queried by control scripts
     * */
    public static bool AllowMotion
    {
        get
        {
            return allowMotion;
        }
    }

    public static bool overrideDriveColor = false;
    public static Color driveColor = Color.white;

    public static float drag = 0, angularDrag = 0;

    private static Controller[] cachedShips = new Controller[0];


    /**
     * Fetches all currently registered ship instances
     * The same prefab ship may be registered multiple times
     */
    public static Controller[] ActiveShips
    {
        get
        {
            return cachedShips;
        }
    }


    /**
     * Sets ship motion. By default ships are disallowed from motion
     * EnableMotion(true) must be called at least once to allow players to move ships
     */
    public static void  EnableMotion(bool enabled)
    {
        foreach (var ship in cachedShips)
        {
            var bodies = ship.transform.GetComponentsInChildren<Rigidbody>();
            foreach (var body in bodies)
            {
                body.isKinematic = !enabled;
                body.useGravity = enabled;
            }
            var forces = ship.transform.GetComponentsInChildren<ConstantForce>();
            foreach (var force in forces)
            {
                force.enabled = enabled;
            }
        }
        allowMotion = enabled;



        GameObject gameLogic = GameObject.Find("GameLogic");
        ListShips listShips = gameLogic.GetComponent<ListShips>();
        ListWorlds listWorlds = gameLogic.GetComponent<ListWorlds>();
        listShips.enabled = !enabled;
        listWorlds.enabled = !enabled;
    }




    /**
     * Redetects all ships present in the local level
     * This method is called automatically where needed
     */
    public static void UpdateShipList(string context, Controller exclude = null)
    {
        Debug.Log("Refreshing ships (" + context + ")");
        List<Controller> controllers = new List<Controller>();
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objects)
        {
            Controller ctrl = obj.GetComponent<Controller>();
            if (ctrl != null && ctrl != exclude)
            {
                controllers.Add(ctrl);
                Debug.Log("Added "+ctrl.name);
            }
        }
        cachedShips = controllers.ToArray();
        EnableMotion(allowMotion);
    }


    void OnLevelWasLoaded(int level)
    {
        UpdateShipList("level loaded: " + level);
        allowMotion = false;
        Physics.gravity = new Vector3(0, 0, 0);
        drag = 0;
        angularDrag = 0;

        startPoints = new System[4]
        {
            new System(){position = new Vector3(-separation,-separation,0), orientation = Quaternion.identity},
            new System(){position = new Vector3(separation,-separation,0), orientation = Quaternion.identity},
            new System(){position = new Vector3(separation,separation,0), orientation = Quaternion.identity},
            new System(){position = new Vector3(-separation,separation,0), orientation = Quaternion.identity}
        };
        overrideDriveColor = false;
    }


    /**
     * Redefines the starting locations for ships.
     * Should be set once the map has finished loading (e.g. in some Start() method)
     */
    public static void DefineStartPoints(Transform[] locations)
    {
        for (int i = 0; i < 4 && i < locations.Length; i++)
            startPoints[i] = new System() { position = locations[i].position, orientation = locations[i].rotation };
    }


    struct System
    {
        public Vector3 position;
        public Quaternion orientation;
    }

    private const float separation = 50f;

    static System[] startPoints = new System[4]
    {
        new System(){position = new Vector3(-separation,-separation,0), orientation = Quaternion.identity},
        new System(){position = new Vector3(separation,-separation,0), orientation = Quaternion.identity},
        new System(){position = new Vector3(separation,separation,0), orientation = Quaternion.identity},
        new System(){position = new Vector3(-separation,separation,0), orientation = Quaternion.identity}
    };



	/**
     * Fetches a starting location for the given input number
     * @param inputNumber Control number (0-3)
     * @param[out] position Resulting center position
     * @param[out] orientation Resulting ship orientation
     */
    public static void GetSpawnPoint(int inputNumber, out Vector3 position, out Quaternion orientation)
    {
        position = startPoints[inputNumber].position;
        orientation = startPoints[inputNumber].orientation;
    }

}
