using UnityEngine;
using System.Collections;

public class T6RaceLogic : MonoBehaviour {

    static float[] deltaV;
    static int[] turns;

    int lastGateID;
    int last2GateID;
    int currentState;
    float usedV;

    public static void init()
    {
        deltaV = new float[4];
        turns = new int[4];
        for (int i = 0; i < 4; i++)
        {
            deltaV[i] = 500;
        }
    }
	// Use this for initialization
	void Start () {
        last2GateID = -1;
        lastGateID = -1;
        currentState = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float currentThrust = 0;
        foreach (ConstantForce f in this.gameObject.GetComponentsInChildren<ConstantForce>())
        {
            currentThrust += f.relativeForce.magnitude;
        }
        float mass = 0;
        foreach (Rigidbody r in this.gameObject.GetComponentsInChildren<Rigidbody>())
        {
            mass += r.mass;
        }
        currentThrust = currentThrust / Time.fixedDeltaTime / mass;
        usedV += currentThrust;
      //  Debug.Log(usedV);
	}

    public void passedGate(GameObject gate)
    {
        int newGateID = int.Parse(gate.name.Substring(4));
        if (currentState == 4)
        {
            turns[this.GetComponentInParent<Controller>().ctrlControlIndex]++;
            currentState = 1;
            Debug.Log("RUNDE " + turns[this.GetComponentInParent<Controller>().ctrlControlIndex]);
        }

        if ((currentState == 0 && newGateID == 0) || (currentState == 1 && newGateID == 1) || (currentState == 2 && newGateID == 0) || (currentState ==3 && newGateID == 2))
        {
            currentState++;
        }
        
        
    }
}
