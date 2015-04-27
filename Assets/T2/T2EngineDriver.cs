using UnityEngine;
using System.Collections;

public class T2EngineDriver : MonoBehaviour {


	public float	maxLifetime = 0.2f,
					maxForce = -30000f;

	protected ParticleSystem sys;
	protected ConstantForce force;
	protected Axes axes;
	// Use this for initialization
	void Start ()
	{
		sys = this.GetComponent<ParticleSystem>();
		force = this.GetComponent<ConstantForce>();

	}

	protected class Axes
	{
		public string	horizontal,
						vertical,
						other,
						acceleration;
	}


	protected virtual Axes FetchAxis(Controller controller)
	{
		return new Axes(){horizontal = controller.horizontalAxis, vertical = controller.verticalAxis, acceleration = controller.accelerate, other = controller.otherAxis};
	}


	private Axes FetchAxis0()
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

	public float orientation = 1f;


	float ResolveAxis(string name)
	{
		if (name == null || name.Length == 0)
			return 0f;
		float f = Input.GetAxis(name);
		return f;
	}


	// Update is called once per frame
	void Update()
	{


		//bool enabled = Input.GetKey(key);

		if (axes == null || axes.horizontal == null || axes.horizontal.Length == 0)
			axes = FetchAxis0();

		
		if (axes != null)
		{
			Vector3 vec = this.transform.localEulerAngles;
			float v = ResolveAxis(axes.vertical);
			float h = ResolveAxis(axes.horizontal);
			float z = ResolveAxis(axes.other);
			vec.x = Mathf.Clamp(v*0.7f + h * orientation, -1f, 1f) * 35f + 3.2f;
			vec.y = Mathf.Clamp(z,-1f,1f) * -25f + 180f;
			this.transform.localEulerAngles = vec;

			float f = Mathf.Max( Mathf.Max(ResolveAxis(axes.acceleration) , 0f), Mathf.Max(Mathf.Max(Mathf.Abs(h),Mathf.Abs(v)),Mathf.Abs(z)));
			

			if (sys != null)
			{
				sys.enableEmission = f != 0f;
				sys.startLifetime = f * maxLifetime;
			}

			if (force != null)
			{
        		force.relativeForce = new Vector3(0,0,maxForce * f);
			}
		}
	}
}
