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
	AudioSource explosion;
	AudioSource turretShoot;
	AudioSource enemyPlaneShoot;
	AudioSource explosionBullet;
    AudioSource bossHit;
    AudioSource endTheme;
    AudioSource bossAttack;

    private bool bossThemeAlreadyPlayed = false;
	// Use this for initialization
	void Start () {
		AudioSource[] audios = GetComponents<AudioSource> ();
		mainTheme = audios [1];
		countDownBeep = audios [2];
		startBeep = audios [3];
		bossTheme = audios [4];
		powerUp = audios [5];
		playerShoot = audios [6];
		explosion = audios [7];
		turretShoot = audios [8];
		enemyPlaneShoot = audios [9];
		explosionBullet = audios [10];
        bossHit = audios[11];
        endTheme = audios[12];
        bossAttack = audios[13];
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
		} else {
			rocket.volume=0;
		}
	}

	public void regulateVolume(/*AudioSource audio,*/ Vector3 pos, int tag){
		if (ship_init) {
			float distance = ComputeDistance (pos, tag);
			float volume;
			if (distance<=250 && distance>1){ 
				volume = 1f-distance/250f;
			}else if(distance <=1){
				volume = 1;
			}else{
				volume = 0;
			}
			if (volume>rocket_cur_highest)rocket_cur_highest=volume;
		}
	}

	public void playTurretShoot(Vector3 pos1, Vector3 pos2){
		float distance=Vector3.Distance(pos1, pos2);
		if (distance <= 400 && distance > 1) {
			turretShoot.volume = 1f - distance / 400f;
			turretShoot.Play ();
		}else if(distance <=1){
				turretShoot.volume=1;
				turretShoot.Play ();
		}
	}

	public void playEnemyPlaneShoot(Vector3 pos1, Vector3 pos2){
		float distance=Vector3.Distance(pos1, pos2);
		if (distance <= 1000 && distance > 1) {
			enemyPlaneShoot.volume = 1f - distance / 1000f;
			enemyPlaneShoot.Play ();
		}else if(distance <=1){
			enemyPlaneShoot.volume=1;
			enemyPlaneShoot.Play ();
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
        if (!bossThemeAlreadyPlayed) {
            bossThemeAlreadyPlayed = true;
            // boss theme not playing already?
            // stop main theme
            stopMainTheme();
            // play boss theme
            bossTheme.Play();
        }
	}

	public void playPowerUp(){
		powerUp.Play ();
	}

	public void playExplosion(){
		explosion.Play ();
	}

	public void playExplosionBullet(){
		explosionBullet.Play ();
	}

    public void playBossHit() {
        bossHit.Play();
    }

    public void playEndTheme() {
        endTheme.Play();
    }

    public void playBossAttack() {
        bossAttack.Play();
    }

	public void stopBossTheme(){
		if(bossTheme.isPlaying){
			bossTheme.Stop();
		}
	}

	public void playPlayerShoot(){
        //Debug.Log("ABOUT TO PLAY PLAYERSHOOT");
		playerShoot.Play ();
	}

	public void playCountDownBeep(){
		countDownBeep.Play ();
	}
	public void playStartBeep(){
		startBeep.Play ();
	}

	float ComputeDistance(Vector3 pos, int tag){
		float distance = 100000;
		float tmpDistance;
		int i = 0;

		foreach (Rigidbody rigid in rb) {
			if(Level.ActiveShips[i].gameObject.layer==tag && rigid != null){
				tmpDistance=Vector3.Distance(rigid.transform.position, pos);
				if(tmpDistance < distance)
					distance= tmpDistance;
			}
			i++;
		}	
		return distance;
	}
}
