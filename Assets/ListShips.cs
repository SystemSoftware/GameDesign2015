using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListShips : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	

	List<GameObject>	shipPrefabs = new List<GameObject>();


	GameObject[]	selected = null;


	public void Reset()
	{
		selected = null;
	}

	public void Reassign(int index)
	{
		Camera[] cameras = this.GetComponentsInChildren<Camera>();
		if (selected[index] != null)
		{
			cameras[index].GetComponent<Assign>().Setup(selected[index], index);
		}


	}

	void OnGUI()
	{
		
		int counter = 1;
		

		if (shipPrefabs.Count == 0)
			for (;;)
			{
				GameObject resource = (GameObject)Resources.Load("T"+counter+"/Ship");
				counter++;
				if (!resource)
					break;
				shipPrefabs.Add(resource);

			}

		Camera[] cameras = this.GetComponentsInChildren<Camera>();
		if (selected == null || selected.Length != cameras.Length)
			selected = new GameObject[cameras.Length];
		counter = 0;
		foreach (var ship in shipPrefabs)
		{
			string name = "Ship "+(counter+1);
			int i = 0;
			foreach (var c in cameras)
			{
				//Debug.Log(c.rect.min);
			
				if (c.isActiveAndEnabled && GUI.Toggle(new Rect(Screen.width * c.rect.min.x + Screen.width / 4 - 125, Screen.height * (1f - c.rect.max.y) + 50 + counter * 30, 250, 30), selected[i] == ship, name))
				{
					if (selected[i] != ship)
					{ 
						selected[i] = ship;

						c.GetComponent<Assign>().Setup(ship,i);
					}
				}
				i++;
			}
			


			counter ++ ;

		}
	}
}
