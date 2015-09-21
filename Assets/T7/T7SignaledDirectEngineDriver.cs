using UnityEngine;
using System.Collections;

public class T7SignaledDirectEngineDriver : DirectEngineDriver {

	public float forceP = 0f;

	protected override float Filter(float f)
	{
		forceP = Mathf.Max (f, 0f);
		return forceP; //thruster logic
	}
}
