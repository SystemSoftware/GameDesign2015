using UnityEngine;
using System.Collections;

public class T7RingPropAlign : MonoBehaviour {
	public Vector3 center;
	public float faktor = 0f, maxFaktor=1f;
	public int currentRightTransform = 0;
	public T7SignaledDirectEngineDriver tar;
	public T7VEDrive tarVEDown;
	public T7VEDrive tarVE;

	protected void Start()
	{
		if (transform.Find ("Torus") == null) {
		} else {
			center =  transform.Find ("Torus").GetComponent<MeshFilter> ().mesh.bounds.center;
		}
	}
	
	protected void Update(){
		if (tar.forceP != 0f) {
				if (tarVE.forceP != 0f) { //HOCH + FAHREND
					if(currentRightTransform > -45){
						transform.RotateAround (transform.TransformPoint (center), transform.right, -6);
						currentRightTransform -= 6;
					}
					else if(currentRightTransform < -52){
						transform.RotateAround (transform.TransformPoint (center), transform.right, +1);
						currentRightTransform += 1;
					}
				}
				else if (tarVEDown.forceP != 0f) { //RUNTER + FAHREND
					if(currentRightTransform > -135){
						transform.RotateAround (transform.TransformPoint (center), transform.right, -6);
						currentRightTransform -= 6;
					}
					else if(currentRightTransform < -142){
						transform.RotateAround (transform.TransformPoint (center), transform.right, +1);
						currentRightTransform += 1;
					}
				}
				else{
					if(currentRightTransform > -90){
						transform.RotateAround (transform.TransformPoint (center), transform.right, -6);
						currentRightTransform -= 6;
					}
					else{
						if(currentRightTransform < -91){
							transform.RotateAround (transform.TransformPoint (center), transform.right, 1);
							currentRightTransform += 1;
						}
					}

				}
		} else {
			if (tarVEDown.forceP != 0f){ //RUNTER & NICHT FAHREND
				if(currentRightTransform > -180){
					transform.RotateAround (transform.TransformPoint (center), transform.right, -6);
					currentRightTransform -= 6;
				}
			}
			else if(currentRightTransform < 0){
				transform.RotateAround (transform.TransformPoint (center), transform.right, 1);
				currentRightTransform += 1;
			}
		}
	}
}
