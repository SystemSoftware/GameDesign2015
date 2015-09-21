using UnityEngine;
using System.Collections;

public class T7Level : MonoBehaviour {

	public GUITexture minimap;
	public GameObject[] icon;
    private const float REFSCALEWIDTH = 1920;
    private const float REFSCALEHEIGHT = 1080;
    private char[] states = new char[] { 'f', 'f', 'f', 'f' };
    private int[] laps = new int[] {99, 99, 99, 99};
    bool started = false;
	float maxSpeed = 570f; //Geschwindigkeitsanzeige
    private GameObject[,,] playIcons = new GameObject[4,3,10];
    private int[,] playIconCurrent = new int[4, 3];
    private GameObject[,] lapIcons = new GameObject[4,5];
    private GameObject[] blankScreens = new GameObject[4];
    private int[] sieger = new int[] { 0, 0, 0, 0 };
    private GameObject[] siegerIcon = new GameObject[4];
    private Color32[] colors = new Color32[] { new Color32(90, 130, 255, 255), new Color32(255, 80, 90, 255), new Color32(110, 196, 100, 255), new Color32(196, 120, 60, 255) };
    private float camrotspeed = 0.01f, camrotw = 20000f, camroth= 20000f, timer = 1.0f;
    private Vector3 camSpeedOld;

    private Material Boden;
    private Material Pickel1;
    private Material Pickel2;
    private float val = 0.0f, val2 = 0.0f;
    private const float freq = 0.5f, freq2 = 4.0f;
    private const float PI = 3.1415f;

