using UnityEngine;
using System.Collections;
using UnityStandardAssets.Water;

public class T4CullingMask : MonoBehaviour {
    private Controller ctrl;

	// Use this for initialization
	void Start () {
        ctrl = this.GetComponent<Controller>();

        // calc culling mask
        int cull_mask = 0; //ctrl.ctrlAttachedCamera.cullingMask
        // activate the first 27 layers for the ship
        for (int i = 0; i <= 27; i++) {
            cull_mask |= 1 << i;
        }
        // add player world
        cull_mask |= 1 << (28 + ctrl.ctrlControlIndex);
        
        // apply culling mask for camera and water
        ctrl.ctrlAttachedCamera.cullingMask = cull_mask;
        foreach (Transform child in GameObject.Find("Ocean").transform) {
            child.GetComponent<Water>().reflectLayers = cull_mask;
            child.GetComponent<Water>().refractLayers = cull_mask;
        }

	}
}
