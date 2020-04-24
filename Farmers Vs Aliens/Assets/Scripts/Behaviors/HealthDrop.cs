using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : PickUp
{
    public int health;
    // Start is called before the first frame update
    protected override void PickUpEffect()
    {
        GameManager._instance.GetPlayer().GetComponent<PlayerController>().GainHealth(health);
        base.PickUpEffect();
    }
}
