using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ShipSize : MonoBehaviour {

//    public bool setSize = false;
    public int triangleCount = 0;
    public bool showCentersOfGravity = true;

    public Vector3 currentSize;
    public Vector3 wantSize;
    public bool applySize = false;


	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
        //counter++;
        //if (counter >= 10)
        if (!Application.isPlaying)
        {

            triangleCount = 0;
            foreach (MeshFilter filter in GetComponentsInChildren<MeshFilter>())
            {
                triangleCount += filter.sharedMesh.triangles.Length / 3;
            }




            Bounds boundingBox;
            boundingBox = new Bounds();
            bool any = false;
            {
                Renderer m = GetComponent<Renderer>();
                if (m != null)
                {
                    boundingBox = m.bounds;
                    any = true;
                }
            }


            foreach (Renderer m in GetComponentsInChildren<Renderer>())
            {
                if (!any)
                {
                    boundingBox = m.bounds;
                    any = true;
                }
                else
                    boundingBox.Encapsulate(m.bounds);
            }

            currentSize = boundingBox.max - boundingBox.min;



            if (applySize)
            {
                Vector3 fc = new Vector3(
                                    wantSize.x / currentSize.x,
                                    wantSize.y / currentSize.y,
                                    wantSize.z / currentSize.z);
                float largest = Mathf.Max(Mathf.Max(fc.x, fc.y), fc.z);
                if (largest > 0f)
                {
                    this.transform.localScale *= largest;
                }
                applySize = false;
                wantSize = Vector3.zero;
                Update();
            }


			//Draw center of mass
            if (showCentersOfGravity)
            {
                foreach (var body in this.GetComponentsInChildren<Rigidbody>())
                {
                    DrawEdge(body.worldCenterOfMass, new Vector3(-1, 0, 0), new Vector3(1, 0, 0));
                    DrawEdge(body.worldCenterOfMass, new Vector3(0, -1, 0), new Vector3(0, 1, 0));
                    DrawEdge(body.worldCenterOfMass, new Vector3(0, 0, -1), new Vector3(0, 0, 1));


                    //Rigidbody body = this.GetComponent<Rigidbody>();
             //       if (body != null)
                    //{
                    //    for (int x = 0; x < 2; x++)
                    //        for (int y = x + 1; y < 3; y++)
                    //        {
                                
                    //            for (int k = 0; k < 4; k++)
                    //            {
                    //                Vector3 v = new Vector3(0,0,0),
                    //                        w = new Vector3(0,0,0);
                    //                v[x] = (k%2)==0? (k/2==0 ? 1 : -1) : 0;
                    //                v[y] = (k % 2) != 0 ? (k / 2 == 0 ? 1 : -1) : 0;
                    //                w[x] = (k % 2) != 0 ? ((k == 0 || k == 3)? 1 : -1) : 0;
                    //                w[y] = (k % 2) == 0 ? ((k == 0 || k == 3) ? 1 : -1) : 0;
                    //                DrawEdge(body.worldCenterOfMass, v, w);
                    //            }
                    //        }

                    //}
                }
            }
        }
        else
            this.enabled = false;


	}

    void DrawEdge(Vector3 c, Vector3 v, Vector3 w)
    {
        Debug.DrawLine(c + v * 10f, c + w * 10f, Color.red);
    }
}
