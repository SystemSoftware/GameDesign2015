using UnityEngine;
using System.Collections;

public class ListWorlds : MonoBehaviour {

    static ListWorlds first = null;
	public void LoadWorld(int index)
	{
        Debug.Log("Load World: " + index);
        DontDestroyOnLoad(this);

		
		Application.LoadLevel(index);

		GetComponent<ListShips>().Reset();
		currentLevel = index;


        GetComponent<ListShips>().enabled = index != 0;
	}

	int currentLevel = 0;
    int currentOffset = 0;


	void OnGUI()
	{
        if (currentLevel != 0)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 40, 250, 40), "Unload World"))
            {
                LoadWorld(0);
                //currentOffset = 0;
            }
        }
        else
        {
            int maxLevelsAtOnce = Screen.height /2 / 30;

            float y = Screen.height / 2 - (maxLevelsAtOnce / 2 + 1) * 30;
            float h = 25;

            if (currentOffset > 0)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 100, y, 200, h), "Up"))
                {
                    currentOffset -= maxLevelsAtOnce;
                }
            }
            y += 30;

            for (int i = 1 + currentOffset; i < Application.levelCount; i++)
            {
                if (i - currentOffset > maxLevelsAtOnce)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 100, y, 200, h), "Down"))
                    {
                        currentOffset += maxLevelsAtOnce;
                    }
                    break;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 125, y, 250, h), "World " + i))
                {
                    if (currentLevel != i)
                        LoadWorld(i);
                }
                y += 30;
            }
        }
	}

	// Use this for initialization
	void Start () {
        if (first == null)
            first = this;
        else
            Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
