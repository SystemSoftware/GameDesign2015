using UnityEngine;
using System.Collections;

public class ListWorlds : MonoBehaviour {

	
	public void LoadWorld(int index)
	{
		DontDestroyOnLoad(this);

		
		Application.LoadLevel(index);

		GetComponent<ListShips>().Reset();
		currentLevel = index;
	}

	int currentLevel = 0;



	void OnGUI()
	{

		for (int i = 1; i < Application.levelCount; i++)
		{
			if (GUI.Toggle(new Rect(Screen.width / 2 - 125, Screen.height/2 + i * 30, 250, 30), currentLevel == i, "World "+i))
			{
				if (currentLevel != i)
					LoadWorld(i);
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
