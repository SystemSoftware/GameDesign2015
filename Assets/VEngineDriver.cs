using UnityEngine;
using System.Collections;

public class VEngineDriver : EngineDriver {

	public int sign = 0;

	protected override float Filter(float f)
	{
        return sign != (int)Mathf.Sign(f) ? Mathf.Abs(f) : 0f;
    }


	protected override string FetchAxis(Controller ctrl)
	{
		return ctrl.verticalAxis;
	}


}
