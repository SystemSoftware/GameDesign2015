using UnityEngine;
using System.Collections;

public class T6Gates : MonoBehaviour {
    float dt;
	// Use this for initialization
	void Start () {
        dt = -5;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider coll)
    {
        if (Time.time - dt > 5)
        {
            coll.GetComponentInParent<T6RaceLogic>().passedGate(this.gameObject);
        }
        dt = Time.time;
    }
}
