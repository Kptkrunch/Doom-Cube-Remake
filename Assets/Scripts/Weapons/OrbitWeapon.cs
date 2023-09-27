using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitWeapon : MonoBehaviour
{
    public float rotationSpeed;
    public Transform projectileFrame;

    void Start()
    {
        
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, projectileFrame.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));
    }
}
