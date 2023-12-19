using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Player() : base(150)
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    override public void Die()
    {
        base.Die();
        Debug.Log("Game Over");
        
    }
}
