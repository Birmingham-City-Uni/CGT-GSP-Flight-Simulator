using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullets;
    public GameObject missile;
    public GameObject blast;
    public Transform shootPoint;
    public Transform shootPoint_M;
    public TargetLock TargetLock;
    public float fireRate = 0.01f;
    public float fireRate_M = 0.01f;
    public Transform Enemy;
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
            if (TargetLock.isLocked)
            {
                ShootMissile(TargetLock.lockedEnemy);
                nextFireTime_M = Time.time + fireRate_M;
            }
                
        }
    }

    void Shoot()
    {
        GameObject m = Instantiate(bullets, shootPoint.position,shootPoint.rotation);
       
        Instantiate(blast, shootPoint.position, Quaternion.identity);
    }

    void ShootMissile(Transform target)
    {
        Debug.Log("MISSILE FIRED!");
        Instantiate(blast, shootPoint_M.position, Quaternion.identity);
        GameObject m = Instantiate(missile, shootPoint_M.position, shootPoint_M.rotation);
        m.transform.LookAt(target);
        m.GetComponent<Missile>().target = target;
    }
}
