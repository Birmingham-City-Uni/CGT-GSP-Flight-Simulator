using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //Mouse sensitivty
    public float MouseSense = 400f;

    //Object the camera rotates around
    public Transform PlayerCam;

    //Holds the players roataion
    float xRota = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Locks the player curser
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //When the C key is held down the player can move the camaera
        if (Input.GetKey(KeyCode.C))
        {
            //Gets the inputs from the mouse and multiplys by preset sensitivty and applies it over time  
            float mouseX = Input.GetAxis("Mouse X") * MouseSense * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSense * Time.deltaTime;

            //Prevents the player from looking too far up or down 
            xRota -= mouseY;
            xRota = Mathf.Clamp(xRota, -90f, 90f);

            //Applies the transformation
            transform.localRotation = Quaternion.Euler(xRota, 0f, 0f);
            PlayerCam.Rotate(Vector3.up * mouseX);
        }

        //When the C key is released it snaps the view back to fordward facing 
        if (Input.GetKeyUp(KeyCode.C))
        {
            //Applies transformation
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            PlayerCam.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}