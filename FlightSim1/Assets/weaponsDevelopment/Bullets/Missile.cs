using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed_M = 40f;
    public float delay_M = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, delay_M);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed_M * Time.deltaTime);
    }
}
