using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class T1AutoDistance : DirectEngineDriver {

	// Use this for initialization
    protected new void Start()
    {
        base.Start();
        
	}

    public float idealDistance = 2f;
    public float effectiveAtDistance = 20f;



    protected override float Filter(float f)
    {
        
        
        Ray ray = new Ray(transform.position,-transform.forward);
        RaycastHit hit;

        // Bit shift the index of the layer (8) to get a bit mask
        var layerMask = 1 << 8;
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        if (target != null && Physics.SphereCast(ray, 2f,out hit))
        {
            //float dot = Vector3.Dot(ray.direction, target.velocity);
            float speedTowardsWall = Vector3.Dot(ray.direction, target.velocity);
            float counterAcceleration = 0f;
            if (speedTowardsWall > 0f)
            {
                float timeToImpact = hit.distance / speedTowardsWall;
                float timeToCompensate = Mathf.Max(0.1f, (hit.distance - idealDistance) / speedTowardsWall);
                float severity = (1f - Mathf.SmoothStep(0f, effectiveAtDistance, (hit.distance - idealDistance))) * 2f;
                counterAcceleration = speedTowardsWall / timeToCompensate * severity * 200f;
            //    Debug.Log(hit.distance + " => " + severity);
            }
            if (hit.distance < idealDistance)
            {
                float wantSpeed = (idealDistance - hit.distance);
                float inTime = 0.1f;
                float speedDelta = wantSpeed + speedTowardsWall;
                if (speedDelta > 0f)
                    counterAcceleration += speedDelta / inTime * 1000f;
            }
            //float dot = ;
            //counterAcceleration += Vector3.Dot(ray.direction, acceleration);    //some natural form of acceleration (gravity?)

            if (counterAcceleration > 0f)
                return counterAcceleration * target.mass / maxForce;
            return 0f;
            //dot = Mathf.Max(dot,Vector3.Dot(ray.direction, target.velocity) * 10f);
            
            ////Debug.DrawRay(transform.position, target.velocity*10f);
            //return 1f / hit.distance * Mathf.Max(0f,dot);
        }
        else
            return 0;
    }

    Vector3 lastSpeed = new Vector3(0, 0, 0), acceleration;
    Queue<Vector3> accelerations = new Queue<Vector3>();

    protected void FixedUpdate()
    {
        base.FixedUpdate();
        if (target)
        {
            Vector3 accel = (target.velocity - lastSpeed) / Time.fixedDeltaTime;
            lastSpeed = target.velocity;
            accelerations.Enqueue(accel);
            while (accelerations.Count > 10)
            {
                accelerations.Dequeue();
            }

            acceleration = Vector3.zero;
            foreach (var a in accelerations)
                acceleration += a;
            acceleration /= accelerations.Count;



        }



    }


	
}
