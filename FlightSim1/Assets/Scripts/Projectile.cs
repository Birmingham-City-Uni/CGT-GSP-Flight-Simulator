using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        ProjectileFade();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.DealDamage();   // Call the method that deals damage
        }
        Destroy(this.gameObject);
    }
    // When the projectile is too far away.
    void ProjectileFade()
    {
        float dist = Vector3.Distance(transform.position, startPos);
        if (dist > 100)
        {
            Destroy(this.gameObject);
        }
    }
}
