using UnityEngine;
using System.Collections;

public class T7SidePropeller : MonoBehaviour {
	public Vector3 center;
	public T7SignaledDirectEngineDriver tar;
	public HEDrive tarRight;
	public HEDrive tarLeft;
	public VEDrive tarUp;
	public VEDrive tarDown;

	public float faktor = 0f, maxFaktor=1f;
	
	protected void Start()
	{
		center = GetComponent<MeshFilter> ().mesh.bounds.center;
	}
	
	protected void Update(){
		if (tar.forceP == 0f && tarRight.forceP == 0f && tarLeft.forceP == 0f && tarUp.forceP == 0f && tarDown.forceP == 0f){
			transform.RotateAround (transform.TransformPoint (center), transform.up, Time.deltaTime * faktor * 500f);
			faktor -= 0.0125f;
			if(faktor < 0f) faktor = 0f;
		}
		else {
			faktor = maxFaktor;
			transform.RotateAround (transform.TransformPoint (center), transform.up, Time.deltaTime * maxFaktor * 500f);
		}
	}
}
