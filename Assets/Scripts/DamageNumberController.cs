using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController dnController;

    private void Awake()
    {
        dnController = this;
    }

    public DamageNumber damageNumber;
    public Transform damageNumberTransform;

    public void ShowDamage(float damage, Vector3 location)
    {
        int rounded = Mathf.RoundToInt(damage);
        DamageNumber newDamage = Instantiate(damageNumber, location, Quaternion.identity, damageNumberTransform);
        
        newDamage.Setup(rounded);
        newDamage.gameObject.SetActive(true);
    }
}
