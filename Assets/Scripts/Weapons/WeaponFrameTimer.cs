using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class WeaponFrameTimer : MonoBehaviour
{
    public float activeInterval;
    public GameObject weaponFrame;
    public float coolDownTimer;
    private float _activeTimer;
    private bool _isActive;
    
    void Start()
    {
        weaponFrame.SetActive(false);
        _isActive = false;
    }

    void Update()
    {
        _activeTimer -= Time.deltaTime;
        if (_activeTimer <= 0)
        {
            _activeTimer = activeInterval;
            if (!_isActive)
            {
                weaponFrame.SetActive(true);
                _isActive = true;
                _activeTimer = activeInterval;
            }
            else
            {
                _activeTimer = coolDownTimer;
                weaponFrame.SetActive(false);
                _isActive = false;
            }
        }
    }
}