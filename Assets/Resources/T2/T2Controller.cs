using UnityEngine;
using System.Collections;

public class T2Controller : Controller {

	Transform shipTransform;


	public float idealDistance = 50.0f,
					idealYOffset = 10.0f;
	
	// Use this for initialization
	void Start () {
		foreach (Transform child in transform)
		{
			shipTransform = child;
			break;
		}


	}


	protected override void OnAssignCamera()
	{ 
		float w = myCamera.rect.width/4,
			h = myCamera.rect.height / 4;
		Rect pos = new Rect(myCamera.rect.center.x-w/2,myCamera.rect.min.y,w,h);

		//if (shipTransform != null)
			this.GetComponentInChildren<Camera>().rect = pos;
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (shipTransform != null && myCamera != null)
		{
			Vector3 delta = shipTransform.position - this.transform.position;

			Vector3 idealLocation =
				//shipTransform.position - shipTransform.forward * idealDistance + shipTransform.up * idealYOffset;
				shipTransform.position - delta.normalized * idealDistance;
			//(delta - target.up * Vector3.Dot(delta, target.up)).normalized * idealDistance + target.up * idealYOffset;
			Vector3 deltaToIdeal = idealLocation - myCamera.transform.position;
			myCamera.transform.transform.position += deltaToIdeal * (1.0f - Mathf.Pow(0.02f, Time.deltaTime));
			myCamera.transform.LookAt(shipTransform.position);

		}
	}
}
