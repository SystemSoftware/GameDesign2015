using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ShipBounds : MonoBehaviour {

    public bool setSize = false;
    public Vector3 size;
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
            Bounds boundingBox;
            boundingBox = new Bounds();
            {
                Renderer m = GetComponent<Renderer>();
                if (m != null)
                {

                    boundingBox.Encapsulate(m.bounds);
                }
            }


            foreach (Renderer m in GetComponentsInChildren<Renderer>())
            {
                boundingBox.Encapsulate(m.bounds);
            }

            Vector3 currentSize = boundingBox.max - boundingBox.min;
            if (setSize && applySize)
            {
                Vector3 fc = new Vector3(
                                    size.x / currentSize.x,
                                    size.y / currentSize.y,
                                    size.z / currentSize.z);
                float largest = Mathf.Max(Mathf.Max(fc.x, fc.y), fc.z);
                if (largest > 0f)
                {
                    this.transform.localScale *= largest;
                }
                setSize = false;
                applySize = false;
                Update();
            }
            else if (!setSize)
                size = currentSize;

        }
        else
            this.enabled = false;


	}
}
