using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;

    void Update()
    {
        float rotationInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationInput = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationInput = 1f;
        }

        // Rotate around Z axis
        transform.Rotate(0f, 0f, rotationInput * rotationSpeed * Time.deltaTime);
    }
}