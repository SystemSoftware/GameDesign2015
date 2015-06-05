using UnityEngine;
using System.Collections;

public class T6PlantesRotateMapCamera : MonoBehaviour {

    public Controller ship;
	// Use this for initialization

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (enabled)
        {
            Vector3 orbitPane = ship.GetComponent<T6Trajectory>().orbitPane;
            Vector2 pane2d = new Vector2(orbitPane.x,orbitPane.y);
            float angle = Vector2.Angle(pane2d,new Vector2(1,1))+ 90;
            rotation(angle);
        }
	}

    public void rotation(float degree)
    {
        float alpha = Mathf.Deg2Rad * degree;
        float x = Mathf.Cos(alpha) * 100000;
        float y = Mathf.Sin(alpha) * 100000;
        float z = 0;
        this.transform.position = new Vector3(x, y, z);
        this.transform.LookAt(new Vector3(0, 0, 0));
    }
}
