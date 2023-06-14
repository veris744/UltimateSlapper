using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEvent : Event
{
    public Car car;

    public override void OnTriggerWithPlayer(PlayerController player)
    {
        car.Run();
        gameObject.SetActive(false);
    }


}
