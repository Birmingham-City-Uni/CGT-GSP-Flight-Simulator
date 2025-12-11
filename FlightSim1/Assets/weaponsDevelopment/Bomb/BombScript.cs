using System;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject explosionFX;
    [SerializeField] MeshRenderer meshRenderer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 3)
        {
            meshRenderer.enabled = false;
            rb.angularVelocity = Vector3.zero;
            explosionFX.SetActive(true);
        }
    }
}
