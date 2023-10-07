using UnityEngine;

public class FlipX : MonoBehaviour
{
    public Rigidbody2D rb2d;

    private void FixedUpdate()
    {
        var velocity = rb2d.velocity;
        if (velocity.x > 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }

        if (velocity.x < 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }
}
