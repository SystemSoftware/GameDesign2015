using UnityEngine;
using System.Collections;

public class T6PewPewPew : MonoBehaviour {

    bool fired = false;
    bool opened = false;
    T6Controller controller;
    Rigidbody ship;
    float cooldown = 2;

	// Use this for initialization
	void Start () {
        controller = this.GetComponentInParent<T6Controller>();
        ship = this.transform.parent.parent.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Level.AllowMotion) {
        cooldown -= Time.deltaTime;
        Debug.Log(Input.GetButton("Fire1"));
        if (!fired && !opened && cooldown < 0)
        {
            if (Input.GetButton("Fire1"))
            {
                GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                bullet.AddComponent<Rigidbody>();
                bullet.GetComponent<Rigidbody>().mass = 100;
                bullet.transform.position = this.transform.position;
                bullet.GetComponent<Rigidbody>().velocity = ship.velocity * 2;
                bullet.AddComponent<T6BulletPortal>();
                bullet.GetComponent<T6BulletPortal>().controller = controller;
                bullet.GetComponent<T6BulletPortal>().source = this.gameObject;
                bullet.transform.localScale = new Vector3(5, 5, 5);
                fired = true;

            }
        }}
	}
}
