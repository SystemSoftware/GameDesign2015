using UnityEngine;
using System.Collections;

public class T1SpawnAsteroids : MonoBehaviour {

	public GameObject asteroid;

	// Use this for initialization
	void Start () {
//		Random random;

		var super = new GameObject();

		for (int i = 0; i < 2000; i++)
		{
			
			((GameObject)Instantiate(asteroid, Random.insideUnitSphere * 10000.0f, Random.rotationUniform)).transform.parent = super.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
