using System.ComponentModel;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    public Transform enemy;

    public float detectAngle = 60f;
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
            currentLockTime += Time.deltaTime;

            if (currentLockTime >= lockTime)
            {
                if (!isLocked)
                {
                    isLocked = true;
                    lockedEnemy = enemy;
                    Debug.Log("Target LOCKED!");
                }
            }
        }
        else
        {
            
            currentLockTime = 0f;
            isLocked = false;
            lockedEnemy = null;
        }

        bool IsInHud (Transform enemy)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(enemy.position);
            //Debug.Log("Enemy screen pos = " + screenPos);
            if (screenPos.z < 0 )
            {
                return false;
            }

            bool inside = RectTransformUtility.RectangleContainsScreenPoint(hudRect, screenPos);
            //Debug.Log("Inside HUD? " + inside);
            return inside;
        }
    }
}
