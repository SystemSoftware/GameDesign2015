using UnityEngine;
using System.Collections;

public class T4EnemyShips : MonoBehaviour {
    private GameObject[] enemies;
    private int controlID = 0;
    private int next = 0;

	// Use this for initialization
	void Start () {
        controlID = this.gameObject.layer - 28;
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
            enemies[i] = transform.GetChild(i).gameObject;
        }
    }

    public void launchNext()
    {
        enemies[next].SetActive(true);
        enemies[next].GetComponent<T4EnemyAI>().launch();
        next++;
    }
}
