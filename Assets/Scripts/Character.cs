using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    int MaxHealth { get; set; }
    int Health { get; set; }

    void TakeDamage(int damage);
    void Heal(int healAmount);
    void Die();
}

public abstract class Character : MonoBehaviour, IHealth
{
    private int _maxHealth;
    private int _health;

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public Character(int maxHealth = 100)
    {
        this._maxHealth = maxHealth;
        this._health = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }
    
    public void Heal(int healAmount)
    {
        _health += healAmount;
    }

    public virtual void Die()
    {
        Debug.Log(this + " has died.");
    }
}
