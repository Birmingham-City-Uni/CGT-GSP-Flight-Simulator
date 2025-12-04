using UnityEngine;

public class Cargo : MonoBehaviour
{
    
    public float FallSpeed;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - FallSpeed, transform.position.z);
    }
}
