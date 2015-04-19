using UnityEngine;
using System.Collections;

public class Maximize : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
		{
			bool first = true;
			foreach (var c in this.GetComponentsInChildren<Camera>())
			{
				if (first)
				{
					c.rect = new Rect(0,0,1,1);
				}
				else
				{
					c.enabled = false;
				}

				first = false;

			}
			GetComponent<ListShips>().Reassign(0);

		}

	}
}
