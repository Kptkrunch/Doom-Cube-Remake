using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : MonoBehaviour
{
    public int critterLevel;
    public Rigidbody2D rb2d;
    public CircleCollider2D aggroField;
    public CritterAi ai;
    public List<CritterStats> stats;

    private void Start()
    {
        ai.aggroArea.radius = stats[critterLevel].aggroRange;
        ai.attackRange = stats[critterLevel].range;
    }
}

[System.Serializable]
public class CritterStats
{
    public float health, damage, speed, range, aggroRange, rateOfFire, lifeTime;
}
