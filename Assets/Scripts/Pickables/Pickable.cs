using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickable : MonoBehaviour
{
    public float pickableTime = 5;
    public float timeToRegenerate = 5;
    protected int boostType;


    protected MeshRenderer meshRenderer;
    protected SphereCollider objectCollider;
    protected GameManager gameManager;

    public abstract void OnTriggerWithPlayer(PlayerController player);
    
    public abstract void ResetPlayer(PlayerController player);
    


    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        objectCollider = GetComponent<SphereCollider>();
        gameManager = GameManager.Instance;
    }


    public IEnumerator CountdownToReset(PlayerController player)
    {
        float currCountdownValue = pickableTime;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        ResetPlayer(player);

        Vector2 pickable2DLocation = new Vector2(transform.position.x, transform.position.z);
        gameManager.ListOfOccupiedSpawners.Remove(pickable2DLocation);

        if (timeToRegenerate >= 0)
        {
            StartCoroutine(CountdownToRegenerate());
        }
    }

    public IEnumerator CountdownToRegenerate()
    {
        float currCountdownValue = timeToRegenerate;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        RelocatePickable();

        meshRenderer.enabled = true;
        objectCollider.enabled = true;

    }

    public void RelocatePickable()
    {
        int size = gameManager.ListOfAllSpawners.Count;
        int arrayPos = Random.Range(0, size);

        if (gameManager.ListOfOccupiedSpawners.Contains(gameManager.ListOfAllSpawners[arrayPos]))
        {
            RelocatePickable();
        }
        else
        {
            Vector3 spawnerPos = new Vector3(gameManager.ListOfAllSpawners[arrayPos].x, 1, gameManager.ListOfAllSpawners[arrayPos].y);
            this.transform.position = spawnerPos;
            gameManager.ListOfOccupiedSpawners.Add(gameManager.ListOfAllSpawners[arrayPos]);
        }
    }
}

