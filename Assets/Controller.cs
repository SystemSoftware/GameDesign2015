using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public Camera myCamera;

	public string	accelerate, custom, 
					horizontalAxis, verticalAxis, otherAxis;


	public void AssignCamera(Camera camera)
	{
		myCamera = camera;
		OnAssignCamera();
	}

	protected virtual void OnAssignCamera()
	{}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
