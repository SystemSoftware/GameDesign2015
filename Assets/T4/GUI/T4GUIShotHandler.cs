using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class T4GUIShotHandler : MonoBehaviour {
    private Maximize m;
    private Controller ctrl;
    private GameObject shotA, shotB, shotC, shotD;
    private int shots_left;
    private Sprite full, empty;

    Vector2 fullscr_anchor, split_anchor;

	// Use this for initialization
	void Start () {
	    m = GameObject.Find("GameLogic").GetComponent<Maximize>();
        ctrl = this.GetComponent<Controller>();

        full = Resources.Load<Sprite>("Sprites/shot_icon");
        empty = Resources.Load<Sprite>("Sprites/shotempty_icon");
        // get the shots for each of the elements
        shotA = GameObject.Find("Shot" + ctrl.ctrlControlIndex).transform.Find("ShotA").gameObject;
        shotB = GameObject.Find("Shot" + ctrl.ctrlControlIndex).transform.Find("ShotB").gameObject;
        shotC = GameObject.Find("Shot" + ctrl.ctrlControlIndex).transform.Find("ShotC").gameObject;
        shotD = GameObject.Find("Shot" + ctrl.ctrlControlIndex).transform.Find("ShotD").gameObject;
        // set size
        shotA.GetComponent<RectTransform>().sizeDelta = new Vector2(17,33);
        shotB.GetComponent<RectTransform>().sizeDelta = new Vector2(17,33);
        shotC.GetComponent<RectTransform>().sizeDelta = new Vector2(17,33);
        shotD.GetComponent<RectTransform>().sizeDelta = new Vector2(17,33);
        // make the sprites visible
        shotA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        shotB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        shotC.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        shotD.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        // set inital sprites
        shotA.GetComponent<Image>().sprite = full;
        shotB.GetComponent<Image>().sprite = full;
        shotC.GetComponent<Image>().sprite = full;
        shotD.GetComponent<Image>().sprite = full;

        shots_left = 2;
        float cam_width = Screen.width / 2;
        float cam_height = Screen.height / 2;
        switch (ctrl.ctrlControlIndex) {
            case 0: // player 1
                split_anchor = new Vector2(cam_width-105, Screen.height - cam_height * 0.95f);
                fullscr_anchor = new Vector2(Screen.width-205, cam_height - cam_height * 0.92f);
                break;
            case 1: // player 2
                split_anchor = new Vector2(Screen.width - 105, Screen.height - cam_height * 0.95f);

                break;
            case 2: // player 3
                split_anchor = new Vector2(cam_width - 105, cam_height - cam_height * 0.95f);

                break;
            case 3: // player 4
                split_anchor = new Vector2(Screen.width - 105, cam_height - cam_height * 0.95f);

                break;
        }
    }

    int count = 0;
	// Update is called once per frame
	void Update () {
        if (!m.maximized) {
            // splitscreen

            // sizing
            shotA.GetComponent<RectTransform>().sizeDelta = new Vector2(17, 33);
            shotB.GetComponent<RectTransform>().sizeDelta = new Vector2(17, 33);
            shotC.GetComponent<RectTransform>().sizeDelta = new Vector2(17, 33);
            shotD.GetComponent<RectTransform>().sizeDelta = new Vector2(17, 33);
            // positioning
            shotA.GetComponent<RectTransform>().position = new Vector2(split_anchor.x + 21, split_anchor.y +28);
            shotB.GetComponent<RectTransform>().position = new Vector2(split_anchor.x + 41, split_anchor.y +28);
            shotC.GetComponent<RectTransform>().position = new Vector2(split_anchor.x + 61, split_anchor.y +28);
            shotD.GetComponent<RectTransform>().position = new Vector2(split_anchor.x + 81, split_anchor.y +28);

            
        } else {
            if (ctrl.ctrlControlIndex != 0) {
                // not player 1, hide the controls
                shotA.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                shotB.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                shotC.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                shotD.GetComponent<RectTransform>().position = new Vector2(-200, -200);
            } else {
                // fullscreen

                // sizing
                shotA.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 66);
                shotB.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 66);
                shotC.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 66);
                shotD.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 66);
                // positioning
                shotA.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 42, fullscr_anchor.y +56);
                shotB.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 82, fullscr_anchor.y +56);
                shotC.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 122, fullscr_anchor.y +56);
                shotD.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 162, fullscr_anchor.y +56);
            }
        }

        // set sprite for the shots
        for (int i = 0; i < 4; i++) {
            switch (i) {
                case 0: // A
                    if (shots_left >= 1) {
                        shotA.GetComponent<Image>().sprite = full;
                    } else {
                        shotA.GetComponent<Image>().sprite = empty;
                    }
                    break;
                case 1: // B
                    if (shots_left >= 2) {
                        shotB.GetComponent<Image>().sprite = full;
                    } else {
                        shotB.GetComponent<Image>().sprite = empty;
                    }
                    break;
                case 2: // C
                    if (shots_left >= 3) {
                        shotC.GetComponent<Image>().sprite = full;
                    } else {
                        shotC.GetComponent<Image>().sprite = empty;
                    }
                    break;

                case 3: // D
                    if (shots_left >= 4) {
                        shotD.GetComponent<Image>().sprite = full;
                    } else {
                        shotD.GetComponent<Image>().sprite = empty;
                    }
                    break;
            }
        }

        if (count == 0) {
            shots_left++;
            if (shots_left == 5) { shots_left = 0; }
        }
        count = (count + 1) % 10;
	}
}
