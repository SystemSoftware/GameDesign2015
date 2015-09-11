using UnityEngine;
using System.Collections;

public class T4Sound3DLogic : MonoBehaviour {
	bool ship_init=false;
	int numberShips;
	Rigidbody[] rb;
	float rocket_cur_highest;
	public AudioSource rocket;
	AudioSource mainTheme;
	AudioSource bossTheme;
	AudioSource countDownBeep;
	AudioSource startBeep;
	AudioSource powerUp;
	AudioSource playerShoot;
	// Use this for initialization
	void Start () {
		AudioSource[] audios = GetComponents<AudioSource> ();
		mainTheme = audios [1];
		countDownBeep = audios [2];
		startBeep = audios [3];
		bossTheme = audios [4];
		powerUp = audios [5];
		playerShoot = audios [6];
	}
	
	// Update is called once per frame
	void Update () {
		if (!ship_init) {
			numberShips = Level.ActiveShips.Length;
			rb = new Rigidbody[numberShips];
			for(int i = 0; i< numberShips; i++) {
				rb[i] = Level.ActiveShips[i].gameObject.GetComponent<Rigidbody>();
				ship_init = true;
			}
			Debug.Log ("Ships initiated and numberShips = " + numberShips, transform.gameObject);
		}
	}

	void LateUpdate() {
		if (rocket_cur_highest > 0) {
			rocket.volume = rocket_cur_highest;
			rocket_cur_highest = 0;
		}
	}

	public void regulateVolume(/*AudioSource audio,*/ Vector3 pos){
		if (ship_init) {
			float distance = ComputeDistance (pos);
			float volume;
			if (distance<=500 && distance>1){ 
				volume = 1f-distance/500f;
			}else if(distance <=1){
				volume = 1;
			}else{
				volume = 0;
			}
			if (volume>rocket_cur_highest)rocket_cur_highest=volume;
		}
	}

	public void playMainTheme(){
		mainTheme.Play ();
	}

	public void stopMainTheme(){
		if(mainTheme.isPlaying){
			mainTheme.Stop();
		}
	}

	public void playBossTheme(){
		bossTheme.Play ();
	}

	public void playPowerUp(){
		powerUp.Play ();
	}

	public void stopBossTheme(){
		if(bossTheme.isPlaying){
			bossTheme.Stop();
		}
	}

	public void playPlayerShoot(){
		playerShoot.Play ();
	}

	public void playCountDownBeep(){
		countDownBeep.Play ();
	}
	public void playStartBeep(){
		startBeep.Play ();
	}

	float ComputeDistance(Vector3 pos){
		float distance = 100000;
		float tmpDistance;

		foreach (Rigidbody rigid in rb) {
			tmpDistance=Vector3.Distance(rigid.transform.position, pos);
			if(tmpDistance < distance)
				distance= tmpDistance;
		}
		return distance;
	}
}
