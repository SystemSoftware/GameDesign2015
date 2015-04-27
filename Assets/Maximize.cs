using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maximize : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


    bool maximized = false;
    List<Rect> originalRects = new List<Rect>();
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
		{
            if (maximized)
            {
                int at = 0;
                foreach (var c in this.GetComponentsInChildren<Camera>())
                {
                    c.rect = originalRects[at];
                    c.enabled = true;
                    GetComponent<ListShips>().AdjustToCameraChange(at);
                    at++;
                }
                maximized = false;
            }
            else
            {
                originalRects.Clear();
                bool first = true;
                foreach (var c in this.GetComponentsInChildren<Camera>())
                {
                    originalRects.Add(c.rect);
                    if (first)
                    {
                        c.rect = new Rect(0, 0, 1, 1);
                    }
                    else
                    {
                        c.enabled = false;
                    }

                    first = false;

                }
                GetComponent<ListShips>().AdjustToCameraChange(0);
                maximized = true;
            }
		}

	}
}
