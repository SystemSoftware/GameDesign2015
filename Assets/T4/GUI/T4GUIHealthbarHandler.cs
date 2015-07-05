using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class T4GUIHealthbarHandler : MonoBehaviour {
    private Controller ctrl;
    private Maximize m;
    private GameObject hbar_numA, hbar_numB, hbar_numC, bar, icon;

    // screen pos
    Vector2 fullscr_anchor, split_anchor;

	// Use this for initialization
	void Start () {
        m = GameObject.Find("GameLogic").GetComponent<Maximize>();
        ctrl = this.GetComponent<Controller>();

        // load sprites
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/MiddleNumbers");
        Debug.Log("sprites loaded = " + sprites.Length);

        // first number A config
        hbar_numA = GameObject.Find("Healthbar"+ctrl.ctrlControlIndex).transform.Find("NumA").gameObject;
        hbar_numA.GetComponent<RectTransform>().sizeDelta = new Vector2(25,25);
        hbar_numA.GetComponent<Image>().color = new Color32(255,255,255,255);
        hbar_numA.GetComponent<Image>().sprite = sprites[0];

        // second number B config
        hbar_numB = GameObject.Find("Healthbar" + ctrl.ctrlControlIndex).transform.Find("NumB").gameObject;
        hbar_numB.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        hbar_numB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        hbar_numB.GetComponent<Image>().sprite = sprites[1];

        // third number C config
        hbar_numC = GameObject.Find("Healthbar" + ctrl.ctrlControlIndex).transform.Find("NumC").gameObject;
        hbar_numC.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        hbar_numC.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        hbar_numC.GetComponent<Image>().sprite = sprites[2];

        // get the bar
        bar = GameObject.Find("Healthbar" + ctrl.ctrlControlIndex).transform.Find("Bar").gameObject;
        bar.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 25);
        bar.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        bar.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/hbar_full");

        // healthbar icon
        icon = GameObject.Find("Healthbar" + ctrl.ctrlControlIndex).transform.Find("Icon").gameObject;
        icon.GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32);
        icon.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/hbar_icon");

        float cam_width = Screen.width / 2;
        float cam_height = Screen.height / 2;
        // set the fullscrn_anchor & splitscreenanchor for each player
        switch (ctrl.ctrlControlIndex) {
            case 0: // player 1
                // 20% from left and 90% from top of camera
                // 210 since the texture is 200px width and 25 height
                split_anchor = new Vector2(105, Screen.height-cam_height*0.95f);
                fullscr_anchor = new Vector2(205, cam_height - cam_height * 0.92f);
                break;
            case 1: // player 2
                split_anchor = new Vector2(cam_width + 105, Screen.height - cam_height * 0.95f);

                break;
            case 2: // player 3
                split_anchor = new Vector2(105, cam_height - cam_height * 0.95f);

                break;
            case 3: // player 4
                split_anchor = new Vector2(cam_width + 105, cam_height - cam_height * 0.95f);

                break;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (!m.maximized) { // splitscreen
            if (ctrl.ctrlControlIndex == 0) {   // adjust size of components used for player 1
                icon.GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32);
                hbar_numA.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                hbar_numB.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                hbar_numC.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                bar.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 25);
            }
            // position components
            icon.GetComponent<RectTransform>().position = new Vector2(split_anchor.x + 10, split_anchor.y + 25);
            hbar_numA.GetComponent<RectTransform>().position = new Vector2(split_anchor.x + 35, split_anchor.y + 25);
            hbar_numB.GetComponent<RectTransform>().position = new Vector2(split_anchor.x + 55, split_anchor.y + 25);
            hbar_numC.GetComponent<RectTransform>().position = new Vector2(split_anchor.x + 75, split_anchor.y + 25);
            bar.GetComponent<RectTransform>().position = split_anchor;
        } else { // fullscreen
            if (ctrl.ctrlControlIndex != 0) { // not player 1
                // hide components of other players
                icon.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                hbar_numA.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                hbar_numB.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                hbar_numC.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                bar.GetComponent<RectTransform>().position = new Vector2(-200, -200);
            } else { // is player 1
                // adjust size of ui parts
                icon.GetComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
                hbar_numA.GetComponent<RectTransform>().sizeDelta = new Vector2(50,50);
                hbar_numB.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                hbar_numC.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                bar.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 50);
                // position components
                icon.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 20, fullscr_anchor.y + 50);
                hbar_numA.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 70, fullscr_anchor.y + 50);
                hbar_numB.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 110, fullscr_anchor.y + 50);
                hbar_numC.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 150, fullscr_anchor.y + 50);
                bar.GetComponent<RectTransform>().position = fullscr_anchor;
            }
        }
	}
}
