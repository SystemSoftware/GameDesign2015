using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class T4Path : MonoBehaviour {

    private bool ship_init = false;
    private bool gizmo_init = false;
    private bool cur_dist_alrcalc = false;
    private GameObject ship;
    private Rigidbody rb;
    private Text ui_debug;

    Color rayColor = new Color32(255, 41, 212, 255);
    Color circleColor = new Color32(0, 253, 226, 255);
    List<Transform> path;
    List<Vector3> portal_point;

    int radius = 80;
    float cur_dist = 0;

    // gravity vars
    private int portal = 0;
    private int ptli = 0;
    private Vector3 pt_a;

	// Use this for initialization
	void Start () {

        initGizmos();
        gizmo_init = true;
        ui_debug = GameObject.Find("DebugInfo").GetComponent<Text>();
        
        /*
        Vector3 a = new Vector3(0,0,0);
        Vector3 b = new Vector3(0, 10, 0);
        Vector3 c = new Vector3(0, 5, 6);
        float rad = 5;
        float length = 10;
        Debug.Log("point is " + insideArbitrarilyOrientedCylinder(a, b, Mathf.Pow(length, 2), Mathf.Pow(rad, 2), c));
         * */
        pt_a = new Vector3(0, 0, 0);

	}
	
	// Update is called once per frame
	void Update () {
        if (!ship_init) {
            if (Level.ActiveShips.Length > 0) {
                ship = Level.ActiveShips[0].gameObject;
                rb = ship.GetComponent<Rigidbody>();
                ship_init = true;
            }
        }

        ui_debug.text = ("CurDist=" + cur_dist + "      Port_i=" + ptli);
	}

    void FixedUpdate() {
        

        cur_dist_alrcalc = false;

        if (ship_init && gizmo_init) {
            //if (ship.gameObject.GetComponent<T4ShipPositioned>().positioned) {
            if (Level.AllowMotion) {
                // Calculcate the closest point on the path
                // closer to next point than to current one
                if (ptli+1<=(portal_point.Count-1)) {
                    //Debug.Log("##################################################");
                    cur_dist = Vector3.Distance(portal_point[ptli], ship.transform.position);
                    float next_dist = Vector3.Distance(portal_point[ptli+1], ship.transform.position);
                    cur_dist_alrcalc = true;

                    if (cur_dist > next_dist) {
                        ptli++;
                    }
                }
                // closer to previous point than to current one
                if (ptli - 1 > 0) {
                    if (!cur_dist_alrcalc) {
                        cur_dist = Vector3.Distance(portal_point[ptli], ship.transform.position);
                    }
                    float prev_dist = Vector3.Distance(portal_point[ptli - 1], ship.transform.position);

                    if (cur_dist > prev_dist) {
                        ptli--;
                    }
                }

                // apply force
                float perc_dist = cur_dist / radius;
                // is the ship in the inner 60% around the actual portalpoint?
                if (perc_dist <= 0.6f) { return; }

                // calc the impact 0 - 100% of the force that should be applied
                float impact_factor = (perc_dist - 0.6f) / 0.4f; // 0 - 0.4
                Vector3 push_f = ((portal_point[ptli] - ship.transform.position).normalized * 22500) * impact_factor;
                //push_f.z = 0;
                //float dx = distance;
                rb.AddForce(push_f);
            }
        }
    }


    void OnDrawGizmos() {
        Gizmos.color = new Color32(255,255,255,255);
        if ((ship_init) && (gizmo_init) && portal_point[ptli] != null && ship.transform.position != null) {
            Gizmos.DrawLine(portal_point[ptli], ship.transform.position);
        }

        if (Application.isEditor) { initGizmos(); }
            // iterate over the pathobjects
            for (int i = 0; i < path.Count; i++) {
                Gizmos.color = rayColor;
                Vector3 pos = path[i].position;
                //float path_intervals = 6;//+1
                Vector3 cubep = new Vector3(pos.x, pos.y, pos.z);
                if (i > 0) {
                    Vector3 prev = path[i - 1].position;
                    Gizmos.DrawLine(prev, pos);
                    /*
                    for (int k = 0; k < portal_point.Count; k++) {
                        Gizmos.DrawWireSphere(portal_point[k], 2f);
                    }
                     */
                } else {
                    // draw the first cube on the path
                    Gizmos.DrawCube(cubep, new Vector3(1, 1, 1));
                }

                /*
                // draw circle around it
                Gizmos.color = circleColor;
                float t = 0;
                float precision = 8;
                float box_size = 3;
                // calc the points of the circle and draw it with cubes
                for (int j = 0; j <= (precision); j++) {
                    t = (j / precision);
                    float x = Mathf.Lerp(pos.x-radius, pos.x+radius, t);
                    // draw upper circle
                    float y_top = pos.y + Mathf.Sqrt(Mathf.Pow(radius,2) - Mathf.Pow((x - pos.x), 2));
                    Gizmos.DrawCube(new Vector3(x, y_top, pos.z), new Vector3(box_size, box_size, box_size));
                    // draw lower circle
                    float y_bot = pos.y - Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow((x - pos.x), 2));
                    Gizmos.DrawCube(new Vector3(x, y_bot, pos.z), new Vector3(box_size, box_size, box_size));
                }
                */
            }
        
    }



    void initGizmos() {
        float path_intervals = 6;

        // collect the path items
        Transform[] childs = transform.GetComponentsInChildren<Transform>();
        path = new List<Transform>();
        portal_point = new List<Vector3>();
        foreach (Transform c in childs) {
            if (c != transform) {
                path.Add(c);
                if(path.Count>1){
                    // get previous point
                    Vector3 prev = path[path.Count - 2].position;
                    Vector3 pos = c.transform.position;
                    //Debug.Log("prev=" + prev.ToString() + " pos=" + pos.ToString()+"-------------");
                    Vector3 tmp = Vector3.zero;
					/*
                    for (int k = 1; k <= path_intervals; k++) {
                        tmp = new Vector3(Mathf.Lerp(prev.x, pos.x, (k / path_intervals)), 
                                        Mathf.Lerp(prev.y, pos.y, (k / path_intervals)), 
                                        Mathf.Lerp(prev.z, pos.z, (k / path_intervals)));
                        portal_point.Add(tmp);
                    }
                    */
					Vector3 tmp3 = (pos-prev);
					Vector3 toAdd = tmp3.normalized*8;
					int k = 0;
					Vector3 tmp2 = prev;
					while(Vector3.Distance(prev, pos) > Vector3.Distance(prev, tmp2)){
						portal_point.Add (tmp2);
						tmp2 =  tmp2 + toAdd;
					}
                }
                
            }
        }
    }

    // check for an arbitrarily oriented cylinder if a given point is inside or outside the cylinder
    bool insideArbitrarilyOrientedCylinder(Vector3 pt1, Vector3 pt2, float length_squared, float radius_squared, Vector3 pt_check) {
        Vector3 d;  // vector d from pt1 to pt2
        Vector3 pd; // vector pd from pt1 to pt_check
        float dot, dsq;

        // calc d
        d.x = pt2.x - pt1.x;
        d.y = pt2.y - pt1.y;
        d.z = pt2.z - pt1.z;

        // calc pd
        pd.x = pt_check.x - pt1.x;
        pd.y = pt_check.y - pt1.y;
        pd.z = pt_check.z - pt1.z;

        // dotproduct / skalarprodukt
        dot = pd.x * d.x + pd.y * d.y + pd.z * d.z;

        if (dot < 0.0f || dot > length_squared) {
            return false;
        } else { 
            // distance squared to cylinder axis
            dsq =  (pd.x*pd.x + pd.y*pd.y + pd.z*pd.z) - (dot*dot/length_squared);

            if (dsq > radius_squared) {
                return false;
            } else {
                // point pt_check is inside!
                return true;
            }
        }
    }
}
