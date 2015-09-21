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


	
    struct ShipCollection
    {
        public int teamIndex;
        public GameObject[] members;

    }


    List<ShipCollection> collections = new List<ShipCollection>();


	GameObject[]	selected = null;


	public void Reset()
	{
		selected = null;
	}

	public void AdjustToCameraChange(int index)
	{
		Camera[] cameras = this.GetComponentsInChildren<Camera>();
		if (selected[index] != null)
		{
			cameras[index].GetComponent<Assign>().AdjustToCameraChange();
		}


	}

	void OnGUI()
	{




        if (collections.Count == 0)
        {
            int counter = 0;
            for (; ; )
            {
                counter++;
                GameObject[] ships = Resources.LoadAll<GameObject>("T" + counter);

                //GameObject resource = (GameObject)Resources.Load("T"+counter+"/Ship");
                if (ships == null || ships.Length == 0)
                {
                    if (counter > 20)
                        break;
                    else
                        continue;
                }
                collections.Add(new ShipCollection() { teamIndex = counter, members = ships });

            }
        }

		Camera[] cameras = this.GetComponentsInChildren<Camera>();
		if (selected == null || selected.Length != cameras.Length)
			selected = new GameObject[cameras.Length];
        int lineCounter = 0;
		foreach (var collection in collections)
		{
            foreach (var ship in collection.members)
            {
                string name = "Team "+collection.teamIndex+"/"+ship.name;
                int i = 0;
                foreach (var c in cameras)
                {
                    //Debug.Log(c.rect.min);
                    float h = 20;
                    float y = Screen.height * (1f - c.rect.max.y) + 30 + lineCounter * h;
                    if (c.isActiveAndEnabled 
                        && GUI.Toggle(new Rect(Screen.width * c.rect.min.x + Screen.width / 4 - 125, y, 250, h), selected[i] == ship, name))
                    {
                        if (selected[i] != ship)
                        {
                            selected[i] = ship;

                            c.GetComponent<Assign>().Setup(ship, i, name);
                        }
                    }
                    i++;

                }
                lineCounter++;
            }




		}
	}
}
