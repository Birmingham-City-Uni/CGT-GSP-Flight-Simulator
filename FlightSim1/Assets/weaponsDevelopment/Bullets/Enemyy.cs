using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemyy : MonoBehaviour
{
    public GameObject missile;
    public Transform shootPoint;
    public float fireRate = 0.01f;
    public Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Enemy Fired");
            GameObject m = Instantiate(missile, shootPoint.position, shootPoint.rotation);
            m.GetComponent<e_missile>().target = player;
        }
    }
}
