using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class T4GUIGlobalEndHandler : MonoBehaviour {
    private Maximize m;
    private GameObject titleA, titleB, titleC, credit, bg, hbar1, hbar2, vbar1, vbar2, vbar3;
    private GameObject r1No, r1Player, r1ShipName, r1Points;
    private GameObject r2No, r2Player, r2ShipName, r2Points;
    private GameObject r3No, r3Player, r3ShipName, r3Points;
    private GameObject r4No, r4Player, r4ShipName, r4Points;
    private float cam_width, cam_height;

    private string r1PName, r2PName, r3PName, r4PName;
    private string r1SName, r2SName, r3SName, r4SName;
    private int r1Score, r2Score, r3Score, r4Score;
    private GameObject globalEnd;
    private T4Sound3DLogic soundLogic;

    // Use this for initialization
    void Start() {
        globalEnd = GameObject.Find("UI/GlobalEnd");
        soundLogic = GameObject.Find("SoundContainer").GetComponent<T4Sound3DLogic>();

        bg = GameObject.Find("UI/GlobalEnd/Background");
        titleA = GameObject.Find("UI/GlobalEnd/TitleA");
        titleB = GameObject.Find("UI/GlobalEnd/TitleB");
        titleC = GameObject.Find("UI/GlobalEnd/TitleC");
        credit = GameObject.Find("UI/GlobalEnd/Credit");

        hbar1 = GameObject.Find("UI/GlobalEnd/HBar1");
        hbar2 = GameObject.Find("UI/GlobalEnd/HBar2");
        vbar1= GameObject.Find("UI/GlobalEnd/VBar1");
        vbar2 = GameObject.Find("UI/GlobalEnd/VBar2");
        vbar3 = GameObject.Find("UI/GlobalEnd/VBar3");

        // row1
        r1No = GameObject.Find("UI/GlobalEnd/Row1/No");
        r1Player = GameObject.Find("UI/GlobalEnd/Row1/Player");
        r1ShipName = GameObject.Find("UI/GlobalEnd/Row1/ShipName");
        r1Points = GameObject.Find("UI/GlobalEnd/Row1/Points");
        // row2
        r2No = GameObject.Find("UI/GlobalEnd/Row2/No");
        r2Player = GameObject.Find("UI/GlobalEnd/Row2/Player");
        r2ShipName = GameObject.Find("UI/GlobalEnd/Row2/ShipName");
        r2Points = GameObject.Find("UI/GlobalEnd/Row2/Points");
        // row3
        r3No = GameObject.Find("UI/GlobalEnd/Row3/No");
        r3Player = GameObject.Find("UI/GlobalEnd/Row3/Player");
        r3ShipName = GameObject.Find("UI/GlobalEnd/Row3/ShipName");
        r3Points = GameObject.Find("UI/GlobalEnd/Row3/Points");
        // row4
        r4No = GameObject.Find("UI/GlobalEnd/Row4/No");
        r4Player = GameObject.Find("UI/GlobalEnd/Row4/Player");
        r4ShipName = GameObject.Find("UI/GlobalEnd/Row4/ShipName");
        r4Points = GameObject.Find("UI/GlobalEnd/Row4/Points");

        cam_width = Screen.width / 2;
        cam_height = Screen.height / 2;

        // set default vars
        r1PName = "Player 1"; r2PName = "Player 2"; r3PName = "Player 3"; r4PName = "Player 4";
        r1SName = "###"; r2SName = "###"; r3SName = "###"; r4SName = "###";
        r1Score = 0; r2Score = 0; r3Score = 0; r4Score = 0;
    }

    // Update is called once per frame
    void Update() {
        bg.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        titleA.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) - 280, 
                                                                    (Screen.height / 2) + 300);
        hbar1.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width*0.7f, 10);
        hbar1.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2),
                                                                    (Screen.height / 2) + 170);
        titleB.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) - 280, 
                                                                    (Screen.height / 2) + 140);
        int shiftMidBy = 160;
        vbar1.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 240);
        vbar1.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) - 280 - shiftMidBy, (Screen.height / 2) - 40);
        vbar2.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 240);
        vbar2.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) + 40 - shiftMidBy, (Screen.height / 2) - 40);
        vbar3.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 240);
        vbar3.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) + 290 - shiftMidBy, (Screen.height / 2) - 40);
        // names
        r1Player.GetComponent<Text>().text = r1PName;
        r2Player.GetComponent<Text>().text = r2PName;
        r3Player.GetComponent<Text>().text = r3PName;
        r4Player.GetComponent<Text>().text = r4PName;
        // ships
        r1ShipName.GetComponent<Text>().text = r1SName;
        r2ShipName.GetComponent<Text>().text = r2SName;
        r3ShipName.GetComponent<Text>().text = r3SName;
        r4ShipName.GetComponent<Text>().text = r4SName;
        // scores
        r1Points.GetComponent<Text>().text = r1Score+" Points";
        r2Points.GetComponent<Text>().text = r2Score + " Points";
        r3Points.GetComponent<Text>().text = r3Score + " Points";
        r4Points.GetComponent<Text>().text = r4Score + " Points";
        /* player scores display */
        // row1
        //r1No.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) - 280 - shiftMidBy, (Screen.height / 2) + 50);
        r1Player.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) - 160 - shiftMidBy, (Screen.height / 2) + 50);
        r1ShipName.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) + 160 - shiftMidBy, (Screen.height / 2) + 50);
        r1Points.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) + 400 - shiftMidBy, (Screen.height / 2) + 50);
        // row2
        //r2No.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) - 280 - shiftMidBy, (Screen.height / 2) - 10);
        r2Player.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) - 160 - shiftMidBy, (Screen.height / 2) - 10);
        r2ShipName.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) + 160 - shiftMidBy, (Screen.height / 2) - 10);
        r2Points.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) + 400 - shiftMidBy, (Screen.height / 2) - 10);
        // row3
        //r3No.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) - 280 - shiftMidBy, (Screen.height / 2) - 70);
        r3Player.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) - 160 - shiftMidBy, (Screen.height / 2) - 70);
        r3ShipName.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) + 160 - shiftMidBy, (Screen.height / 2) - 70);
        r3Points.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) + 400 - shiftMidBy, (Screen.height / 2) - 70);
        // row4
        //r4No.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) - 280 - shiftMidBy, (Screen.height / 2) - 130);
        r4Player.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) - 160 - shiftMidBy, (Screen.height / 2) - 130);
        r4ShipName.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) + 160 - shiftMidBy, (Screen.height / 2) - 130);
        r4Points.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) + 400 - shiftMidBy, (Screen.height / 2) - 130);

        r1No.SetActive(false);
        r2No.SetActive(false);
        r3No.SetActive(false);
        r4No.SetActive(false);
        vbar1.SetActive(false);

        hbar2.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 0.7f, 10);
        hbar2.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2),
                                                                    (Screen.height / 2) - 200);
        titleC.GetComponent<RectTransform>().position = new Vector2((Screen.width / 2) - 320,
                                                                    (Screen.height / 2) - 230);
        credit.GetComponent<RectTransform>().position = new Vector2(Screen.width - 570, 20);
    }

    public void playEnd() {
        Debug.Log("play global end");
        globalEnd.SetActiveRecursively(true);
        soundLogic.playEndTheme();
        // todo turn all sounds beside endtheme off
    }
}
