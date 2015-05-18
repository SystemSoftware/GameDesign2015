using UnityEngine;
using System.Collections;  

public class ShipMovement : Controller {

	public float idealDistance = 50.0f,
	idealYOffset = 10.0f;

	protected GameObject rbThruster; 
	protected GameObject lbThruster;  
	protected ConstantForce rbtForce;
	protected ConstantForce lbtForce; 
	protected Rigidbody rbtBody; 
	protected Rigidbody lbtBody; 
	private int turnRight;
	private int turnLeft; 
	
	// Use this for initialization
	new void Start () {
		rbThruster = GameObject.FindGameObjectWithTag ("RightBackThruster");
		lbThruster = GameObject.FindGameObjectWithTag ("LeftBackThruster");
		rbtBody = rbThruster.GetComponent <Rigidbody>();
		lbtBody = lbThruster.GetComponent<Rigidbody> ();
		rbtForce = rbThruster.GetComponent<ConstantForce> (); 
		lbtForce = lbThruster.GetComponent<ConstantForce> ();
		turnRight = 0; 
		turnLeft = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis(this.ctrlAxisAccelerate) >= 0.5 || Input.GetAxis(this.ctrlAxisVertical) >= 0.5)
		{
			if (Input.GetAxis(this.ctrlAxisAccelerate) >= 0.5)
			{
				rbtBody.AddRelativeForce (new Vector3 (0, 0, -100));
				lbtBody.AddRelativeForce (new Vector3 (0, 0, -100));
			}

			if (Input.GetAxis(this.ctrlAxisVertical) >= 0.5)
			{
				if(turnLeft > 0){
					lbtBody.AddRelativeForce(new Vector3(0, 0, -20*turnLeft));
					turnLeft = 0; 
				}

				if(turnRight > 0){
					rbtBody.AddRelativeForce(new Vector3(0, 0, -20*turnRight));
					turnRight = 0; 
				}
				rbtBody.drag = 0; 
				lbtBody.drag = 0; 

			}
		} else {

			if(Input.GetAxis(this.ctrlAxisHorizontal) >= 0.5){
				rbtBody.AddRelativeForce(new Vector3(0, 0, -20));
				turnLeft = turnLeft++;
			}
			else{
				if (Input.GetAxis(this.ctrlAxisHorizontal) <= -0.5)
				{
					lbtBody.AddRelativeForce(new Vector3(0, 0, -20));
					turnRight = turnRight++; 
				}
				else{
                   if(turnLeft > 0){
						rbtBody.drag = turnLeft;
						turnLeft = 0; 
					}
					else{
						rbtBody.drag = 1; 
					}

					if(turnRight > 0){
						lbtBody.drag = turnRight;
						turnRight = 0; 

					}
					else{
						lbtBody.drag = 1; 
					} 
				}
			}


		}
		
	}

	void LateUpdate () {
		if (ctrlAttachedCamera != null)
		{
			//Vector3 delta = target.position - this.transform.position;
			
			Vector3 idealLocation =
				transform.position - transform.forward * idealDistance + transform.up * idealYOffset;
			//target.position - delta.normalized * idealDistance;
			//(delta - target.up * Vector3.Dot(delta, target.up)).normalized * idealDistance + target.up * idealYOffset;
			Vector3 deltaToIdeal = idealLocation - ctrlAttachedCamera.transform.position;
			ctrlAttachedCamera.transform.transform.position += deltaToIdeal * (1.0f - Mathf.Pow(0.02f, Time.deltaTime));
			ctrlAttachedCamera.transform.rotation = Quaternion.Lerp(ctrlAttachedCamera.transform.rotation, transform.rotation, (1.0f - Mathf.Pow(0.02f, Time.deltaTime)));
		}
	}
	
	
}
