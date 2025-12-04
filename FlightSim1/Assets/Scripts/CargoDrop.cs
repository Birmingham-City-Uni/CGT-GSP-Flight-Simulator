using UnityEngine;

public class CargoDrop : MonoBehaviour
{
    [Header("Cargo Settings")]
    public GameObject Cargo;
    public float Offset = 0.0f;
    void DropCargo()
    {
        Debug.Log("Dropped Cargo");

        var CargoOffset = new Vector3(0, Offset, 0);
        Instantiate(Cargo, transform.position - CargoOffset, Quaternion.identity);

    }

    //TO DO LIST:
    //
    // CARGO DROPPING + PARACHUTE
    // SHOOTING
    //

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("FIRE2");
            DropCargo();
        }
    }
}
