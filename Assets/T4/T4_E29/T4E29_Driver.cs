using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class T4E29_Driver : MonoBehaviour {
	public float MovementSpeed = 1.0f;
	public int invert = 1; // 1 not inverted, -1 inverted
	
	// input axis
	private float horizontal = 0f;
	private float vertical = 0f;

	// path vars
	private int pi = 0; // path index
	private List<Transform> path;
	private Vector3 tmp_vec;

	// force
    float thrust = 420000f;
    float v_thrust = 250000f;
    float h_thrust = 120000f;

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


        float accl = Input.GetAxis(ctrl.ctrlAxisAccelerate);
        Debug.Log(accl);


        // BACK THRUSTER
        Vector3 acc_force = Vector3.forward * 5555 * accl * Time.deltaTime;
        back_engine.AddRelativeForce(acc_force, ForceMode.Impulse);
        // turn on the spotlights
        back_spotleft.intensity = 8 * accl;
        back_spotright.intensity = 8 * accl;
        // turn on particlesystems
        back_psleft.enableEmission = (accl > 0) ? true : false;
        back_psright.enableEmission = (accl > 0) ? true : false;

        // WING THRUSTERS
        horizontal = Input.GetAxis(ctrl.ctrlAxisHorizontal);
        float wing_thrust = 300;
        float angle = Vector3.Angle(Vector3.up, transform.up);
        Debug.Log("angle=" + angle + " ship" + transform.rotation);
        if (horizontal < 0) {// fly right
            left_wing_engine.AddRelativeForce(-Vector3.up * wing_thrust, ForceMode.Force);
            right_wing_engine.AddRelativeForce(Vector3.up * wing_thrust, ForceMode.Force);
        } else if (horizontal > 0) // fly left
        {
            left_wing_engine.AddRelativeForce(Vector3.up * wing_thrust, ForceMode.Force);
            right_wing_engine.AddRelativeForce(-Vector3.up * wing_thrust, ForceMode.Force);
        }
        ship.velocity = Vector3.Lerp(ship.velocity, Vector3.zero, Time.fixedDeltaTime);

        // BOTTOM / TOP THRUSTERS
        float updown_thrust = 500;
        vertical = Input.GetAxis(ctrl.ctrlAxisVertical);
        if (vertical < 0) {
            front_engine.AddRelativeForce(Vector3.up * updown_thrust, ForceMode.Force);
        } else if (vertical > 0) {
            front_engine.AddRelativeForce(-Vector3.up * updown_thrust, ForceMode.Force);
        }
        /*
        horizontal = Input.GetAxis("Horizontal0");
        vertical = Input.GetAxis("Vertical0");
        float fire1 = Input.GetAxis("Fire1");
        //Debug.Log("H=" + horizontal + " V=" + vertical + " Jump=" + fire1);
        // back thruster
        

        float speed = Mathf.Sqrt(Mathf.Pow(front_engine.position.x - old_pos.x, 2) + 
            Mathf.Pow(front_engine.position.y - old_pos.y, 2) + 
            Mathf.Pow(front_engine.position.z - old_pos.z, 2));

        Vector3 acc_force = Vector3.forward * thrust * fire1 * Time.deltaTime;
        //Vector3 acc_force = (front_push.position - back_engine.position) * thrust * fire1 * Time.deltaTime;
        old_pos = front_engine.position;

        /**
         *  ------ BACK THRUSTER
         
        back_engine.AddRelativeForce(acc_force, ForceMode.Impulse);
        // turn on the spotlights
        back_spotleft.intensity = 8 * fire1;
        back_spotright.intensity = 8 * fire1;
        // turn on particlesystems
        back_psleft.enableEmission = (fire1>0)?true:false;
        back_psright.enableEmission = (fire1>0)?true:false;
        
        //Debug.Log(vertical);
        //left_wing_engine.AddForce(transform.up * thrust, ForceMode.Force);

        
        // wing engines

        float angle = Vector3.Angle(Vector3.up, transform.up);
        Debug.Log("angle=" + angle+" ship"+transform.rotation);
        if (horizontal > 0){// && angle < 85) || (horizontal > 0 && (left_wing_engine.position.z<right_wing_engine.position.z))) {
            // left
            left_wing_engine.AddRelativeForce(-Vector3.up * h_thrust, ForceMode.Force);
            right_wing_engine.AddRelativeForce(Vector3.up * h_thrust, ForceMode.Force);
        }
        else if (horizontal < 0) //&& angle < 85) || (horizontal > 0 && (left_wing_engine.position.z > right_wing_engine.position.z)))
        {
            // right
            left_wing_engine.AddRelativeForce(Vector3.up * h_thrust, ForceMode.Force);
            right_wing_engine.AddRelativeForce(-Vector3.up * h_thrust, ForceMode.Force);
        }
        ship.velocity = Vector3.Lerp(ship.velocity, Vector3.zero, Time.fixedDeltaTime);
        
        // seems fine
        
        if (vertical > 0) {
            // nose down
            front_engine.AddRelativeForce(Vector3.up* v_thrust, ForceMode.Force);
        }
        else if (vertical < 0) { 
            // nose up
            //Debug.Log("rais nose");
            front_engine.AddRelativeForce(-Vector3.up * v_thrust, ForceMode.Force);
        }
        */
         
    }


}