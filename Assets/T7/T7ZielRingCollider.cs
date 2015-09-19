using UnityEngine;
using System.Collections;

public class T7ZielRingCollider : MonoBehaviour
{
    public int ringNr;
    public T7Level levelLogic;
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.transform.root.name.StartsWith("Team"))
        {
            levelLogic.ringVisited(ringNr, col.gameObject.transform.root.gameObject);
        }
    }
}