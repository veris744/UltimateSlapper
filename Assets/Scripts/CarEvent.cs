using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEvent : Event
{
    public override void OnTriggerWithPlayer(PlayerController player)
    {
        Debug.Log("Event car");
        this.gameObject.SetActive(false);
    }
}
