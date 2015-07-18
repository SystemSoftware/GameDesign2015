using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class T4GUIScoreHandler : MonoBehaviour {
    private int score, seconds_passed;
    private Controller ctrl;
    private Maximize m;
    private Sprite[] score_num, time_num;
    private Sprite star_sprite, colon_sprite;
    private GameObject scoreNA, scoreNB, scoreNC, scoreND, timeNA, timeNB, timeNC, timeND, star, colon;
    private T4Logic logic;

    Vector2 fullscr_anchor, split_anchor;

	// Use this for initialization
	void Start () {
        score = 0;
        seconds_passed = 683; // 11:23

        m = GameObject.Find("GameLogic").GetComponent<Maximize>();
        logic = GameObject.Find("Logic").GetComponent<T4Logic>();
        ctrl = this.GetComponent<Controller>();

        // load star icon
        star_sprite = Resources.Load<Sprite>("Sprites/star_icon");
        colon_sprite = Resources.Load<Sprite>("Sprites/colon_icon");
        // load number sprites
        score_num = Resources.LoadAll<Sprite>("Sprites/ScoreNumbers");
        time_num = Resources.LoadAll<Sprite>("Sprites/TimeNumbers");

        // get GameObjects
        star = GameObject.Find("ScoreTime" + ctrl.ctrlControlIndex).transform.Find("Star").gameObject;
        colon = GameObject.Find("ScoreTime" + ctrl.ctrlControlIndex).transform.Find("Colon").gameObject;
        scoreNA = GameObject.Find("ScoreTime" + ctrl.ctrlControlIndex).transform.Find("scoreNA").gameObject;
        scoreNB = GameObject.Find("ScoreTime" + ctrl.ctrlControlIndex).transform.Find("scoreNB").gameObject;
        scoreNC = GameObject.Find("ScoreTime" + ctrl.ctrlControlIndex).transform.Find("scoreNC").gameObject;
        scoreND = GameObject.Find("ScoreTime" + ctrl.ctrlControlIndex).transform.Find("scoreND").gameObject;
        timeNA = GameObject.Find("ScoreTime" + ctrl.ctrlControlIndex).transform.Find("timeNA").gameObject;
        timeNB = GameObject.Find("ScoreTime" + ctrl.ctrlControlIndex).transform.Find("timeNB").gameObject;
        timeNC = GameObject.Find("ScoreTime" + ctrl.ctrlControlIndex).transform.Find("timeNC").gameObject;
        timeND = GameObject.Find("ScoreTime" + ctrl.ctrlControlIndex).transform.Find("timeND").gameObject;

        // set inital size
        star.GetComponent<RectTransform>().sizeDelta = new Vector2(22, 22);
        colon.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 8);
        scoreNA.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        scoreNB.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        scoreNC.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        scoreND.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        timeNA.GetComponent<RectTransform>().sizeDelta = new Vector2(16, 16);
        timeNB.GetComponent<RectTransform>().sizeDelta = new Vector2(16, 16);
        timeNC.GetComponent<RectTransform>().sizeDelta = new Vector2(16, 16);
        timeND.GetComponent<RectTransform>().sizeDelta = new Vector2(16, 16);

        // set default sprites
        star.GetComponent<Image>().sprite = star_sprite;
        colon.GetComponent<Image>().sprite = colon_sprite;
        scoreNA.GetComponent<Image>().sprite = score_num[0];
        scoreNB.GetComponent<Image>().sprite = score_num[0];
        scoreNC.GetComponent<Image>().sprite = score_num[0];
        scoreND.GetComponent<Image>().sprite = score_num[0];
        timeNA.GetComponent<Image>().sprite = time_num[0];
        timeNB.GetComponent<Image>().sprite = time_num[0];
        timeNC.GetComponent<Image>().sprite = time_num[0];
        timeND.GetComponent<Image>().sprite = time_num[0];

        // make parts visible
        star.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        colon.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        scoreNA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        scoreNB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        scoreNC.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        scoreND.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        timeNA.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        timeNB.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        timeNC.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        timeND.GetComponent<Image>().color = new Color32(255, 255, 255, 255);



        // set anchor position
        float cam_width = Screen.width / 2;
        float cam_height = Screen.height / 2;
        // set the fullscrn_anchor & splitscreenanchor for each player
        switch (ctrl.ctrlControlIndex) {
            case 0: // player 1
                // 20% from left and 90% from top of camera
                // 210 since the texture is 200px width and 25 height
                split_anchor = new Vector2(cam_width - 205, Screen.height - cam_height * 0.05f);
                fullscr_anchor = new Vector2(Screen.width - 410, Screen.height - cam_height * 0.05f);
                break;
            case 1: // player 2
                split_anchor = new Vector2(Screen.width - 205, Screen.height - cam_height * 0.05f);

                break;
            case 2: // player 3
                split_anchor = new Vector2(cam_width - 205, cam_height - cam_height * 0.05f);

                break;
            case 3: // player 4
                split_anchor = new Vector2(Screen.width - 205, cam_height - cam_height * 0.05f);

                break;
        }
	}

    public void addScore(int toAdd) {
        score += toAdd;
    }

    public int getScore() {
        return score;
    }

    public void setScore(int toSet) {
        score = toSet;
    }

    public int getPassedTimeInSec() {
        return seconds_passed;
    }

    public void setPassedTimeInSec(int new_time) {
        seconds_passed = new_time;
    }

    public void addPassedTimeInSec(int new_sec) {
        seconds_passed += new_sec;
    }

	// Update is called once per frame
	void Update () {
        // update the time
        seconds_passed = logic.getPassedTimeInSeconds();

        // score numbers
        scoreNA.GetComponent<Image>().sprite = score_num[(int)(score / 1000)];
        scoreNB.GetComponent<Image>().sprite = score_num[(int)((score % 1000) / 100)];
        scoreNC.GetComponent<Image>().sprite = score_num[(int)((score % 100) / 10)];
        scoreND.GetComponent<Image>().sprite = score_num[(score%10)];
        // calc the time to display
        // minutes
        int min = (int)((float)seconds_passed / 60);
        timeNA.GetComponent<Image>().sprite = time_num[(int)((float)min / 10)];
        timeNB.GetComponent<Image>().sprite = time_num[(int)(min % 10)];
        // seconds
        int sec = seconds_passed % 60;
        timeNC.GetComponent<Image>().sprite = time_num[(int)((float)sec / 10)];
        timeND.GetComponent<Image>().sprite = time_num[(int)(sec % 10)];


        if (!m.maximized) {
            // SPLITSCREEN
            
            // sizing
            star.GetComponent<RectTransform>().sizeDelta = new Vector2(22, 22);
            colon.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 8);
            scoreNA.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            scoreNB.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            scoreNC.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            scoreND.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            timeNA.GetComponent<RectTransform>().sizeDelta = new Vector2(16, 16);
            timeNB.GetComponent<RectTransform>().sizeDelta = new Vector2(16, 16);
            timeNC.GetComponent<RectTransform>().sizeDelta = new Vector2(16, 16);
            timeND.GetComponent<RectTransform>().sizeDelta = new Vector2(16, 16);

            // positioning
            star.GetComponent<RectTransform>().position =    new Vector2(split_anchor.x + 4, split_anchor.y - 9);
            scoreNA.GetComponent<RectTransform>().position = new Vector2(split_anchor.x + 40, split_anchor.y - 8);
            scoreNB.GetComponent<RectTransform>().position = new Vector2(split_anchor.x + 85, split_anchor.y - 8);
            scoreNC.GetComponent<RectTransform>().position = new Vector2(split_anchor.x + 130, split_anchor.y - 8);
            scoreND.GetComponent<RectTransform>().position = new Vector2(split_anchor.x + 175, split_anchor.y - 8);
            timeNA.GetComponent<RectTransform>().position =  new Vector2(split_anchor.x + 110, split_anchor.y - 34);
            timeNB.GetComponent<RectTransform>().position =  new Vector2(split_anchor.x + 130, split_anchor.y - 34);
            colon.GetComponent<RectTransform>().position =   new Vector2(split_anchor.x + 145, split_anchor.y - 34);
            timeNC.GetComponent<RectTransform>().position =  new Vector2(split_anchor.x + 160, split_anchor.y - 34);
            timeND.GetComponent<RectTransform>().position =  new Vector2(split_anchor.x + 180, split_anchor.y - 34);

        } else {
            // FULLSCREEN

            if (ctrl.ctrlControlIndex != 0) {
                // not player 1
                star.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                scoreNA.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                scoreNB.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                scoreNC.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                scoreND.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                timeNA.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                timeNB.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                colon.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                timeNC.GetComponent<RectTransform>().position = new Vector2(-200, -200);
                timeND.GetComponent<RectTransform>().position = new Vector2(-200, -200);
            } else {
                // player 1

                // sizing
                star.GetComponent<RectTransform>().sizeDelta = new Vector2(45, 44);
                colon.GetComponent<RectTransform>().sizeDelta = new Vector2(8, 16);
                scoreNA.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                scoreNB.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                scoreNC.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                scoreND.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                timeNA.GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32);
                timeNB.GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32);
                timeNC.GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32);
                timeND.GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32);

                // positioning
                star.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 8, fullscr_anchor.y - 26);
                scoreNA.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 80, fullscr_anchor.y - 24);
                scoreNB.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 170, fullscr_anchor.y - 24);
                scoreNC.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 260, fullscr_anchor.y - 24);
                scoreND.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 350, fullscr_anchor.y - 24);
                timeNA.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 220, fullscr_anchor.y - 68);
                timeNB.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 260, fullscr_anchor.y - 68);
                colon.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 290, fullscr_anchor.y - 68);
                timeNC.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 320, fullscr_anchor.y - 68);
                timeND.GetComponent<RectTransform>().position = new Vector2(fullscr_anchor.x + 360, fullscr_anchor.y - 68);
            }
        }
	}
}
