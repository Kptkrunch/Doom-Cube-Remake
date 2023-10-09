using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterAi : MonoBehaviour
{
    public CircleCollider2D aggroArea;
    public Attack attack;
    // public FollowPlayer follow;
    public float aggroRadius, attackRange;
    public bool enemies, obstacles, civilians, vehicles, catchingUp;

    private void Move()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Vector3.MoveTowards(transform.position, collision.transform.position, aggroRadius);
        }
    }
}
