using UnityEngine;
using System.Collections;

public class T7VEDrive : DirectEngineDriver {

		public int sign = 0;
		public float forceP = 0f;
		
		protected override float Filter(float f)
		{
		//	Debug.Log (forceP);
			forceP = sign == (int)Mathf.Sign(f) ? Mathf.Abs(f) : 0f;
		Debug.Log (f + " => " + forceP);
			return forceP;
		}
		
		protected override string FetchAxis(Controller ctrl)
		{
//			Debug.Log (ctrl.ctrlAxisVertical);
			return ctrl.ctrlAxisVertical;
		}
}