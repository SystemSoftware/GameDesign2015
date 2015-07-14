using UnityEngine;
using System.Collections;

public class T4EnemyAI : MonoBehaviour {
    private bool launched = false;
    private GameObject ship;
    private T4EnPath path;
    private int path_i = 0;
    private GameObject[] path_objects;
    private Vector3 velocity = Vector3.zero;
    float lerp_pos = 0f;

    // Update is called once per frame
    void Update () {
        if (launched) {
            Debug.Log("path points total > "+path.getPathObjects().Length);

            Vector3 prev = path_objects[path_i - 1].transform.position;
            Vector3 pos = path_objects[path_i].transform.position;

            ship.transform.position = Vector3.SmoothDamp(prev, pos, ref velocity, lerp_pos);

            lerp_pos += (0.1f * Time.deltaTime);
        }
	}

    public void launch() {
        ship = transform.GetChild(0).gameObject; // first child is the model
        path = transform.GetChild(1).gameObject.GetComponent<T4EnPath>();

        path.collectPathObjects();
        path_objects = path.getPathObjects();
        path_i = 1;
        ship.transform.position = path_objects[path_i].transform.position;
        

        launched = true;
    }
}
