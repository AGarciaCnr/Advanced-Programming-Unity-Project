using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public Enemy(int maxHealth) : base(maxHealth)
    {
        
    }

    override public void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        Debug.Log("Enemy took " + damage + " damage.");
    }

    override public void Die()
    {
        base.Die();
        PoolManager.Despawn(gameObject);
    }
}
