using System;
using Controllers.Pools;
using UnityEngine;

public class TechActivator : MonoBehaviour
{
    public static TechActivator techActivator;
    private float _timer = .2f;
    public int techId;

    private void Awake()
    {
        techActivator = this;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        GetTechFromPool();
    }

    private void GetTechFromPool()
    {
        var tech = TechPools.pools.techList[techId].GetPooledGameObject();
        tech.SetActive(true);
        tech.transform.position = transform.position;
    }
}
