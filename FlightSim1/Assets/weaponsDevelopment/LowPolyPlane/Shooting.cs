using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullets;
    public GameObject missile;
    public Transform shootPoint;
    public Transform shootPoint_M;
    public float fireRate = 0.01f;
    public float fireRate_M = 0.01f;

    private float nextFireTime = 0f;
    private float nextFireTime_M = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.Space)  && Time.time >= nextFireTime_M)
        {
            ShootMissile();
            nextFireTime_M = Time.time + fireRate_M;

        }
    }

    void Shoot()
    {
        Instantiate(bullets, shootPoint.position,shootPoint.rotation);
    }

    void ShootMissile()
    {
        Instantiate(missile, shootPoint_M.position, shootPoint_M.rotation);
    }
}
