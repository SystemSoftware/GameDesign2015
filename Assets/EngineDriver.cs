using UnityEngine;
using System.Collections;

public class EngineDriver : MonoBehaviour {


	public float	maxLifetime = 0.5f,
					maxForce = -500000f;

	protected ParticleSystem sys;
	protected ConstantForce force;
	protected string axis;
	// Use this for initialization
	void Start ()
	{
		sys = this.GetComponent<ParticleSystem>();
		force = this.GetComponent<ConstantForce>();

        if (sys != null)
        {
            if (Level.overrideDriveColor)
                sys.startColor = Level.driveColor;
        }



	}


	protected virtual string FetchAxis(Controller controller)
	{
		return controller.accelerate;
	}


	private string FetchAxis0()
	{
		Transform t = transform.parent;
		while(t != null)
		{
			var ctrl = t.GetComponent<Controller>();
			if (ctrl != null)
				return FetchAxis(ctrl);
			t = t.parent;
		}
		return null;
	}


	protected virtual bool DoIgnore(float f)
	{
		return false;

	}

	// Update is called once per frame
	void Update()
	{


		//bool enabled = Input.GetKey(key);

		if (axis == null || axis.Length == 0)
			axis = FetchAxis0();

		
		{
			float f = 0f;
			if (axis != null && axis.Length > 0)
			{ 
				//Debug.Log(axis);
				f = Input.GetAxis(axis);
				if (DoIgnore(f))
					f = 0f;
				else
					f = Mathf.Abs(f);
			}
			if (sys != null)
			{
				sys.enableEmission = f != 0f;
				sys.startLifetime = f * maxLifetime;
			}

			if (force != null)
			{
                force.enabled = f != 0f && Level.allowMotion;
				force.relativeForce = new Vector3(0,0,maxForce * f);
			}
		}
	}
}
