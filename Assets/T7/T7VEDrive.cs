using UnityEngine;
using System.Collections;

public class T7VEDrive : DirectEngineDriver {

		public int sign = 0;
		public float forceP = 0f;
		
		protected override float Filter(float f)
		{
			forceP = sign == (int)Mathf.Sign(f) ? Mathf.Abs(f) : 0f;
			return forceP;
		}
		
		protected override string FetchAxis(Controller ctrl)
		{
//			Debug.Log (ctrl.ctrlAxisVertical);
			return ctrl.ctrlAxisVertical;
		}
}