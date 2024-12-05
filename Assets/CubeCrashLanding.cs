using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using MoreMountains.Feedbacks;
using UnityEngine;

public class CubeCrashLanding : MonoBehaviour
{
    private static readonly int GradBoostY = Shader.PropertyToID("_GradBoostY");
    private static readonly int FadeAmount = Shader.PropertyToID("_FadeAmount");
    public SpriteRenderer actualSprite, skySprite;
    public MMFeedbacks player;
    public GameObject explosionParticle, fallingObject;
    public int eid, fallingTime, fadeAmountByTime, startDelay;
    private bool _hasCrashed;
    private float _delayTimer, _fallingTimer, _fadeAmountByTimer;

    private void Awake()
    {
        ResetSprites();
        _delayTimer = 0f;
        _fallingTimer = 0f;
        _fadeAmountByTimer = 0f;
        _delayTimer = startDelay;
        skySprite.sharedMaterial.SetFloat(FadeAmount, 0f);
        skySprite.sharedMaterial.SetFloat(GradBoostY, .1f);
    }

    private void Start()
    {
        player.PlayFeedbacks();
        fallingObject.SetActive(true); 
        // MusicManager.Instance.musicPlayer.FeedbacksList[1].Play(transform.position);
    }

    private void FixedUpdate()
    {
        if (!PlayerController.contPlayer.allowedControl) PlayerController.contPlayer.allowedControl = _hasCrashed;

        if (_fallingTimer < fallingTime && !_hasCrashed)
        {
            _fallingTimer += Time.fixedDeltaTime;
            var gradBoostY = _fallingTimer / fallingTime;
            skySprite.sharedMaterial.SetFloat(GradBoostY, gradBoostY);
        }
        
        if (_fallingTimer >= fallingTime)
        {
            if (_fadeAmountByTimer < fadeAmountByTime)
            {
                _fadeAmountByTimer += Time.fixedDeltaTime;
                var fadeAmount = _fadeAmountByTimer / 2;
                skySprite.sharedMaterial.SetFloat(FadeAmount, fadeAmount);
            }
        }
        
        if (_fadeAmountByTimer > fadeAmountByTime)
        {
            if (_delayTimer < startDelay) _delayTimer += Time.fixedDeltaTime;
        }
        
        if (_delayTimer > startDelay)
        {
            if (!_hasCrashed) return;
            PlayerController.contPlayer.allowedControl = true;

            fallingObject.SetActive(false);
        }

        if (!_hasCrashed && !player.IsPlaying)
        {
            _hasCrashed = true;
        } 
        
        if (!player.IsPlaying && _hasCrashed)
        {
            if (explosionParticle.activeInHierarchy) return;            
            actualSprite.gameObject.SetActive(true);
            fallingObject.SetActive(false);

            explosionParticle.SetActive(true);
            // MusicManager.Instance.sfxPlayerProjectiles2.FeedbacksList[eid].Play(transform.position);
        }
    }

    private void ResetSprites()
    {
        fallingObject.SetActive(true);
        actualSprite.gameObject.SetActive(false);
    }
}
