using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _health;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
