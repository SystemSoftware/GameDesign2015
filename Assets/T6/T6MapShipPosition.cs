using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class T6MapShipPosition : MonoBehaviour {


    public Transform ship;
	// Use this for initialization
	void Start () {
        Debug.Log(GetComponentInParent<RectTransform>().rect.width);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
