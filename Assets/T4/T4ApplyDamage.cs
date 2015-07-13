using UnityEngine;
using System.Collections;

public class T4ApplyDamage : MonoBehaviour{
    private T4GUIHealthbarHandler healthbar;
    private int ControlID;
    private GameObject ship;
    public int damage = 5;

    // Use this for initialization
    void Start(){

    }

    void OnTriggerEnter(Collider other){
        // did the object hit a ship on the same layer?
        if (other.gameObject.tag.Equals("Ship") && (other.gameObject.layer == this.gameObject.layer)){
            ship = other.gameObject;
            healthbar = ship.GetComponent<T4GUIHealthbarHandler>();
            // apply damage
            healthbar.setHealth(healthbar.getHealth() - 5);
            if (healthbar.getHealth() <= 0)
            {
                healthbar.setHealth(100);
            }
        }
    }
}
