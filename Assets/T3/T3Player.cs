using UnityEngine;
using System.Collections;

public class T3Player : MonoBehaviour {
    public int checkpoint;
    static int place;
    public static int maxpoint = 10;
    public int placed;
	// Use this for initialization
	void Start () {
        place = 0;
        placed = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if(checkpoint == maxpoint){
            if (placed == 0)
            {
                placed = ++place;
                Debug.Log("Platz " + placed);
            }
        }
	}
}
