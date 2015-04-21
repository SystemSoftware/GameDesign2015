using UnityEngine;
using System.Collections;
using System;

public class T1LevelLogic_Water : MonoBehaviour {

    GameObject gameLogic;
    ListShips listShips;
    ListWorlds listWorlds;

	// Use this for initialization
	void Start () {

        Level.drag = 1f;
        Level.angularDrag = 2f;
        Level.overrideDriveColor = true;
        Level.driveColor = new Color(0.6f, 0.9f, 1f);
        //Physics.gravity = new Vector3(0, -100, 0);


        gameLogic = GameObject.Find("GameLogic");
        listShips = gameLogic.GetComponent<ListShips>();
        listWorlds = gameLogic.GetComponent<ListWorlds>();
	}

    void OnGUI()
    {

        if (!Level.allowMotion)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2, 250, 40), "Start"))
            {
                listShips.enabled = false;
                listWorlds.enabled = false;
                Level.EnableMotion(true);
                //Physics.gravity = new Vector3(0, -1f, 0);
            }
        }
    }

    public GameObject bubbleSpawner;

    Controller[] lastShips;

	// Update is called once per frame
	void Update () {
        Controller[] ships = Level.GetShips();
        if (ships != lastShips)
        {
            foreach (var ship in ships)
            {
                if (lastShips == null || Array.IndexOf(lastShips, ship) == -1)
                {
                    Transform shipTransform = ship.FindOrFetchShipTransform();
                    Debug.Log(shipTransform.name);
                    ((GameObject)Instantiate(bubbleSpawner, shipTransform.position, Quaternion.identity)).transform.parent = shipTransform;
                }
            }

            lastShips = ships;


        }



	}
}
