using UnityEngine;
using System.Collections;
using UnityStandardAssets.Water;

public class T4CullingMask : MonoBehaviour {
    private Controller ctrl;

	// Use this for initialization
	void Start () {
        ctrl = this.GetComponent<Controller>();

        // calc culling mask
        int basic_cullm = 0;
        int cull_mask = 0; //ctrl.ctrlAttachedCamera.cullingMask

        // activate the first 27 layers for the ship
        for (int i = 0; i <= 27; i++) {
            cull_mask |= 1 << i;
        }
        basic_cullm = cull_mask;
        // add player world
        cull_mask |= 1 << (28 + ctrl.ctrlControlIndex);
        
        // apply culling mask for camera and water
        ctrl.ctrlAttachedCamera.cullingMask = cull_mask;
        if (GameObject.Find("Ocean") != null) {
            foreach (Transform child in GameObject.Find("Ocean").transform) {
                child.GetComponent<Water>().reflectLayers = basic_cullm;
                child.GetComponent<Water>().refractLayers = basic_cullm;
            }
        }
	}
}
