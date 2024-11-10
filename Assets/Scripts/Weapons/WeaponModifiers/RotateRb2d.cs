using UnityEngine;

namespace Weapons.WeaponModifiers
{
    public class RotateRb2d : MonoBehaviour
    {
        public Rigidbody2D rb2d;
        public float rotationSpeed;

        public void RotateParentRb2d()
        {
            transform.rotation = Quaternion.Euler(0f, 0f,
                transform.rotation.eulerAngles.z + rotationSpeed * 360f * Time.deltaTime * Mathf.Sign(rb2d.velocity.x));
        }
    }
}