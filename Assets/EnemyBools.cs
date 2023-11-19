using System;
using UnityEngine;

public class EnemyBools : MonoBehaviour
{
    public bool deathRay;

    private void Awake()
    {
        deathRay = false;
    }

    private void OnEnable()
    {
        deathRay = false;
    }
}
