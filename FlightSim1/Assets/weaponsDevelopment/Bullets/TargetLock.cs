using UnityEngine;

public class TargetLock : MonoBehaviour
{
    public Transform enemy;

    public float detectAngle = 30f;
    public float detectDistance = 1000f;
    public RectTransform hudRect;
    public Camera cam;
    public float lockTime = 3f;
    float currentLockTime = 0f;
    public bool isLocked = false;
    public Transform lockedEnemy = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInHud(enemy))
        {
            currentLockTime = Time.deltaTime;
            if (currentLockTime >= lockTime)
            {
                lockedEnemy = enemy;
                isLocked = true;
            }
            else
            {
                currentLockTime = 0f;
                isLocked = false;
                lockedEnemy = null;
            }
        }

        //bool EnemyInTriangle(Transform enem)
        //{
        //    if(enem == null)
        //    {
        //        return false;
        //    }

        //    Vector3 dir = enem.position - transform.position;
        //    float angle = Vector3.Angle(transform.forward, dir);

        //    return (angle < detectAngle && dir.magnitude < detectDistance);
        //}

        bool IsInHud (Transform enemy)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(enemy.position);
            if (screenPos.z < 0 )
            {
                return false;
            }

            return RectTransformUtility.RectangleContainsScreenPoint(hudRect, screenPos);
        }
    }
}
