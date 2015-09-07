using UnityEngine;
using System.Collections;

public class T6ViewController : MonoBehaviour {

   Camera mapCamera;
   Camera gameCamera;

	// Use this for initialization
	void Start () {
        
	}

    void OnEnable()
    {
        Controller ctrl = GetComponent<Controller>();
        mapCamera = GameObject.Find("MapCamera" + ctrl.ctrlControlIndex).GetComponent<Camera>();
        gameCamera = ctrl.ctrlAttachedCamera;
    }

    public void setCameras(Camera map, Camera game)
    {
        mapCamera = map;
        gameCamera = game;
        this.enabled = true;
     }
	

    public LineRenderer getLineRenderer()
    {
		return this.GetComponentInChildren<LineRenderer>();
    }



}
