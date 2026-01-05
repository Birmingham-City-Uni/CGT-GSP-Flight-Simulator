using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkHandler : MonoBehaviour
{
    // https://www.youtube.com/watch?v=QJOi8Jyn5ck
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] chunkParents;
    [SerializeField] float activationDistance = 7000.0f;
    [SerializeField] float checkInterval = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Calls every second
        StartCoroutine(CheckChunkDistance());
    }

    IEnumerator CheckChunkDistance()
    {
        while (true)
        {
            foreach (GameObject chunkParent in chunkParents)
            {
                float distanceToPlayer = Vector3.Distance(player.transform.position, chunkParent.transform.position);

                if (distanceToPlayer <= activationDistance)
                {
                    chunkParent.SetActive(true);
                }
                else
                {
                    chunkParent.SetActive(false);
                }
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }
}
