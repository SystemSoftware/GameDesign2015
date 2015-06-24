using UnityEngine;
using System.Collections;

public class T6GenerateThrustRotation : MonoBehaviour {

    public static bool done = false;
    public float count = 12;
    public float radius = 12.2f;
    public float offset = 90;
    Vector3 p; //Pivot value -1..1, calculated from Mesh bounds
    Vector3 last_p; //Last used pivot

    GameObject obj; //Selected object in the Hierarchy
    MeshFilter meshFilter; //Mesh Filter of the selected object
    Mesh mesh; //Mesh of the selected object
    Collider col; //Collider of the selected object

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!done) {
			done=true;
            Transform ship = this.transform.parent;
            Transform shipOld = ship;
            ship.position = new Vector3(0, 0, 0);
            ship.rotation = Quaternion.Euler(0, 0, 0);

            RecognizeSelectedObject(this.gameObject);
            p.Set(0, 0, 1);
            UpdatePivot();
            this.gameObject.AddComponent<T6RotateThrustFlaps>();
            GameObject.Destroy(this.gameObject);

			for(int i=0;i<count;i++){
				float alpha = 360f/(float) count * i + offset;
				alpha = Mathf.Deg2Rad * alpha;
				float x = Mathf.Cos (alpha) * radius * ship.localScale.x;
				float y = Mathf.Sin(alpha) * radius * ship.localScale.x;
                float z = -36f * ship.localScale.x;
                GameObject thrustFlap = new GameObject("ThrustFlaps" + i.ToString("D2"));
                thrustFlap.AddComponent<MeshRenderer>().sharedMaterials = this.GetComponent<MeshRenderer>().sharedMaterials;
                thrustFlap.AddComponent<MeshFilter>().sharedMesh = this.GetComponent<MeshFilter>().sharedMesh;
                thrustFlap.AddComponent<MeshCollider>().convex = true; //Kollisionsboxen verschieben CoM                
                thrustFlap.transform.parent = this.transform.parent;
                thrustFlap.transform.position = new Vector3(x, y, z);
                float tmp = Mathf.Sqrt(ship.localScale.x)*1.2f;
                thrustFlap.transform.localScale = new Vector3(tmp, tmp, tmp);
                thrustFlap.transform.Rotate(new Vector3(0, 0, 1), 360f / (float)count * i);
                thrustFlap.AddComponent<T6RotateThrustFlaps>();
                
			}
            ship.position = shipOld.position;
            ship.rotation = shipOld.rotation;
		}

	}

    //Gather references for the selected object and its components
    //and update the pivot vector if the object has a Mesh specified
    void RecognizeSelectedObject(GameObject o)
    {
        Transform t = o.transform;
        obj = t ? t.gameObject : null;
        if (obj)
        {
            meshFilter = obj.GetComponent(typeof(MeshFilter)) as MeshFilter;
            mesh = meshFilter ? meshFilter.sharedMesh : null;
            if (mesh)
                UpdatePivotVector();
            col = obj.GetComponent(typeof(Collider)) as Collider;
        }
        else
        {
            mesh = null;
        }
    }

    void UpdatePivotVector()
    {
        Bounds b = mesh.bounds;
        Vector3 offset = -1 * b.center;
        p = last_p = new Vector3(offset.x / b.extents.x, offset.y / b.extents.y, offset.z / b.extents.z);
    }

    void UpdatePivot()
    {
        Vector3 diff = Vector3.Scale(mesh.bounds.extents, last_p - p); //Calculate difference in 3d position
        obj.transform.position -= Vector3.Scale(diff, obj.transform.localScale); //Move object position by taking localScale into account
        //Iterate over all vertices and move them in the opposite direction of the object position movement
        Vector3[] verts = mesh.vertices;
        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] += diff;
        }
        mesh.vertices = verts; //Assign the vertex array back to the mesh
        mesh.RecalculateBounds(); //Recalculate bounds of the mesh, for the renderer's sake
        //The 'center' parameter of certain colliders needs to be adjusted
        //when the transform position is modified
        if (col)
        {
            if (col is BoxCollider)
            {
                ((BoxCollider)col).center += diff;
            }
            else if (col is CapsuleCollider)
            {
                ((CapsuleCollider)col).center += diff;
            }
            else if (col is SphereCollider)
            {
                ((SphereCollider)col).center += diff;
            }
        }
    }
}
