using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponKnockback : MonoBehaviour
{
    public float knockBackAmount;
    public float knockBackDuration;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().KnockBack(knockBackAmount, knockBackDuration);
        }
    }
}

