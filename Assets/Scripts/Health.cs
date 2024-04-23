using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;

    public event Action OnHurt;
    public event Action OnDie;

    private bool isDead;

    public void TakeDamage(int damageTakenValue)
    {
        health -= damageTakenValue;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if(health <= 0 && isDead == false)
        {
            isDead = true;
            OnDie?.Invoke();
        }
        else
        {
            OnHurt?.Invoke();
        }
    }
}
