using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Assign : MonoBehaviour {

	// GameObject	targetType;

	//public int inputNumber;

	GameObject	playerShip = null;

	// Use this for initialization
	void Start () {

	}


	public void Setup(GameObject targetType, int inputNumber)
	{
		if (playerShip != null)
		{
			Destroy(playerShip);
			playerShip = null;
		}

        Vector3 position;
        Quaternion orientation;
        Level.GetSpawnPoint(inputNumber, out position, out orientation);

		playerShip = (GameObject)Instantiate(targetType, position, orientation);

		var scripts = playerShip.GetComponents<MonoBehaviour>();
		for (int i = 0; i < scripts.Length; i++)
		{
			MonoBehaviour data = scripts[i];
			Controller controller = data as Controller;
			if (controller != null)
			{
				controller.AssignCameraAndControl(GetComponent<Camera>(),inputNumber);

			}
		}

        Level.UpdateShipList("ship spawned: "+inputNumber);
	}

 
	
	// Update is called once per frame
	void Update () {
	
	}
}