    void OnGUI()
	{
        if (!Level.AllowMotion)
		{
            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2, 250, 40), "Start"))
			{
				Debug.Log ("Schiffanzahl ="+Level.ActiveShips.Length);
				icon = new GameObject[Level.ActiveShips.Length];
				Transform[] startPoints = new Transform[Level.ActiveShips.Length];
                GUITexture mainicon = GameObject.Find("Icon").GetComponent<GUITexture>();
                GUITexture iconBountyOne = GameObject.Find("Icon Bounty One").GetComponent<GUITexture>();
                GUITexture iconDelusion = GameObject.Find("Icon Delusion").GetComponent<GUITexture>();
                GUITexture iconDOSE = GameObject.Find("Icon DOSE").GetComponent<GUITexture>();
                GUITexture iconSimple = GameObject.Find("Icon Simple").GetComponent<GUITexture>();
                GUITexture iconSpaceCAT = GameObject.Find("Icon SpaceCAT").GetComponent<GUITexture>();
                GUITexture iconSpacefork = GameObject.Find("Icon Spacefork").GetComponent<GUITexture>();
                GUITexture iconViper = GameObject.Find("Icon Viper").GetComponent<GUITexture>();
                GUITexture blank = GameObject.Find("Black").GetComponent<GUITexture>();
                

                for(int i=0;i<playIcons.GetLength(0); i++)
                {
                    for(int e=0; e<playIcons.GetLength(1); e++)
                    {
                        playIconCurrent[i, e] = 0;
                        for(int f=0; f<playIcons.GetLength(2); f++)
                        {
                            playIcons[i, e, f] = new GameObject();
                            playIcons[i, e, f].AddComponent<GUITexture>();
                            playIcons[i, e, f].GetComponent<GUITexture>().texture = GameObject.Find("Icon" + f).GetComponent<GUITexture>().texture;
                            playIcons[i, e, f].GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
                        }
                    }
                }
                for (int i = 0; i < lapIcons.GetLength(0); i++)
                {
                    for (int e = 0; e < lapIcons.GetLength(1); e++)
                    {
                        lapIcons[i, e] = new GameObject();
                        lapIcons[i, e].AddComponent<GUITexture>();
                        lapIcons[i, e].GetComponent<GUITexture>().texture = GameObject.Find("Icon" + (e < 3 ? e+1 : ((e < 4) ? 3 : 99))).GetComponent<GUITexture>().texture;
                        lapIcons[i, e].GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
                    }
                }
               int[] startposes = new int[] { 490, 330, 610, 330, 490, 210, 610, 210 };
                for(int i=0; i<4; i++)
                {
                    siegerIcon[i] = new GameObject();
                    siegerIcon[i].AddComponent<GUITexture>();
                    siegerIcon[i].GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
                    siegerIcon[i].GetComponent<GUITexture>().texture = GameObject.Find("Icon" + (i+1)).GetComponent<GUITexture>().texture;
                    siegerIcon[i].GetComponent<Transform>().localPosition = new Vector3(siegerIcon[i].GetComponent<Transform>().localPosition.x, siegerIcon[i].GetComponent<Transform>().localPosition.y, siegerIcon[i].GetComponent<Transform>().localPosition.z + 50000);

                    blankScreens[i] = new GameObject();
                    blankScreens[i].AddComponent<GUITexture>();
                    blankScreens[i].GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
                    blankScreens[i].GetComponent<GUITexture>().texture = blank.texture;
                    Rect scr;
                    switch (i)
                    {
                        case 0: scr = new Rect(0, Screen.height / 2, Screen.width / 2, Screen.height / 2); break;
                        case 1: scr = new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2); break;
                        case 2: scr = new Rect(0, 0, Screen.width / 2, Screen.height / 2); break;
                        default: scr = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height / 2); break;
                    }
                    blankScreens[i].GetComponent<GUITexture>().pixelInset = scr;
                    blankScreens[i].GetComponent<Transform>().localPosition = new Vector3(blankScreens[i].GetComponent<Transform>().localPosition.x, blankScreens[i].GetComponent<Transform>().localPosition.y, blankScreens[i].GetComponent<Transform>().localPosition.z + 4);
                    blankScreens[i].GetComponent<GUITexture>().color = new Color(0, 0, 0, 255);
                }
				for (int i = 0; i< Level.ActiveShips.Length; i++) {
                    laps[i] = 1;
                    icon[i] = new GameObject();
					icon[i].name = "ShipIcon" + i;
					icon[i].AddComponent<GUITexture>();
                    icon[i].GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
                    switch (Level.ActiveShips[i].name.Split(" ".ToCharArray())[1].ToString().Split("/".ToCharArray())[0].ToString())
                    {
                        case "1": icon[i].GetComponent<GUITexture>().texture = iconSimple.texture; break;
                        case "2": icon[i].GetComponent<GUITexture>().texture = iconDelusion.texture; break;
                        case "3": icon[i].GetComponent<GUITexture>().texture = iconBountyOne.texture; break;
                        case "4": icon[i].GetComponent<GUITexture>().texture = iconViper.texture; break;
                        case "6": icon[i].GetComponent<GUITexture>().texture = iconSpaceCAT.texture; break;
                        case "7": icon[i].GetComponent<GUITexture>().texture = iconDOSE.texture;  break;
                        default: icon[i].GetComponent<GUITexture>().texture = mainicon.texture; break;
                    }
                    icon[i].GetComponent<Transform>().localPosition = new Vector3(icon[i].GetComponent<Transform>().localPosition.x, icon[i].GetComponent<Transform>().localPosition.y, icon[i].GetComponent<Transform>().localPosition.z + 50000);
                    icon[i].GetComponent<GUITexture>().color = colors[i];
                    rotateShip(Level.ActiveShips[i]);
                    icon[i].GetComponent<Transform>().localScale = new Vector3(0,0,0);
					startPoints[i] = Level.ActiveShips[i].transform;
					addLightsToShip(Level.ActiveShips[i]);
                    normalizeShip(Level.ActiveShips[i]);
					startPoints[i].position = new Vector3 (startposes[i * 2], startposes[(i * 2)+1], 2425);
                    blankScreens[Level.ActiveShips[i].ctrlControlIndex].GetComponent<GUITexture>().pixelInset = new Rect(0,0,0,0);
                }
				started = true;
                foreach (GameObject x in GameObject.FindGameObjectsWithTag("MainCamera"))
                {
                    x.GetComponent<Camera>().farClipPlane = 150000f;
                }
                GameObject.Find("Camera").GetComponent<Camera>().nearClipPlane = 0.01f;
                GameObject.Find("Camera").GetComponent<Camera>().farClipPlane = 0.02f;
                minimap.GetComponent<Transform>().localPosition = new Vector3(minimap.GetComponent<Transform>().localPosition.x, minimap.GetComponent<Transform>().localPosition.y, minimap.GetComponent<Transform>().localPosition.z+50000);
                Level.DefineStartPoints(startPoints);
				Level.EnableMotion(true);
                this.GetComponentInChildren<AudioSource>().Play();
            }
		}
	}
    //finding the Skybox_BodenMat
    void findSkyBox_BodenMat()
    {

        Object[] renderers = GameObject.FindObjectsOfType(typeof(Renderer));
        int i_max = renderers.Length;
        for (int i = 0; i < i_max; i++)
        {
            Material[] materials = ((Renderer)renderers[i]).materials;
            int j_max = materials.Length;
            for (int j = 0; j < j_max; j++)
            {
                if (materials[j].name.StartsWith("Skybox_BodenMat"))
                {
                    Boden = materials[j];
                }
                if (materials[j].name.StartsWith("Pickel00"))
                {
                    if (Pickel1 == null)
                    {
                        Pickel1 = materials[j];
                    }
                    else
                    {
                        Pickel2 = materials[j];
                    }
                }
            }
        }
    }
    // Use this for initialization
    void Start () {
        if (Boden == null)
        {
            findSkyBox_BodenMat();
        }
        Level.drag = 0.3f;
		Level.angularDrag = 0.8f;
		Physics.gravity = new Vector3 (0f,0f,0f);
        float ox = GameObject.Find("Camera").GetComponent<Transform>().localPosition.x;
        float oy = GameObject.Find("Camera").GetComponent<Transform>().localPosition.y;
        float oz = GameObject.Find("Camera").GetComponent<Transform>().localPosition.z;
        camSpeedOld = new Vector3(ox, oy, oz);
        Level.InitializationDone();
	}
	
	// Update is called once per frame
	void Update () {
        val = (val + (freq * Time.deltaTime)) % (2 * PI);
        val2 = (val2 + (freq2 * Time.deltaTime)) % (2 * PI);

        float xm = (Mathf.Sin(val) * 0.000005f) + 0.00003f;
        float ym = (Mathf.Sin(val) * 0.0375f + 0.0375f) + 0.005f;
        float zm = (Mathf.Sin(val2) + 1.0f) * 0.5f;

        RenderSettings.fogDensity = xm;
        Boden.SetFloat("_Parallax", ym);
        Pickel1.color = new Color(zm, zm, zm, 1.0f);
        Pickel2.color = new Color(zm, zm, zm, 1.0f);

        if (!started)
        {
            foreach (GameObject i in GameObject.FindGameObjectsWithTag("MainCamera"))
            {
                i.GetComponent<Camera>().farClipPlane = 10f;
            }

            float x = Mathf.Cos(timer) * camrotw;
            float z = Mathf.Sin(timer) * camroth;

            float ox = camSpeedOld.x;
            float oy = camSpeedOld.y;
            float oz = camSpeedOld.z;
            GameObject.Find("Camera").GetComponent<Transform>().localPosition = new Vector3(ox+x, oy, oz+z);
            GameObject.Find("Camera").transform.LookAt(GameObject.Find("Camlock").GetComponent<Transform>().localPosition);
            timer += camrotspeed;
        }
        GUI.depth = 1;
        float texWidth = (minimap.texture.width * (Screen.width / REFSCALEWIDTH));
        float texHeight = (minimap.texture.height * (Screen.height / REFSCALEHEIGHT));
        float newX = (Screen.width - texWidth) / 2;
        float newY = (Screen.height - texHeight) / 2;

        if (started)
		for (int i = 0; i< Level.ActiveShips.Length; i++) {
                if (laps[i] < 4)
                    icon[i].GetComponent<GUITexture>().pixelInset = new Rect(newX + (texWidth/2) + (((Level.ActiveShips[i].transform.position.x) * (Screen.width / REFSCALEWIDTH))/150.0f), newY + (texHeight / 2) + (((Level.ActiveShips[i].transform.position.z - 21500) * (Screen.height / REFSCALEHEIGHT)) / 125.0f), 40 * (Screen.width / REFSCALEWIDTH), 40 * (Screen.height / REFSCALEHEIGHT));
		}

        if (playIcons[0, 0, 0] != null)
            for (int i = 0; i < Level.ActiveShips.Length; i++)
            {
                int speed = (int)Vector3.Magnitude(Level.ActiveShips[i].GetComponentInParent<Rigidbody>().velocity) / 2;


                Rect std = new Rect(0, 0, 0, 0);
                playIcons[i, 0, playIconCurrent[i, 0]].GetComponent<GUITexture>().pixelInset = std;
                playIcons[i, 1, playIconCurrent[i, 1]].GetComponent<GUITexture>().pixelInset = std;
                playIcons[i, 2, playIconCurrent[i, 2]].GetComponent<GUITexture>().pixelInset = std;
                for(int e=0; e<3; e++)
                {
                    lapIcons[i, e].GetComponent<GUITexture>().pixelInset = std;
                }

                float refWidth = 50 * (Screen.width / REFSCALEWIDTH);
                float refHeight = 50 * (Screen.height / REFSCALEHEIGHT);

                float[] heights = new float[] { (Screen.height / 2) + refHeight / 2, (Screen.height / 2) + refHeight / 2, refHeight / 2, refHeight / 2};
                float[] widths = new float[] { (Screen.width * 0.375f), (Screen.width * 0.625f), (Screen.width * 0.375f), (Screen.width * 0.625f) };

                float[] heightsLap = new float[] {Screen.height - refHeight * 1.5f, Screen.height - refHeight * 1.5f, refHeight * 2.5f, refHeight * 2.5f };
                float[] widthsLap = new float[] {refWidth * 0.5f, Screen.width - refWidth * 2.5f, refWidth * 0.5f, Screen.width - refWidth * 2.5f };

                lapIcons[i, 3].GetComponent<GUITexture>().pixelInset = new Rect(widthsLap[Level.ActiveShips[i].ctrlControlIndex] + refWidth, heightsLap[Level.ActiveShips[i].ctrlControlIndex] - refHeight, refWidth, refHeight);
                lapIcons[i, 3].GetComponent<GUITexture>().color = colors[i];
                lapIcons[i, 4].GetComponent<GUITexture>().pixelInset = new Rect(widthsLap[Level.ActiveShips[i].ctrlControlIndex] + refWidth / 2, heightsLap[Level.ActiveShips[i].ctrlControlIndex] - refHeight / 2, refWidth, refHeight);
                lapIcons[i, 4].GetComponent<GUITexture>().color = colors[i];
                if (laps[i] < 4)
                {
                    lapIcons[i, laps[i] - 1].GetComponent<GUITexture>().pixelInset = new Rect(widthsLap[Level.ActiveShips[i].ctrlControlIndex], heightsLap[Level.ActiveShips[i].ctrlControlIndex], refWidth, refHeight);
                    lapIcons[i, laps[i] - 1].GetComponent<GUITexture>().color = colors[i];
                }
                else
                {
                    if (blankScreens[Level.ActiveShips[i].ctrlControlIndex].GetComponent<GUITexture>().pixelInset.width == 0)
                    {
                        Rect scr;
                        switch (Level.ActiveShips[i].ctrlControlIndex)
                        {
                            case 0: scr = new Rect(0, Screen.height / 2, Screen.width / 2, Screen.height / 2); break;
                            case 1: scr = new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2); break;
                            case 2: scr = new Rect(0, 0, Screen.width / 2, Screen.height / 2); break;
                            default: scr = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height / 2); break;
                        }
                        blankScreens[Level.ActiveShips[i].ctrlControlIndex].GetComponent<GUITexture>().pixelInset = scr;
                    }
                }


                if (speed >= 999)
                {
                    playIconCurrent[i, 0] = 9;
                    playIconCurrent[i, 1] = 9;
                    playIconCurrent[i, 2] = 9;
                    playIcons[i, 0, 9].GetComponent<GUITexture>().pixelInset = new Rect(widths[Level.ActiveShips[i].ctrlControlIndex] - 1.5f * refWidth, heights[Level.ActiveShips[i].ctrlControlIndex], refWidth, refHeight);
                    playIcons[i, 1, 9].GetComponent<GUITexture>().pixelInset = new Rect(widths[Level.ActiveShips[i].ctrlControlIndex] - 0.5f * refWidth, heights[Level.ActiveShips[i].ctrlControlIndex], refWidth, refHeight);
                    playIcons[i, 2, 9].GetComponent<GUITexture>().pixelInset = new Rect(widths[Level.ActiveShips[i].ctrlControlIndex] + 0.5f * refWidth, heights[Level.ActiveShips[i].ctrlControlIndex], refWidth, refHeight);
                }
                else if (speed >= 0)
                {
                    playIconCurrent[i, 0] = (speed / 100);
                    playIconCurrent[i, 1] = (speed % 100) / 10;
                    playIconCurrent[i, 2] = (speed % 10);
                    playIcons[i, 0, (speed / 100)].GetComponent<GUITexture>().pixelInset = new Rect(widths[Level.ActiveShips[i].ctrlControlIndex] - 1.5f * refWidth, heights[Level.ActiveShips[i].ctrlControlIndex], refWidth, refHeight);
                    playIcons[i, 1, (speed % 100) / 10].GetComponent<GUITexture>().pixelInset = new Rect(widths[Level.ActiveShips[i].ctrlControlIndex] - 0.5f * refWidth, heights[Level.ActiveShips[i].ctrlControlIndex], refWidth, refHeight);
                    playIcons[i, 2, (speed % 10)].GetComponent<GUITexture>().pixelInset = new Rect(widths[Level.ActiveShips[i].ctrlControlIndex] + 0.5f * refWidth, heights[Level.ActiveShips[i].ctrlControlIndex], refWidth, refHeight);
                    playIcons[i, 0, (speed / 100)].GetComponent<GUITexture>().color = new Color(0.5f - (0.4f * speed / 1000), 0.9f - (1.5f * speed / 1000), 0.5f - (1f * speed / 1000), 255);
                    playIcons[i, 1, (speed % 100) / 10].GetComponent<GUITexture>().color = new Color(0.5f - (0.4f * speed / 1000), 0.9f - (1.5f * speed / 1000), 0.5f - (1f * speed / 1000), 255);
                    playIcons[i, 2, (speed % 10)].GetComponent<GUITexture>().color = new Color(0.5f - (0.4f * speed / 1000), 0.9f - (1.5f * speed / 1000), 0.5f - (1f * speed / 1000), 255);
                }
            }
        minimap.pixelInset = new Rect(newX, newY, texWidth, texHeight);
   	}

	void normalizeShip(Controller ship){
        foreach(var Listener in ship.GetComponentsInChildren<AudioListener>())
        {
            Destroy(Listener);
        }
    }

    void rotateShip(Controller ship)
    {
        ship.transform.Rotate(Vector3.up, 180.0f);
    }

	void addLightsToShip (Controller ship)
	{
        Light lightComp = ship.gameObject.GetComponent<Light>();
        if (lightComp == null)
            lightComp = ship.gameObject.AddComponent<Light>();
		lightComp.color = Color.white;
		lightComp.range = 2000f;
		lightComp.type = LightType.Spot;
        lightComp.intensity = 50f;
	}

    public void ringVisited(int ringnumber, GameObject ship)
    {
        for(int i=0; i<Level.ActiveShips.Length; i++)
        {
            if (Level.ActiveShips[i].gameObject.transform.root.gameObject == ship)
            {
                switch (ringnumber)
                {
                    case 1:
                        switch (states[i])
                        {
                            case '1':
                            case '2': 
                            case '3': states[i] = '2'; break;
                            default: states[i] = 'f';  break;
                        }
                        break;
                    case 2:
                        switch (states[i])
                        {
                            case '2':
                            case '3': states[i] = '3'; break;
                            case '1':
                            default: states[i] = 'f'; break;
                        }
                        break;
                    case 3:
                        if(states[i] == '3')
                        {
                            laps[i]++;
                            if(laps[i] == 4)
                            {
                                int cnt = 0;
                                for(int e=0; e < 4; e++)
                                {
                                    if(sieger[i] == 1)
                                    {
                                        cnt++;
                                    }
                                }
                                sieger[i] = 1;
                                Color col;
                                Rect scr;

                                switch(cnt)
                                {
                                    case 0: col = new Color32(185, 128, 0, 128); break;
                                    case 1: col = new Color32(157, 165, 165, 128); break;
                                    case 2: col = new Color32(124, 93, 39, 128); break;
                                    default: col = new Color32(77, 130, 168, 128); break;
                                }
                                float refWidth = 50 * (Screen.width / REFSCALEWIDTH);
                                float refHeight = 50 * (Screen.height / REFSCALEHEIGHT);
                                switch (Level.ActiveShips[i].ctrlControlIndex)
                                {
                                    case 0: scr = new Rect(Screen.width * 0.25f - refWidth / 2, Screen.height * 0.75f - refHeight/2, refWidth, refHeight); break;
                                    case 1: scr = new Rect(Screen.width * 0.75f - refWidth / 2, Screen.height * 0.75f - refHeight/2, refWidth, refHeight); break;
                                    case 2: scr = new Rect(Screen.width * 0.25f - refWidth / 2, Screen.height * 0.25f - refHeight/2, refWidth, refHeight); break;
                                    default: scr = new Rect(Screen.width * 0.75f - refWidth / 2, Screen.height * 0.25f - refHeight / 2, refWidth, refHeight); break;
                                }
                                siegerIcon[cnt].GetComponent<GUITexture>().pixelInset = scr;
                                siegerIcon[cnt].GetComponent<GUITexture>().color = col;
                            }
                        }
                        states[i] = '1';
                        break;
                    default: break;
                }
               
            }
        }
    }
}