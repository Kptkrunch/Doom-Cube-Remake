using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WepsAndAbs : MonoBehaviour
{
    public static WepsAndAbs wepsAndAbs;
    public List<Weapon> equippedWeapons;
    private void Awake()
    {
        wepsAndAbs = this;
    }
}
