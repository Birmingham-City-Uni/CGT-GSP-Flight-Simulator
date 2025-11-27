using UnityEngine;

public class Health : MonoBehaviour
{
    //max helth modifier in inspector.
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    //activates each time object takes damage
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Max(_currentHealth, 0f);

        UpdateScale();

        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    //kills game object if health is zero
    private void Die()
    {

        Destroy(gameObject);
    }

    // Scale object based on remaining health
    private void UpdateScale()
    {
        float healthPercent = Mathf.Clamp01(_currentHealth / maxHealth);

        
        transform.localScale = Vector3.one * healthPercent;
    }
}
