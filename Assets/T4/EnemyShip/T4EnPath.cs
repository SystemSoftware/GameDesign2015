using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class T4EnPath : MonoBehaviour {
    private GameObject[] path_objects;
    private Vector3[] path_points;
    private int object_count;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject[] getPathObjects() {
        return path_objects;
    }

    public Vector3[] getPathPoints() {
        return path_points;
    }

    public void collectPathObjects() {
        Transform[] childs = transform.GetComponentsInChildren<Transform>();
        path_objects = new GameObject[childs.Length];
        int i = 0;

        // collect all objects who form the path
        foreach (Transform c in childs)
        {
            path_objects[i++] = c.gameObject;
        }
        object_count = i;

        List<Vector3> path_points_list = new List<Vector3>();
        // calc the points inbetween the path_objects
        for (int k = 2; k < object_count; k++)
        {
            Vector3 prev = path_objects[k - 1].transform.position;
            Vector3 pos = path_objects[k].transform.position;

            Vector3 toAdd = (pos - prev).normalized * 40;
            // set starting point
            Vector3 current_point_between_objects = prev;
            while (Vector3.Distance(prev, pos) > Vector3.Distance(prev, current_point_between_objects))
            {
                path_points_list.Add(current_point_between_objects);
                // next point
                current_point_between_objects += toAdd;
            }
        }

        path_points = new Vector3[path_points_list.Count];
        // convert list to array
        for (int j = 0; j < path_points_list.Count; j++) { 
            path_points[j] = path_points_list[j];
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color32(138, 239, 255, 255);
        if (path_objects == null)
        {
            collectPathObjects();
        }

        for (int k = 2; k < path_objects.Length; k++)
        {
            Vector3 prev = path_objects[k - 1].transform.position;
            Vector3 pos = path_objects[k].transform.position;

            Gizmos.DrawLine(prev, pos);
        }
    }
    
}
