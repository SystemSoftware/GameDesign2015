using UnityEngine;
using System.Collections;

public class AudioListenerMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    static Vector4 Convert(Quaternion q)
    {
        return new Vector4(q.x, q.y, q.z, q.w);
    }
    static Quaternion Convert(Vector4 q)
    {
        return new Quaternion(q.x, q.y, q.z, q.w);
    }

    // Update is called once per frame
    void Update () {
        Vector3 position = Vector3.zero;
        Vector4 rotation = Vector4.zero;
        int stacked = 0;

        foreach (var ship in Level.ActiveShips)
        {
            Controller ctrl = ship.GetComponentInChildren<Controller>();
            if (ctrl != null)
            {
                Camera camera = ctrl.ctrlAttachedCamera;
                if (camera != null)
                {
                    position += camera.transform.position;
                    rotation += Convert(camera.transform.rotation);
                    stacked++;
                }
            }
        }

        if (stacked > 0)
        {
            this.transform.position = position / stacked;
            this.transform.rotation = Convert(rotation.normalized);
        }

	}
}
