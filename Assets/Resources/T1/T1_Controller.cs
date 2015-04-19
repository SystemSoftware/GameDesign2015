using UnityEngine;
using System.Collections;

public class T1_Controller : Controller {

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
	
	// Update is called once per frame
	void LateUpdate () {
		if (shipTransform != null && myCamera != null)
		{
			//Vector3 delta = target.position - this.transform.position;

			Vector3 idealLocation =
				shipTransform.position - shipTransform.forward * idealDistance + shipTransform.up * idealYOffset;
			//target.position - delta.normalized * idealDistance;
			//(delta - target.up * Vector3.Dot(delta, target.up)).normalized * idealDistance + target.up * idealYOffset;
			Vector3 deltaToIdeal = idealLocation - myCamera.transform.position;
			myCamera.transform.transform.position += deltaToIdeal * (1.0f - Mathf.Pow(0.02f, Time.deltaTime));
			myCamera.transform.rotation = Quaternion.Lerp(myCamera.transform.rotation, shipTransform.rotation, (1.0f - Mathf.Pow(0.02f, Time.deltaTime)));
			;

		}
	}
}
