using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class T4KillAfter : MonoBehaviour {
    public int killAfter = 3000;
    private Stopwatch stopwatch;

	// Use this for initialization
	void Start () {
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }
	
	// Update is called once per frame
	void Update () {
        if (stopwatch.ElapsedMilliseconds > killAfter){
            stopwatch.Stop();
            DestroyObject(this.transform.gameObject);
        }
	}
}
