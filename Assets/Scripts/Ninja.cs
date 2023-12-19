using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Enemy
{
    const int maxHealth = 20;
    const int points = 20;
    public Ninja() : base(maxHealth, points)
    {

    }
}
