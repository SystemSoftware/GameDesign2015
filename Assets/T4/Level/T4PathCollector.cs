using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class T4PathCollector : MonoBehaviour {
    public bool showPoints = false;
    private int object_count;
    private GameObject[] path_objects;
    private Vector3[] path_points;
    private GameObject[] path_pointBelongsTo;

    public GameObject getPathObject(int i) {
        return path_pointBelongsTo[i];    
    }

    public Vector3 getPathPoint(int i) {
        if (i >= 0 && i < path_points.Length - 1) {
            return path_points[i];
        }

        return Vector3.zero;
    }

    public int getPathPointCount() {
        return path_points.Length;
    }

    public void collectPathObjects() {
        Transform[] childs = transform.GetComponentsInChildren<Transform>();
        path_objects = new GameObject[childs.Length];
        int i = 0;

        // collect all objects who form the path
        foreach (Transform c in childs) {
            path_objects[i++] = c.gameObject;
        }
        object_count = i;

        List<GameObject> path_pointBelongsTo_list = new List<GameObject>();
        List<Vector3> path_points_list = new List<Vector3>();
        // calc the points inbetween the path_objects
        for (int k = 1; k < object_count-1; k++) {
            Vector3 prev = path_objects[k-1].transform.position;
            Vector3 pos = path_objects[k].transform.position;

            Vector3 toAdd = (pos-prev).normalized*8;
            // set starting point
            Vector3 current_point_between_objects = prev;
            while (Vector3.Distance(prev, pos) > Vector3.Distance(prev, current_point_between_objects)) {
                path_points_list.Add(current_point_between_objects);
                // next point
                current_point_between_objects += toAdd;

                // remember to which gameobject this point belongs
                path_pointBelongsTo_list.Add(path_objects[k - 1]);
            }
        }

        path_points = new Vector3[path_points_list.Count];
        path_pointBelongsTo = new GameObject[path_points_list.Count];
        // convert list to array
        for(int j=0; j<path_points_list.Count-1;j++){
            path_points[j] = path_points_list[j];
            path_pointBelongsTo[j] = path_pointBelongsTo_list[j];
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color32(255, 41, 212, 255);
        if (path_objects == null) {
            collectPathObjects();
        }

        for (int k = 1; k < path_objects.Length - 1; k++) {
            Vector3 prev = path_objects[k - 1].transform.position;
            Vector3 pos = path_objects[k].transform.position;

            Gizmos.DrawLine(prev, pos);
        }

        // draw points on path
        if (showPoints && path_points != null) {
            for (int k = 0; k < path_points.Length - 1; k++) {
                Gizmos.DrawCube(path_points[k], new Vector3(1, 1, 1));
            }
        }
    }
}
