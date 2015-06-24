using UnityEngine;
using System.Collections;

public class T7Level : MonoBehaviour {


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
                if (ship.ctrlAttachedCamera.enabled)
                {

                    GUI.Label(new Rect(Screen.width * ship.ctrlAttachedCamera.rect.min.x, Screen.height * (1f - ship.ctrlAttachedCamera.rect.max.y), 50, 50), ship.GetAttachment<T1RaceTracker>().progress.ToString());

                }

            }

        }
    }




	// Use this for initialization
	void Start () {
        Level.drag = 0.3f;
        Level.angularDrag = 0.8f;
		Physics.gravity = new Vector3 (0f,0f,0f);

        Level.InitializationDone();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
