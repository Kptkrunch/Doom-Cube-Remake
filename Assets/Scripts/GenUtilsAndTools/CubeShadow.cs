using Controllers;
using UnityEngine;

public class CubeShadow : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private void Update()
    {
        if (PlayerController.contPlayer.rb2d.velocity.x < 0) spriteRenderer.flipX = false;
        if (PlayerController.contPlayer.rb2d.velocity.x > 0) spriteRenderer.flipX = true;
    }
}
