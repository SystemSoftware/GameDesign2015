using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class T6UIController : MonoBehaviour
{
    public const float maxFuel = 80000;
    float currentFuel;
    int id;

    // Use this for initialization
    void Start()
    {
        currentFuel = maxFuel;
        id = this.GetComponent<Controller>().ctrlControlIndex;
		setFuel (currentFuel);
    }

    // Update is called once per frame
    void Update()
    {
         float thrust = Mathf.Max(0,Input.GetAxis(this.GetComponent<Controller>().ctrlAxisAccelerate));
        float maxThrust = 1;
        foreach(EngineDriver e in this.GetComponentsInChildren<EngineDriver>()){
            maxThrust += e.maxForce;
        }
         currentFuel -= thrust * Time.deltaTime * maxThrust;
		T6RaceLogic.setDeltaV (id, currentFuel);
		if (currentFuel <= 0) {
			GameObject.Find("LevelLogic").GetComponent<T6PlantesLogic>().end();
		} else {
			setFuel (currentFuel);
		}
    }

    void setFuel(float val)
    {
        GameObject slider = GameObject.Find("Fuel" + id);

        val = val / maxFuel * 200;
        slider.GetComponent<RectTransform>().sizeDelta = new Vector2(val,10);
    }

	public void reset(){
		this.Start ();
	}

}

