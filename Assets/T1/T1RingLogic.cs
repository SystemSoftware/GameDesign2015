using UnityEngine;
using System.Collections;

public class T1RingLogic : MonoBehaviour {


    public int index = 0;

	// Use this for initialization
	void Start () {
	
	}


    void OnTriggerEnter (Collider other)
    {
        Controller ctrl = other.GetComponentInParent<Controller>();
        if (ctrl != null)
        {
            T1RaceTracker tracker = ctrl.GetAttachment<T1RaceTracker>();
            tracker.progress = Mathf.Max(tracker.progress, index);
            Debug.Log(ctrl.transform.name + ": " + tracker.progress);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
