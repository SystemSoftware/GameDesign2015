using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class T6UIController : MonoBehaviour {

    Transform ship;
    public Controller controller;
    GameObject shipMarker;

    public T6UIController(Controller shipController)
    {
        this.controller = shipController;
    }

	// Use this for initialization
	void Start () {
        shipMarker = GameObject.Find(this.name+ "/ShipPosition");
        ship = controller.transform;
	}
	
	// Update is called once per frame
	void Update () {
            drawShipPosition();
	}

    private void drawShipPosition()
    {
        double x = ship.position.z / 309000.0 * gameObject.GetComponent<RectTransform>().rect.width;
        double y = -(ship.position.x / 309000 - 0 * gameObject.GetComponent<RectTransform>().rect.height);
        shipMarker.transform.localPosition = new Vector3((float)x, (float)y, 0);
    }
}
