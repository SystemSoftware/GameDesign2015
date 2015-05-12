using UnityEngine;
using System.Collections;

public class T1LevelLogic_1 : MonoBehaviour {


    void OnGUI()
    {
        if (!Level.AllowMotion)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2, 250, 40), "Start"))
            {
                foreach (var ship in Level.ActiveShips)
                {
                    ship.Attach(new T1RaceTracker());

                }

                Level.EnableMotion(true);
            }
        }
        else
        {
            foreach (var ship in Level.ActiveShips)
            {
                if (ship.attachedCamera.enabled)
                {

                    GUI.Label(new Rect(Screen.width * ship.attachedCamera.rect.min.x, Screen.height * (1f - ship.attachedCamera.rect.max.y), 50, 50), ship.GetAttachment<T1RaceTracker>().progress.ToString());

                }

            }

        }
    }




	// Use this for initialization
	void Start () {
        Level.drag = 0.3f;
        Level.angularDrag = 0.8f;

        Level.InitializationDone();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
