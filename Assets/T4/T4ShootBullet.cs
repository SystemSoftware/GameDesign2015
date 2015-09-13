using UnityEngine;
using System.Collections;

public class T4ShootBullet : MonoBehaviour {

	public GameObject bullet;
	public float velocity=200f;
	protected string fire;
	private bool firePressed = false;
	private bool allowfire = true;
	T4Sound3DLogic soundLogic;
    public int shotsLeft = 0;

    private T4GUIShotHandler shot_handler;
    private float nextBulletRespawn = 0.0f, nextBulletCanBeShotIn = 0.0f;
    // 1f = 1sec
    public float bulletRespawnCD = 1.0f, bulletShotCD = 0.5f;

	
	//load the prefab of the bullet
	void Start() {
		bullet = Resources.Load ("LaserBullet")as GameObject;
		GameObject soundContainer = GameObject.Find ("SoundContainer");
		soundLogic=soundContainer.GetComponent<T4Sound3DLogic>();


        shot_handler = gameObject.GetComponent<T4GUIShotHandler>();
	}
	
	void Update(){		
		if (fire == null) {
			fire = FetchFire0 ();
		}		
		if (fire != null) {
			firePressed=0!=Input.GetAxis(fire);
		}	
	
        // give players new bullet cooldowns
        if (Time.time > nextBulletRespawn) {
            nextBulletRespawn += bulletRespawnCD;
            shot_handler.addShot();
        }
	}
	
	
	//Get the Firekey of the Controller
	protected virtual string FetchFire(Controller controller)
	{
		return controller.ctrlAxisFire;
	}
	
	private string FetchFire0()
	{
		Controller ctrl = GetComponentInParent<Controller>();
		if (ctrl != null)
			return FetchFire(ctrl);
		return null;
	}
	
	//Fires Bullet
	//IEnumerator Fire(){
    void  Fire(){
		//make a clone of the bullet prefab which will then get shot
		GameObject bulletClone = Instantiate (bullet, transform.position, transform.rotation)as GameObject;
		bulletClone.transform.Rotate (0, -90, 90);
		//set the parent of the clone to the player, so that he can get attributed if he hits a target
		bulletClone.transform.parent = transform;


		//shoot the bullet
		bulletClone.GetComponent<Rigidbody>().AddForce (transform.forward * velocity, ForceMode.VelocityChange);
		//yield return new WaitForSeconds(1);//limit the firerate
		//allowfire = true;//allow the next shot to be fired
	}
	
	void FixedUpdate(){
        if (Time.time > nextBulletCanBeShotIn) {
            nextBulletCanBeShotIn += bulletShotCD;
            allowfire = true;
        }

		//only fire if fire-button pressed and firerate allows for next shot
        if (firePressed && allowfire && shot_handler.getShotsLeft() > 0) {
            shot_handler.removeShot();
			soundLogic.playPlayerShoot();
			allowfire = false; //disable shooting for a set period
			firePressed = false;
			//StartCoroutine(Fire());//shoot the laser
            Fire();
		}
		
	}

}
