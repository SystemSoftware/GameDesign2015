using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


[ExecuteInEditMode]
public class T4GUI : Graphic {
    [Range(0, 100)]
    public int fillPercent;
    public bool fill = true;
    public int thikness = 5;

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
        /*
        vbo.Add(vert);
        vert.position = new Vector3(10, 0, 0);
        vbo.Add(vert);
        vert.position = new Vector3(10, 10, 0);
        vbo.Add(vert);
        vert.position = new Vector3(0, 10, 0);
        vbo.Add(vert);

        vert.position = new Vector3(-10, 20, 0);
        vbo.Add(vert);
        vert.position = new Vector3(0, 20, 0);
        vbo.Add(vert);
        vert.position = new Vector3(0, 30, 0);
        vbo.Add(vert);
        vert.position = new Vector3(-10, 30, 0);
        vbo.Add(vert);
         * */
        Vector3 c = new Vector3(0, 0, 0);
        Vector3 p = new Vector3(c.x-100, c.y-100, 0);
        //vbo.Add(vert);
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
        /*
        float f = (float)(this.fillPercent / 100f);
        int fa = (int)(361 * f);

        for (int i = 0; i < fa; i++) {
            float rad = Mathf.Deg2Rad * i;
            float c = Mathf.Cos(rad);
            float s = Mathf.Sin(rad);
            float x = outer * c;
            float y = inner * c;
            vert.color = color;
            vert.position = prevX;
            vbo.Add(vert);
            prevX = new Vector2(outer * c, outer * s);
            vert.position = prevX;
            vbo.Add(vert);
            if (this.fill) {
                vert.position = Vector2.zero;
                vbo.Add(vert);
                vbo.Add(vert);
            } else {
                vert.position = new Vector2(inner * c, inner * s); ;
                vbo.Add(vert);
                vert.position = prevY;
                vbo.Add(vert);
                prevY = new Vector2(inner * c, inner * s);
            }
        }*/
    }
}
