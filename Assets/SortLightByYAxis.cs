using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class Light2DPositioning : MonoBehaviour
{
    public Light2D light2D; // Reference to the Light2D component
    public float offset = 0.1f; // Offset to adjust the light position

    void Start()
    {
        if (light2D == null)
        {
            light2D = GetComponent<Light2D>();
        }
    }

    void Update()
    {
        HandleLightPosition();
    }

    void HandleLightPosition()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(light2D.transform.position);

        foreach (var col in colliders)
        {
            if (col.gameObject != gameObject) // Ignore self
            {
                float colYPosition = col.transform.position.y;
                float lightYPosition = light2D.transform.position.y;

                if (lightYPosition < colYPosition)
                {
                    // Light appears in front of the sprite
                    light2D.transform.position = new Vector3(light2D.transform.position.x, lightYPosition - offset, light2D.transform.position.z);
                }
                else
                {
                    // Light appears behind the sprite
                    light2D.transform.position = new Vector3(light2D.transform.position.x, lightYPosition + offset, light2D.transform.position.z);
                }
            }
        }
    }
}