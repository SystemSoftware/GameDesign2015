using UnityEngine;
using System.Collections;

public class T6RotateThrustFlaps : MonoBehaviour
{
    float selfRotation;
    // Use this for initialization
    void Start()
    {
        this.transform.Rotate(new Vector3(25, 0, 0), Space.Self);
        this.selfRotation = 360f / 12 * float.Parse(this.name.Substring(11));
    }
    // Update is called once per frame
    void Update()
    {
      
    }

   public void Rotate(float acceleration, float up, float right)
    {
       Transform ship = this.transform.parent;
       //  Calculate the position of the point that should be targeted by the flaps
       //                       X-Position                             Ship Offset                Y-Position                                         Z-Position
       Vector3 accelerationVector = (-ship.forward * (acceleration * 30)) - ship.forward * 40 + (-ship.up * 10 * Mathf.Sin(acceleration/2)*up*1.5f) + (ship.right * 10 * Mathf.Sin(acceleration/2)*right*2);
       accelerationVector.Scale(ship.localScale);
       Vector3 pointTo = ship.position + accelerationVector;
       //chase the dot! :3
       Vector3 virtualPoint = this.transform.position + (this.transform.position - pointTo);
       this.transform.LookAt(virtualPoint, ship.up);
       this.transform.Rotate(0, 0, selfRotation, Space.Self);
   }
}
