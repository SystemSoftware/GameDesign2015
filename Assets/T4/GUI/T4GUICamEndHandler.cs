using UnityEngine;
using System.Collections;

public class T4GUICamEndHandler : MonoBehaviour {
    private Maximize m;
    private GameObject text, bg;
    private Vector2 fullscr_anchor, split_anchor;
    private Controller ctrl;
    private float cam_width, cam_height;

	// Use this for initialization
	void Start () {
        m = GameObject.Find("GameLogic").GetComponent<Maximize>();
        text = GameObject.Find("UI/End" + (this.gameObject.layer-28) + "/Text");
        bg = GameObject.Find("UI/End" + (this.gameObject.layer-28) + "/Background");
        ctrl = this.GetComponent<Controller>();

        cam_width = Screen.width / 2;
        cam_height = Screen.height / 2;
        switch (ctrl.ctrlControlIndex) {
            case 0: // player 1
                split_anchor = new Vector2(cam_width/2, Screen.height-(cam_height/2));
                fullscr_anchor = new Vector2(Screen.width - 205, cam_height - cam_height * 0.92f);
                break;
            case 1: // player 2
                split_anchor = new Vector2(cam_width + (cam_width / 2), Screen.height - (cam_height / 2));

                break;
            case 2: // player 3
                split_anchor = new Vector2(cam_width / 2, cam_height/2);

                break;
            case 3: // player 4
                split_anchor = new Vector2(cam_width + (cam_width / 2), cam_height / 2);

                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!m.maximized) {
            // set black background to fully cover the cam
            bg.GetComponent<RectTransform>().sizeDelta = new Vector2(cam_width + 1, cam_height + 1);
            bg.GetComponent<RectTransform>().position = new Vector2(split_anchor.x, split_anchor.y);
            // position the text
            text.GetComponent<RectTransform>().position = new Vector2(split_anchor.x+20, split_anchor.y+20);
        } else {
            if (ctrl.ctrlControlIndex != 0) {
                // not player 1, hide the controls
                bg.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                text.GetComponent<RectTransform>().position = new Vector2(-200, -200);
            } else {

            }
        }
	}

    public void playEnd() {
        bg.gameObject.active = true;
        text.gameObject.active = true;
    }
}
