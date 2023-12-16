using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Warrior : Enemy
{
    const int maxHealth = 50;
    const int points = 10;
    public Warrior() : base(maxHealth, points)
    {
        
    }
}