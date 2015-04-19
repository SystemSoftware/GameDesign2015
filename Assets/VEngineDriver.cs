using UnityEngine;
using System.Collections;

public class VEngineDriver : EngineDriver {

	public int sign = 0;

	protected override bool DoIgnore(float f)
	{
		return sign != (int)Mathf.Sign(f);
	}


	protected override string FetchAxis(Controller ctrl)
	{
		return ctrl.verticalAxis;
	}


}
