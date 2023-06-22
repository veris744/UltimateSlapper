using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallEvent : Event
{
    public FallingObject fallingObject;
    public float restingTime = 5;
    [Range(0, 1)]
    public float probability = 0.5f;
    //public float destroyTime = 5;

    private bool isResting = false;
    private Vector3 Offset = new Vector3(0, 10, 0);

    

    public override void OnTriggerWithPlayer(PlayerController player)
    {
        fallingObject.player = player;
        if (!isResting)
        {
            float r = Random.Range(0f, 1f);
            if (r < 0.5f)
            {
                fallingObject.gameObject.SetActive(true);
                fallingObject.transform.position = player.transform.position + Offset;
                isResting = true;
                StartCoroutine(RestCountdown());
                //StartCoroutine(DestroyCountdown());
            }
        }
    }

    public IEnumerator RestCountdown()
    {
        float currCountdownValue = restingTime;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        isResting = false;
    }


    //public IEnumerator DestroyCountdown()
    //{
    //    float currCountdownValue = destroyTime;
    //    while (currCountdownValue > 0)
    //    {
    //        yield return new WaitForSeconds(1.0f);
    //        currCountdownValue--;
    //    }
    //    fallingObject.gameObject.SetActive(false);
    //}
}
