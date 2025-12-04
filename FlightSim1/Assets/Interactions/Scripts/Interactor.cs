using UnityEngine;

//interaction interface
interface IInteractable
{
    //function for interaction
    public void Interact();
}
public class Interactor : MonoBehaviour
{
    //Ray cast properties
    public Transform InteractorSource;
    public float InteractRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //On press E key
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Creates ray cast
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            //looks for a hit
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                //if collided object has collision interface
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    //run interact function from object 
                    interactObj.Interact();
                }
            }

        }
    }
}
