using UnityEngine;
using System.Collections;

public class T4PlayerShoot : MonoBehaviour {


	Color c1 = Color.white;
	Color c2 = Color.red;
	LineRenderer lineRenderer;
	public float velocity = 500.0f;
	protected string fire;
	private bool firePressed = false;
	private bool allowfire = true;

	
	void Start() {
		lineRenderer = gameObject.AddComponent<LineRenderer> ();
		lineRenderer.material = new Material (Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(0.5f,2f);
		lineRenderer.SetVertexCount(2);
	}
	
	void Update(){
		
		if (fire == null) {
			fire = FetchFire0 ();
		}
		
		if (fire != null) {
			firePressed=0!=Input.GetAxis(fire);
		}
			
	}


	//Get the Firekey of the Controller
	protected virtual string FetchFire(Controller controller)
	{
		return controller.ctrlAxisFire;
	}

	private string FetchFire0()
	{
		Controller ctrl = GetComponentInParent<Controller>();
		if (ctrl != null)
			return FetchFire(ctrl);
		return null;
	}

	//Disables Laser after defined Time
	IEnumerator DisableRenderer(){
		yield return new WaitForSeconds(0.5f);
		lineRenderer.SetVertexCount (0);
	}

	//Speeds ship up
	void SpeedUp(){
		Transform t = transform.parent;
		Rigidbody rigid = gameObject.GetComponent<Rigidbody> ();
		rigid.AddForce (rigid.transform.forward * 500000);
	}

	//Fires Laser, tests if Target Hit, speeds up if target hit
	IEnumerator Fire(){
		
		RaycastHit hit; //raycast to test for hit
		var origin = transform.position; //origin of laser
		var direction = transform.forward; //direction of laser
		var endPoint = origin + direction * 400; //endpoint of Laser
		int skipShips = ~((1<<8)|(1<<9)|(1<<10)|(1<<11)); //LayerMask to ignore Ship-Colliders with Raycast
		if(Physics.Raycast (origin, direction, out hit, 400f, skipShips)){ //Raycast, ignores Ships
			endPoint=hit.point; //Change endpoint of Laser to hitpoint
			if(hit.transform.tag == "Target"){
				Destroy (hit.transform.gameObject); //if hit.point belongst to gameobject of type target, destroy it
				SpeedUp (); //speed boost for the ship
			}
		}
		lineRenderer.SetVertexCount(2); //show the shot
		lineRenderer.SetPosition(0, origin);
		lineRenderer.SetPosition(1, endPoint);
		StartCoroutine(DisableRenderer());//blend the shot out after some time
		yield return new WaitForSeconds(1);//limit the firerate
		allowfire = true;//allow the next shot to be fired
	}

	void FixedUpdate(){

		//only fire if fire-button pressed and firerate allows for next shot
		if (firePressed&&allowfire) {

			allowfire = false; //disable shooting for a set period
			firePressed = false;
			StartCoroutine(Fire());//shoot the laser

			}
						
		}

}
