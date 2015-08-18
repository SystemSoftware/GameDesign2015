using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class T6PlantesLogic : MonoBehaviour {

    static Vector3 gravity;
    bool ended = false;
    void OnGUI()
    {
        if (!Level.AllowMotion)
        {
            if (!this.ended)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2, 250, 40), "Start"))
                {
                    init();
                    //TODO: Counter
                    Level.EnableMotion(true);
                }
            }
            else
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2, 250, 40), "Reset"))
                {
                    Application.LoadLevel(Application.loadedLevel);   
                }
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
                    mapCamera.GetComponent<Camera>().rect = new Rect(0.3f, 0.5f, 0.2f, 0.2f);
                    break;
                case 1:
                    mapCamera.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.2f, 0.2f);
                    break;
                case 2:
                    mapCamera.GetComponent<Camera>().rect = new Rect(0.3f,0.3f, 0.2f, 0.2f);
                    break;
                case 3:
                    mapCamera.GetComponent<Camera>().rect = new Rect(0.5f,0.3f, 0.2f, 0.2f);
                    break;                    
            }
            // GameObject mapCamera = new GameObject("MapCamera" + ship.ctrlControlIndex);
            mapCamera.GetComponent<Camera>().enabled = true;
            mapCamera.AddComponent<T6PlantesRotateMapCamera>().ship = ship;
			Material mat = Resources.Load<Material>("T6Additional/Line"+ship.ctrlControlIndex);

            ship.gameObject.AddComponent<LineRenderer>().material = mat;
            ship.gameObject.AddComponent<T6ViewController>().enabled = false;
            ship.gameObject.GetComponent<T6ViewController>().setCameras(mapCamera.GetComponent<Camera>(), ship.ctrlAttachedCamera);
            ship.gameObject.AddComponent<T6Trajectory>();
            ship.gameObject.AddComponent<T6UIController>();
            T6RaceLogic.init();
            ship.gameObject.AddComponent<T6RaceLogic>();
            GameObject positionOrb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            positionOrb.transform.position = ship.transform.position;
            positionOrb.transform.localScale = new Vector3(1000, 1000, 1000);
			positionOrb.AddComponent<T6UpdateShipPosition>().ship = ship.transform;
			positionOrb.GetComponent<MeshRenderer>().material = mat;
           // positionOrb.transform.parent = ship.transform;
            Destroy(positionOrb.GetComponent<SphereCollider>());


            positionOrb.layer = 5;
        }
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

	public void end(){
		Level.EnableMotion(false);
		string s = "" ;
		int[] turns = T6RaceLogic.getRounds();
		int[] pos = new int[4];
		float[] points = new float[4];
		int[] ships = {0,1,2,3};
		float[] dv = T6RaceLogic.getDeltaV ();
		foreach(Controller ship in Level.ActiveShips){
			pos[ship.ctrlControlIndex] = ship.GetComponent<T6RaceLogic>().getPosition();
		}
		//1st:
		for (int i=0; i<4; i++) {
			points[i] = (turns[i]+1) * (T6UIController.maxFuel+1) * 5 + (pos[i]+1) * (T6UIController.maxFuel+1) + dv[i];
		}

		Array.Sort (points, ships);
		foreach(Controller ship in Level.ActiveShips){
			ship.GetComponent<T6UIController>().reset();
		}
        Debug.Log("END OF GAME");
		for (int i=3; i>=0; i--) {
			s += (4-i)+".:   Spieler "+ships[i] +" mit "+turns[ships[i]] +" Runden, "+pos[ships[i]]+" Teilen und "+(int) dv[ships[i]]+" Energie übrig\n";
		}
        Text t = GameObject.Find("AwesomeText").GetComponent<Text>() ;
        t.text = s;
        ended = true;
		//GUI.TextField (new Rect (10, 10, 50, 50), s);
	}

}
