using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    const int maxHealth = 50;
    const int points = 40;
    public Tank() : base(maxHealth, points)
    {

    }
}
