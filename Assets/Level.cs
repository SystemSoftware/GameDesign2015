using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {

    public static bool allowMotion = false,
                        overrideDriveColor = false;
    public static Color driveColor = Color.white;

    public static float drag = 0, angularDrag = 0;

    private static Controller[] cachedShips = new Controller[0];


    public static Controller[] GetShips()
    {
        return cachedShips;
    }


    /**
     * Redetects all ships present in the local level
     */
    public static void UpdateShipList(string context)
    {
        Debug.Log("Refreshing ships (" + context + ")");
        List<Controller> controllers = new List<Controller>();
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objects)
        {
            Controller ctrl = obj.GetComponent<Controller>();
            if (ctrl != null)
            {
                controllers.Add(ctrl);
            }
        }
        cachedShips = controllers.ToArray();
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


    struct System
    {
        public Vector3 position;
        public Quaternion orientation;
    }

    private const float separation = 20f;

    static System[] startPoints = new System[4]
    {
        new System(){position = new Vector3(-separation,-separation,0), orientation = Quaternion.identity},
        new System(){position = new Vector3(separation,-separation,0), orientation = Quaternion.identity},
        new System(){position = new Vector3(separation,separation,0), orientation = Quaternion.identity},
        new System(){position = new Vector3(-separation,separation,0), orientation = Quaternion.identity}
    };



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void GetSpawnPoint(int inputNumber, out Vector3 position, out Quaternion orientation)
    {
        position = startPoints[inputNumber].position;
        orientation = startPoints[inputNumber].orientation;
    }
}
