using UnityEngine;
using System.Collections;

public class T6PlantesLogic : MonoBehaviour {

    static Vector3 gravity;

    void OnGUI()
    {
        if (!Level.AllowMotion)
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

}
