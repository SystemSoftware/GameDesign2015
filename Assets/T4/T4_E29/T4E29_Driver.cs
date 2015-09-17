using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class T4E29_Driver : MonoBehaviour {
	
	// input axis
	private float horizontal = 0f;
	private float vertical = 0f;
    private float accl = 0f;

    T4E29_Controller ctrl;

    private Rigidbody ship,
        left_wing_engine,
        right_wing_engine,
        back_engine,
        upper_engine,
        front_engine;

    // effect objects
    private Light back_spotleft, back_spotright;
    private ParticleSystem back_psleft, back_psright;


	// Use this for initialization
	void Start () {

		// get engines
		Rigidbody[] engines = this.GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody r in engines) {
			if(r.name == "LeftThruster"){        left_wing_engine = r;   }
			if(r.name == "RightThruster"){       right_wing_engine = r;  }
			if(r.name == "BackThruster"){         
                back_engine = r;
                // collec the spotlights and deactivate them initally
                Light[] ls = r.GetComponentsInChildren<Light>();
                foreach (Light s in ls){
                    if (s.name == "SpotLeft") { back_spotleft = s; back_spotleft.intensity = 0; }
                    if (s.name == "SpotRight") { back_spotright = s; back_spotright.intensity = 0; }
                }
                // collect the particlesys and deactivate them initally
                ParticleSystem[] ps = r.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem p in ps) {
                    if (p.name == "PSLeft") { back_psleft = p; back_psleft.enableEmission = false; }
                    if (p.name == "PSRight") { back_psright = p; back_psright.enableEmission = false; }
                }
            }
			if (r.name == "BottomThruster"){ front_engine = r;       }
            if (r.name == "TopThruster"){       upper_engine = r;       }
		}


		ship = this.GetComponent<Rigidbody> ();
        ship.angularDrag = 55.8f;
        ship.centerOfMass = new Vector3(0, 0, 0);
        old_pos = front_engine.position;

        ctrl = this.GetComponent<T4E29_Controller>();
	}

    Vector3 old_pos = new Vector3();
    void FixedUpdate(){
        if ((Level.AllowMotion) && (ship.useGravity)) {
            // BACK THRUSTER
            float forward_thrust = 7000;
            accl = Input.GetAxis(ctrl.ctrlAxisAccelerate);
            Vector3 acc_force = Vector3.forward * forward_thrust * accl * Time.deltaTime;
            back_engine.AddRelativeForce(acc_force, ForceMode.Impulse);
            // turn on the spotlights
            back_spotleft.intensity = 8 * accl;
            back_spotright.intensity = 8 * accl;
            // turn on particlesystems
            back_psleft.enableEmission = (accl > 0) ? true : false;
            back_psright.enableEmission = (accl > 0) ? true : false;

            // WING THRUSTERS
            horizontal = Input.GetAxis(ctrl.ctrlAxisHorizontal);
            float wing_thrust = 150;
            float angle = Vector3.Angle(Vector3.up, transform.up);
            if (horizontal < 0) {// fly right
                left_wing_engine.AddRelativeForce(-Vector3.up * wing_thrust, ForceMode.Force);
                right_wing_engine.AddRelativeForce(Vector3.up * wing_thrust, ForceMode.Force);
            } else if (horizontal > 0) // fly left
        {
                left_wing_engine.AddRelativeForce(Vector3.up * wing_thrust, ForceMode.Force);
                right_wing_engine.AddRelativeForce(-Vector3.up * wing_thrust, ForceMode.Force);
            }
            //ship.velocity = Vector3.Lerp(ship.velocity, Vector3.zero, Time.fixedDeltaTime);

            // BOTTOM / TOP THRUSTERS
            float updown_thrust = 200;
            vertical = Input.GetAxis(ctrl.ctrlAxisVertical);
            if (vertical < 0) {
                front_engine.AddRelativeForce(Vector3.up * updown_thrust, ForceMode.Force);
            } else if (vertical > 0) {
                front_engine.AddRelativeForce(-Vector3.up * updown_thrust, ForceMode.Force);
            }
        }
    }


}