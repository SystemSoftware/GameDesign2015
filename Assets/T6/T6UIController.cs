using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class T6UIController : MonoBehaviour
{
    const float maxFuel = 25;
    float currentFuel;
    int id;

    // Use this for initialization
    void Start()
    {
        currentFuel = maxFuel;
        id = this.GetComponent<Controller>().ctrlControlIndex;
    }

    // Update is called once per frame
    void Update()
    {
         float thrust = Mathf.Max(0,Input.GetAxis(this.GetComponent<Controller>().ctrlAxisAccelerate));
         currentFuel -= thrust * Time.deltaTime;
         setFuel(currentFuel);
    }

    void setFuel(float val)
    {
        GameObject slider = GameObject.Find("Fuel" + id);
        Debug.Log(val);
        Debug.Log(slider);
        val = val / maxFuel * 200;
        slider.GetComponent<RectTransform>().sizeDelta = new Vector2(val,10);
        
        Debug.Log(val);
    }

}

