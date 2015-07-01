using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


[ExecuteInEditMode]
public class T4GUI : Graphic {
    [Range(0, 100)]
    public int fillPercent;
    public bool fill = true;
    public int thikness = 5;
    private int splitborder_thickness = 1; // *2, ex: 1*2 = 2px
    private Maximize m = null;
    private GameObject g;


    void Start() {
        g = GameObject.Find("GameLogic");
        if (g != null) {
            m = g.GetComponent<Maximize>();
        }
    }

    void Update() {
        this.thikness = (int)Mathf.Clamp(this.thikness, 0, rectTransform.rect.width / 2);
    }

    protected override void OnFillVBO(List<UIVertex> vbo) {
        float outer = -rectTransform.pivot.x * rectTransform.rect.width;
        float inner = -rectTransform.pivot.x * rectTransform.rect.width + this.thikness;

        vbo.Clear();

        UIVertex vert = UIVertex.simpleVert;
        Vector2 prevX = Vector2.zero;
        Vector2 prevY = Vector2.zero;

        vert.color = color;
        vert.position = new Vector3(0,0,0);

        Vector3 c = new Vector3(0, 0, 0);
        Vector3 p = new Vector3(c.x-100, c.y-100, 0);
        //vbo.Add(vert);
        /* Draw red curved arc
        for (int i = 1; i <= 80; i++) {
            Vector3 tmp = new Vector3(0,0,0);
            tmp.x = (c.x + (p.x - c.x) * Mathf.Cos(i * Mathf.Deg2Rad) - (c.y - p.y) * Mathf.Sin(i * Mathf.Deg2Rad));
            tmp.y = (c.y + (p.y - c.y) * Mathf.Sin(i * Mathf.Deg2Rad) + (c.x - p.x) * Mathf.Cos(i * Mathf.Deg2Rad));
            vert.position = tmp;
            vbo.Add(vert);


            vert.position = new Vector3(tmp.x+2, tmp.y, 0);
            vbo.Add(vert);

            vert.position = new Vector3(tmp.x + 2, tmp.y+2, 0);
            vbo.Add(vert);

            vert.position = new Vector3(tmp.x, tmp.y+2, 0);
            vbo.Add(vert);
        }
        */
        splitscreenBorder(vbo);
    }

    private bool test = false;
    private void splitscreenBorder(List<UIVertex> vbo) {
        if ((m != null) && (!m.maximized)) {
            UIVertex vert = UIVertex.simpleVert;
            vert.color = new Color32(30, 30, 30, 255);

            float tmp = Screen.width / 2;
            // horizontal line
            vert.position = new Vector3(-tmp, -splitborder_thickness, 0);
            vbo.Add(vert);
            vert.position = new Vector3(tmp, -splitborder_thickness, 0);
            vbo.Add(vert);
            vert.position = new Vector3(tmp, splitborder_thickness, 0);
            vbo.Add(vert);
            vert.position = new Vector3(-tmp, splitborder_thickness, 0);
            vbo.Add(vert);
            tmp = Screen.height / 2;
            // vertical line
            vert.position = new Vector3(-splitborder_thickness, -tmp, 0);
            vbo.Add(vert);
            vert.position = new Vector3(-splitborder_thickness, tmp, 0);
            vbo.Add(vert);
            vert.position = new Vector3(splitborder_thickness, tmp, 0);
            vbo.Add(vert);
            vert.position = new Vector3(splitborder_thickness, -tmp, 0);
            vbo.Add(vert);
            test = true;
        }
    }
}
