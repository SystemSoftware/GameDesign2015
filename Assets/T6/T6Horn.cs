using UnityEngine;
using System.Collections;

public class T6Horn : MonoBehaviour {

    Controller c;
	// Use this for initialization
	void Start () {
        c = this.GetComponentInParent<Controller>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire" + c.ctrlControlIndex) && Level.AllowMotion)
        {
            this.GetComponent<AudioSource>().Play();
        }
    }
}
