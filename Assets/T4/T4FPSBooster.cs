using UnityEngine;
using System.Collections;

public class T4FPSBooster : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void enhanceFPS() {
        //Debug.Log("fps booster clicked2");
        // disable the water
        GameObject ocean;
        if((ocean = GameObject.Find("Level/Ocean"))!=null){
            ocean.active = false;
        }

        // lower firespeed of turrets > less explosions
        GameObject worldTurrets;
        if ((worldTurrets = GameObject.Find("World0/EnemyTurrets")) != null) {
            Transform[] childs = worldTurrets.gameObject.GetComponentsInChildren<Transform>();
            for (int i = 0; i < childs.Length; i++) {
                if(childs[i].GetComponent<T4TurretTrigger>() != null){
                    childs[i].GetComponent<T4TurretTrigger>().delay = 1000;
                }
            }
        }

        // this object was clicked - do something
        Destroy(this.gameObject);
    }
}
