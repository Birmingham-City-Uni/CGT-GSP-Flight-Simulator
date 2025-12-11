using UnityEngine;

public class e_missile : MonoBehaviour
{
    public float speed = 20f;
    public float delay= 10f;
    public float rotateSpeed = 5f;
    public Transform target;
    bool Eattack = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Eattack = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        //Debug.Log(Eattack);
        transform.forward = Vector3.Lerp(transform.forward, dir, rotateSpeed * Time.deltaTime);

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            Destroy(gameObject);
            Eattack = false;
            Debug.Log("Enemy Hit");
        }
    }
}
