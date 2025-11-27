using UnityEngine;

public class HitscanShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 5f; // shots per second
    [SerializeField] private KeyCode fireKey = KeyCode.Mouse0;

    [Header("Ray Origin")]
    //If empty, the script will use this GameObject's transform.
    [SerializeField] private Transform firePoint;

    //This one basically indicates what the bullet should hit
    [Header("Layers To Hit")]
    [SerializeField] private LayerMask hitLayers = Physics.DefaultRaycastLayers;

    //Slots for visual effects (Muzzle Flash, impact)
    [Header("Visuals (Optional)")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffectPrefab; // a small spark/decal

    private float _nextTimeToFire = 0f;

    private void Awake()
    {
        // If no firePoint is assigned, use this object. Basically makes object act as the bullet. Don't do this.
        if (firePoint == null)
        {
            firePoint = transform;
        }
    }

    private void Update()
    {
        // Simple automatic fire while key is held
        if (Input.GetKey(fireKey) && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + (1f / fireRate);
            Shoot();
        }
    }

    private void Shoot()
    {
        // Play muzzle flash
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        //casts ray in direction of Z axis, constantly active so long as key is pressed down.
        Vector3 origin = firePoint.position;
        Vector3 direction = firePoint.forward;

        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        // Debug ray in Scene view (Adds visual indicator for raycast)
        Debug.DrawRay(origin, direction * range, Color.red, 0.1f);

        if (Physics.Raycast(ray, out hit, range, hitLayers, QueryTriggerInteraction.Ignore))
        {

            // Try to deal damage to something with a 'Health' component (Specifically deals damage to whatever object it hits)
            Health health = hit.collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            // Spawn impact effect at hit point (Needs to have a prefab on it that is the impact effect. Just drag prefab into slot in object).
            if (impactEffectPrefab != null)
            {
                Instantiate(
                    impactEffectPrefab,
                    hit.point,
                    Quaternion.LookRotation(hit.normal)
                );
            }
        }
    }
}
