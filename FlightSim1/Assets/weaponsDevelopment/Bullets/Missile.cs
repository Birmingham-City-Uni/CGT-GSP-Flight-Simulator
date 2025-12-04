using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed_M = 40f;
    public float delay_M = 10f;
    public Transform target;
    public GameObject blast;
    public GameObject blast2;
    public GameObject blast3;
    //public GameObject blast4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Destroy(gameObject, delay_M);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * speed_M * Time.deltaTime);
        transform.position += transform.forward * speed_M * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Is Hit!");
            if (blast != null)
            {
                Instantiate(blast, transform.position, Quaternion.identity);
                Instantiate(blast2, transform.position, Quaternion.identity);
                Instantiate(blast3, transform.position, Quaternion.identity);
            }

            // optional: disable missile or destroy ONLY the missile
            Destroy(gameObject);
        }
    }
}
