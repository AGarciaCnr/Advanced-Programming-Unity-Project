using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Character
{
    [System.Serializable]
    public class EnemyDeathEvent : UnityEvent<Enemy> { }

    public EnemyDeathEvent onEnemyDeath = new EnemyDeathEvent();

    int _points = 0;
    public Enemy(int maxHealth, int points) : base(maxHealth)
    {
        this._points = points;
    }

    public int Points
    {
        get { return _points; }
        set { _points = value; }
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
        onEnemyDeath.Invoke(this);
    }

    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        GameObject enemy = PoolManager.Spawn(this.gameObject, position, rotation);
        enemy.GetComponent<Enemy>().onEnemyDeath.AddListener(GameManager.instance.OnEnemyDeath);
        return enemy;
    }
}
