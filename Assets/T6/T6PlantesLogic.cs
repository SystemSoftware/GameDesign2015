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
    void OnGUI()
    {
        if (!Level.AllowMotion)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2, 250, 40), "Start"))
            {

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
	// Use this for initialization
	void Start () {
        Level.drag = 0.3f;
        Level.angularDrag = 0.8f;
        Level.InitializationDone();
        Camera mapCameraProto = GameObject.Find("MapCamera").GetComponent<Camera>();
        foreach (var ship in Level.ActiveShips)
        {
            ship.GetComponent<Controller>().ctrlAttachedCamera.farClipPlane = 100000;
            GameObject mapCamera = new GameObject("MapCamera" + ship.ctrlControlIndex);
            mapCamera.AddComponent<Camera>().CopyFrom(mapCameraProto);
            mapCamera.transform.position = mapCameraProto.transform.position;
            mapCamera.transform.rotation = mapCameraProto.transform.rotation;
            mapCamera.GetComponent<Camera>().enabled = false;
            ship.gameObject.AddComponent<T6ViewController>();
        }
	}

	// Update is called once per frame
	void Update () {
        
	}
}
