using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] GameObject bomb;
    [SerializeField] InputSystem_Actions input;
    bool bombSpawned = false;

    private void Awake()
    {
        input = new InputSystem_Actions();
        input.Bomb.Enable();
    }

    void Update()
    {
        if (input.Bomb.Space.triggered)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        if (!bombSpawned)
        {
            bombSpawned = true;
            StartCoroutine(Spawner());
        }
    }

    IEnumerator Spawner()
    {
         GameObject newBomb = Instantiate(bomb);
         newBomb.transform.position = gameObject.transform.position;
         yield return new WaitForSeconds(1f);
         GameObject newBomb2 = Instantiate(bomb);
         newBomb.transform.position = gameObject.transform.position;
         yield return new WaitForSeconds(1f);
         GameObject newBomb3 = Instantiate(bomb);
         newBomb.transform.position = gameObject.transform.position;
    }
}
