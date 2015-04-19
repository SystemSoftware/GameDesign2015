using UnityEngine;
using System.Collections;

public class T2SpawnAsteroids : MonoBehaviour {

	public GameObject asteroid;

	// Use this for initialization
	void Start () {
//		Random random;

		for (int i = 0; i < 1000; i++)
		{
			Instantiate(asteroid, Random.insideUnitSphere * 10000.0f, Random.rotationUniform);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
