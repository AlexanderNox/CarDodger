using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private int _scorePenalty;

    private void OnValidate()
    {
        if (_scorePenalty > 0)
        {
            _scorePenalty = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Fuel>() != false)
        {
            col.GetComponent<Fuel>().TakeFuel(_damage);
            col.GetComponent<ScoreCounter>().ChangeScore(_scorePenalty);
        }
    }
}
