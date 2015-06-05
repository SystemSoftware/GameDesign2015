using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class T6UIController : MonoBehaviour {

    Controller ship;
    GameObject shipMarker;

	// Use this for initialization
	void Start () {
        
	}

    public void initialize(Controller ship)
    {
        this.ship = ship;
        shipMarker = GameObject.Find("ShipPosition"+ship.ctrlControlIndex);
    }
	// Update is called once per frame
	void Update () {
            drawShipPosition();
	}

    private void drawShipPosition()
    {
        double x = ship.transform.position.z / 309000.0 * gameObject.GetComponent<RectTransform>().rect.width;
        double y = -(ship.transform.position.x / 309000 - 0 * gameObject.GetComponent<RectTransform>().rect.height);
        shipMarker.transform.localPosition = new Vector3((float)x, (float)y, 0);
    }
}
