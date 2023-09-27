using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeProjectileScale : MonoBehaviour
{
    public Vector3 maxSize;
    private Vector3 _targetSize;

    public float growShrinkSpeed;

    public float staySizeInterval;
    private float _staySizeTimer;
    
    void Start()
    {
        _targetSize = maxSize;
        transform.localScale = Vector3.zero;
        _staySizeTimer = staySizeInterval;
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        _targetSize = maxSize;
        _staySizeTimer = staySizeInterval;
    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, _targetSize, growShrinkSpeed * Time.deltaTime);
        _staySizeTimer -= Time.deltaTime;
        if (_staySizeTimer <= 0)
        {
            _targetSize = Vector3.zero;
        }
    }
}
