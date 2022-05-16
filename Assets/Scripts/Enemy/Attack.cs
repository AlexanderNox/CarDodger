using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Health>() != false)
        {
            col.GetComponent<Health>().TakeDamage(_damage);
        }
    }
}
