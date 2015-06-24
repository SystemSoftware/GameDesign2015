using UnityEngine;
using System.Collections;

public class T2SunBurstLevelLogic: MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Level.angularDrag = 1f;
        Level.drag = 1f;
        Physics.gravity = new Vector3(0, -10, 0);
        Level.InitializationDone();
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

    // Update is called once per frame
    void Update()
    {

    }
}
