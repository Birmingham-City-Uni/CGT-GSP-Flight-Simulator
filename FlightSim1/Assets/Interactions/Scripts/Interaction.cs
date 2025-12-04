using UnityEngine;

public class Interaction : MonoBehaviour, IInteractable
{
    //function that activates on ray cast collision
    public void Interact() 
    {
        Debug.Log("interaction");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
