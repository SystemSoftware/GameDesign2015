using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ShipSize : MonoBehaviour {

//    public bool setSize = false;
    public int triangleCount = 0;
    
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

        }
        else
            this.enabled = false;


	}
}
