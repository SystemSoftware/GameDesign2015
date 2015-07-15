using UnityEngine;
using System.Collections;

public class T4EnemyAI : MonoBehaviour {
    private bool launched = false;
    private GameObject ship;
    private T4EnPath path;
    private int path_i = 0;
    private Vector3[] path_points;
    private Vector3 velocity = Vector3.zero;
    float lerp_pos = 0.0f;

    // Update is called once per frame
    void Update () {
        if (launched) {
            Vector3 prev = path_points[path_i];
            Vector3 pos  = path_points[path_i + 1];

            this.transform.position = Vector3.Lerp(prev, pos, lerp_pos);
            
            lerp_pos += (Time.deltaTime * 8f);
            Debug.Log("lerp_pos=" + lerp_pos);
            if ((lerp_pos >= 1) && (path_i + 1 < path_points.Length - 1)) {
                lerp_pos = 0;
                path_i++;
            } else if (path_i + 1 >= path_points.Length - 1) {
                // end of path reached
                deactivate();
            }
        }
	}

    public void launch() {
        if (!launched) {
            path = transform.parent.GetChild(1).gameObject.GetComponent<T4EnPath>();

            path.collectPathObjects();
            path_points = path.getPathPoints();
            path_i = 1;
            this.transform.position = path_points[path_i];


            launched = true;
        }
    }

    private void deactivate() {
        // hide/deactivate the ship after reaching the end of the path
        launched = false;
        this.gameObject.SetActive(false);
    }
}
