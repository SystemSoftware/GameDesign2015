using UnityEngine;
using System.Collections;

public class T4EnemyShips : MonoBehaviour {
    private GameObject[] enemies;

	// Use this for initialization
	void Start () {
        collectAllShips();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void collectAllShips()
    {
        enemies = new GameObject[transform.childCount];

        Transform[] childs = transform.GetComponentsInChildren<Transform>();
        for(int i= 0; i<transform.childCount; i++){
            Debug.Log("LAYER "+this.gameObject.layer+" enem collected > "+(i)+" > "+transform.GetChild(i));
            
        }
    }
}
