using UnityEngine;

public class bullets:MonoBehaviour
{
    public float speed = 100f;
    public float delay = 20f;

    void Start()
    {
        Destroy(gameObject, delay);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
