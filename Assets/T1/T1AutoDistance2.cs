using UnityEngine;
using System.Collections;

public class T1AutoDistance2 : DirectEngineDriver {

	// Use this for initialization
	protected new void Start () {
        base.Start();
	}
	
	// Update is called once per frame
    protected new void Update()
    {
        base.Update();




        Collider[] colliders = Physics.OverlapSphere(transform.parent.position, 40f);

        float closestDist = float.MaxValue;
        Vector3 closestPos = Vector3.zero;
        hasClosest = false;

        foreach (var collider in colliders)
        {
            if (collider.GetComponentInParent<Rigidbody>() == target)
                continue;
            Debug.Log(collider);
            //Vector3 relative = collider.transform.worldToLocalMatrix.MultiplyPoint(transform.position);
            Vector3 closest = collider.ClosestPointOnBounds(transform.position);
            Debug.Log(closest);
            Debug.Log(transform.position);
            //closest = collider.transform.TransformPoint(closest);
            float dist = Vector3.Distance(closest, transform.parent.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestPos = closest;
                hasClosest = true;
            }
        }

        if (hasClosest)
        {
            dir = (closestPos - transform.position);
            //Debug.Log(Quaternion.LookRotation((closestPos - transform.position).normalized));
            //this.transform.rotation = rot = Quaternion.LookRotation(dir);
            //this.transform.localRotation.SetLookRotation(closestPos - transform.position);
                //LookAt(closestPos);
            Debug.DrawLine(transform.position, closestPos,Color.red,1f,false);
//            Debug.Log(transform.position);
  //          Debug.Log(closestPos);
        }


	}
    public bool hasClosest;
    public Vector3 dir;
    public Quaternion rot;


}
