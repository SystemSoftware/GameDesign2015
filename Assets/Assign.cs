using UnityEngine;
using System.Collections;

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

		playerShip = (GameObject)Instantiate(targetType, Vector3.zero, Quaternion.identity);

		var scripts = playerShip.GetComponents<MonoBehaviour>();
		for (int i = 0; i < scripts.Length; i++)
		{
			MonoBehaviour data = scripts[i];
			Controller controller = data as Controller;
			if (controller != null)
			{
				controller.horizontalAxis = "Horizontal"+inputNumber;
				controller.verticalAxis = "Vertical"+inputNumber;
				controller.accelerate = "Fire1";
				controller.otherAxis = "Other"+inputNumber;
				controller.AssignCamera(GetComponent<Camera>());

			}
		}

	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
