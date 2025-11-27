using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    void Start()
    {
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

    }

    void Shoot()
    {
        rb.AddForce(new Vector3(0, 500, 800));
    }
}
