using UnityEngine;
using System.Collections;

public class T7SideRings : MonoBehaviour {
	public Vector3 center;
	public float faktor = 0f, maxFaktor=1f;
	
	protected void Start()
	{
		center = GetComponent<MeshFilter> ().mesh.bounds.center;
	}
	
	protected void Update(){
		transform.RotateAround (transform.TransformPoint (center), transform.up, Time.deltaTime * 500f);
	}
}
