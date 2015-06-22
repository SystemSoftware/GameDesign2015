using UnityEngine;
using System.Collections;

public class T2InsideCheckpoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Controller ctrl = other.GetComponentInParent<Controller>();
        if (ctrl != null)
        {
            foreach (var body in ctrl.GetComponentsInChildren<Rigidbody>())
                body.useGravity = false;
            //T1RaceTracker tracker = ctrl.GetAttachment<T1RaceTracker>();
            //tracker.progress = Mathf.Max(tracker.progress, index);
            //Debug.Log(ctrl.transform.name + ": " + tracker.progress);
        }
    }


    void OnTriggerExit(Collider other)
    {
        Controller ctrl = other.GetComponentInParent<Controller>();
        if (ctrl != null)
        {
            foreach (var body in ctrl.GetComponentsInChildren<Rigidbody>())
                body.useGravity = true;
        }
    }

    void OnTriggerStay (Collider other)
    {
        Controller ctrl = other.GetComponentInParent<Controller>();
        if (ctrl != null)
        {
            foreach (var body in ctrl.GetComponentsInChildren<Rigidbody>())
                body.useGravity = false;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
