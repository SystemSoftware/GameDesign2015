using UnityEngine;
using System.Collections;

public class T4BulletSound : MonoBehaviour {
		GameObject soundContainer;
		T4Sound3DLogic soundLogic;
		// Use this for initialization
		void Start () {
			soundContainer = GameObject.Find ("SoundContainer");
			soundLogic=soundContainer.GetComponent<T4Sound3DLogic>();
		}
		
		// Update is called once per frame
		void Update () {
			soundLogic.regulateVolume (transform.position, transform.gameObject.layer);
		}
}
