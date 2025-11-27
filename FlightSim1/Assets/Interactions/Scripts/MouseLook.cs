using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSense = 400f;

    public Transform PlayerCam;

    float xRota = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSense * Time.deltaTime;

        xRota -= mouseY;
        //xRota = Mathf.Clamp(xRota, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRota, 0f, 0f);

        PlayerCam.Rotate(Vector3.up * mouseX);
    }
}