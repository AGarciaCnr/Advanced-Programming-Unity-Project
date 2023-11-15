using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    void TakeDamage(int damage);
    void Heal(int healAmount);
    void Die();
}

public abstract class Character : MonoBehaviour, IHealth
{
    private int maxHealth;
    private int health;

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public Character(int maxHealth = 100)
    {
        this.maxHealth = maxHealth;
        this.health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    
    public void Heal(int healAmount)
    {
        health += healAmount;
    }

    public virtual void Die()
    {
        Debug.Log(this + " has died.");
    }
}
