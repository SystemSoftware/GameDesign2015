using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class T1LevelLogic_Water : MonoBehaviour {

    
	// Use this for initialization
	void Start () {

        Level.drag = 1f;
        Level.angularDrag = 2f;
        Level.overrideDriveColor = true;
        Level.driveColor = new Color(0.6f, 0.9f, 1f);
        //Physics.gravity = new Vector3(0, -100, 0);


        List<Transform> startPoints = new List<Transform>();
        foreach (Transform child in transform)
        {
            startPoints.Add(child);
        }

        Level.DefineStartPoints(startPoints.ToArray());

    }

    void OnGUI()
    {

        if (!Level.AllowMotion)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2, 250, 40), "Start"))
            {
                Level.EnableMotion(true);
            }
        }
    }

    public GameObject bubbleSpawner;

    Controller[] lastShips;

	// Update is called once per frame
	void Update () {
        Controller[] ships = Level.ActiveShips;
        if (ships != lastShips)
        {
            foreach (var ship in ships)
            {
                if (lastShips == null || Array.IndexOf(lastShips, ship) == -1)
                {
                    Transform shipTransform = ship.transform;
                    Debug.Log(shipTransform.name);
                    ((GameObject)Instantiate(bubbleSpawner, shipTransform.position, Quaternion.identity)).transform.parent = shipTransform;
                }
            }

            lastShips = ships;


        }



	}
}
