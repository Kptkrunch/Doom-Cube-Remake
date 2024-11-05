using UnityEngine;

namespace Weapons.WeaponModifiers
{
    public class HasLifeTime : MonoBehaviour
    {
        public float lifeTimer;

        public void DisableAfterLifeTime()
        {
            lifeTimer -= Time.deltaTime;
            if (lifeTimer <= 0) gameObject.SetActive(false);
        }
    }
}