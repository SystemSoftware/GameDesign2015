using UnityEngine;
using System.Collections;

public class T2ViperVDriver : DirectEngineDriver
{
    public int sign = 1;



    protected override float Filter(float f)
    {
        f *= sign;
        return Mathf.Max(f, 0f); //thruster logic
    }



    protected override string FetchAxis(Controller controller)
    {
        return controller.ctrlAxisVertical;
    }



	// Update is called once per frame
	protected new void Update () {
        base.Update();
	}
}
