using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttractor : MonoBehaviour
{
    public static ItemAttractor itemAttractor;
    public float pickupRadius;
    public float pullSpeed;
    public CircleCollider2D pickupArea;
    private void Awake()
    {
        itemAttractor = this;
        pickupArea.radius = pickupRadius;
    }
}
