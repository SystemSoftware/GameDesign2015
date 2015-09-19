using UnityEngine;
using System.Collections;

public class T6RaceLogic : MonoBehaviour {

    static float[] deltaV;
    static int[] turns;

    int lastGateID;
    int last2GateID;
    int currentState;
    public static void init()
    {
        deltaV = new float[4];
        turns = new int[4];
    }
	// Use this for initialization
	void Start () {
        last2GateID = -1;
        lastGateID = -1;
        currentState = 0;

	}
	

    public void passedGate(GameObject gate)
    {
        int newGateID = int.Parse(gate.name.Substring(4));
        if (currentState == 4)
        {
			//turns[this.GetComponentInParent<Controller>().ctrlControlIndex]++;
			if(turns[this.GetComponentInParent<Controller>().ctrlControlIndex]++ == 5){
				GameObject.Find("LevelLogic").GetComponent<T6PlantesLogic>().end();
			}
            currentState = 1;
        }

        if ((currentState == 0 && newGateID == 0) || (currentState == 1 && newGateID == 1) || (currentState == 2 && newGateID == 0) || (currentState ==3 && newGateID == 2))
        {
            currentState++;
        }
        else if ((currentState == 0 && newGateID == 0) || (currentState == 1 && newGateID == 2) || (currentState == 2 && newGateID == 0) || (currentState == 3 && newGateID == 1))
        {
            currentState++;
        }
        
        
    }

	public int getPosition(){
		return currentState;
	}

	public static int[] getRounds(){
		return turns;
	}

	public static float[] getDeltaV(){
		return deltaV;
	}

	public static void setDeltaV(int id, float dv){
		deltaV [id] = dv;
	}
}
