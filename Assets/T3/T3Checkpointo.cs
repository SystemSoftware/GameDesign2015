using UnityEngine;
using System.Collections;

public class T3Checkpointo : MonoBehaviour {

	// Use this for initialization
    public int id;
    public bool moving;
    public Vector3 target;
    public float speed;
    private Vector3 start;
    private int direction = 1;
	void Start () {
        start = transform.parent.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {

            float step = speed * Time.deltaTime;
            transform.parent.gameObject.transform.position = Vector3.MoveTowards(transform.position, start+direction*target, step);
            Vector3 pos = transform.parent.gameObject.transform.position;
            if (pos == start + direction * target)
            {
                direction *= -1;
            }
        }
	}

    void OnTriggerEnter(Collider Other)
    {
        GameObject o = Other.transform.parent.gameObject;
        while (o.GetComponent<T3Player>() == null && o.transform.parent != null)
        {
            o = o.transform.parent.gameObject;
        }
        if (o.GetComponent<T3Player>() != null)
        {
            T3Player p = o.GetComponent<T3Player>();
            if (p.checkpoint == id - 1)
            {
                p.checkpoint++;
            }
        }
    }
}
