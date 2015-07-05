using UnityEngine;
using System.Collections;

public class T6Trajectory : MonoBehaviour {
	// Use this for initialization
    LineRenderer l;
    public Vector3 orbitPane;
	void Start () {
	    
	}

    void OnEnable()
    {
        l = this.GetComponent<T6ViewController>().getLineRenderer();
    }
	
	// Update is called once per frame
	void Update () {
        if (Level.AllowMotion)
        {
            this.enabled = true;
        }
        else { this.enabled = false; }
	}

    void FixedUpdate()
    {
   
        foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
        {
            Vector3 direction = planet.transform.position - this.transform.position;
            long r = (long)direction.magnitude;
            direction.Normalize();
            double G = 6.674;
            Vector3 gravity = direction * (float)(G * this.GetComponent<Rigidbody>().mass * planet.GetComponent<Rigidbody>().mass / (r * r));

            this.GetComponent<Rigidbody>().AddForce(gravity);
        }
       var points =  calculateTrajectory(100);
       drawTrajectory(points);
       
    }

    Vector3[] calculateTrajectory(int iterations)
    {
        //initialize output
        Vector3[] points = new Vector3[iterations];
        //Raw data
        Vector3 velocity = this.GetComponent<Rigidbody>().velocity;
        points[0] = this.transform.position;
        for (int i = 1; i < iterations; i++)
        {
            
            foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
            {
                Vector3 direction = planet.transform.position - points[i - 1];
                long r = (long)direction.magnitude;
                direction.Normalize();
                double G = 6.674;
                Vector3 grav = direction * (float)(G * 100 * planet.GetComponent<Rigidbody>().mass / (r * r));
                velocity += grav * 0.01f;
                
            }
            points[i] = points[i - 1] + velocity;

        }
        orbitPane = Vector3.Cross(points[0] - points[iterations / 2], points[iterations / 2] - points[iterations - 1]);
        return points;

    }

    void drawTrajectory(Vector3[] points)
    {
        l.SetColors(Color.red, Color.red);
        l.SetVertexCount(points.Length);
        l.SetWidth(100, 100);
        l.sortingLayerID = 5;
        for (int i = 0; i < points.Length; i++)
        {
            l.SetPosition(i, points[i]);
        }
    }
}
