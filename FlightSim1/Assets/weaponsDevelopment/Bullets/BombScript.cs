using System;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject explosionFX;
    [SerializeField] MeshRenderer meshRenderer;

    void Start()
    {
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

    }

    void Shoot()
    {
        rb.AddForce(new Vector3(0, 300, 500));
    }

    private void OnCollisionEnter(Collision collision)
    {
        meshRenderer.enabled = false;
        rb.angularVelocity = Vector3.zero;
        explosionFX.SetActive(true);
    }
}
