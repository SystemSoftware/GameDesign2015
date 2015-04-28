using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Assign : MonoBehaviour {

	// GameObject	targetType;

	//public int inputNumber;

	GameObject	playerShip = null;
    int lastInput = -1;

	// Use this for initialization
	void Start () {

	}


	public void Setup(GameObject targetType, int inputNumber)
	{
        Controller exclude = null;
		if (playerShip != null)
		{
            exclude = playerShip.GetComponent<Controller>();
			Destroy(playerShip);
			playerShip = null;
		}

        Vector3 position;
        Quaternion orientation;
        Level.GetSpawnPoint(inputNumber, out position, out orientation);
        playerShip = (GameObject)Instantiate(targetType, position, orientation);
        playerShip.layer = 8 + inputNumber;
        playerShip.tag = "Ship";

        lastInput = inputNumber;

        AdjustToCameraChange();




        Level.UpdateShipList("ship spawned: "+inputNumber,exclude);
	}

 
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AdjustToCameraChange()
    {
        var scripts = playerShip.GetComponents<MonoBehaviour>();
        for (int i = 0; i < scripts.Length; i++)
        {
            MonoBehaviour data = scripts[i];
            Controller controller = data as Controller;
            if (controller != null)
            {
                controller.AssignCameraAndControl(GetComponent<Camera>(), lastInput);
            }
        }
    }
}
