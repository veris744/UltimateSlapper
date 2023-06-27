using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickable : Pickable
{
    public float extraLife = 1;

    private void Awake()
    {
        boostType = 4;
    }

    public override void OnTriggerWithPlayer(PlayerController player)
    {
        audioSource.Play();

        GameManager.Instance.AddLifes(1);

        meshRenderer.enabled = false;
        objectCollider.enabled = false;
        player.forceBoosted = true;

        StartCoroutine(CountdownToReset(player));

    }

    public override void ResetPlayer(PlayerController player)
    {
        
    }
}
