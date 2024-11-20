using System.Collections;
using Damagers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons.SpecificWeapons
{
    public class BurnWeaponMelee : MeleeWeapon
    {
        public EDotDamager eDotDamager;
        public GameObject burnerFrame;
        public GameObject burner;

        protected bool Grow;

        private void Start()
        {
            SetStats();
        }

        private void FixedUpdate()
        {
            WeaponFacing();
            if (CanFire) StartCoroutine(AttackLoop());
        }

        protected IEnumerator AttackLoop()
        {
            CanFire = false;
            MusicManager.Instance.sfxPlayerProjectiles.FeedbacksList[stats.pid].Play(transform.position);
            burner.transform.localScale = Vector3.Lerp(burner.transform.localScale,
                stats.weaponLvls[stats.lvl].size, 2.5f);
            burnerFrame.gameObject.SetActive(true);
            yield return new WaitForSeconds(AttackDuration);
            burner.transform.localScale = Vector3.Lerp(burner.transform.localScale, Vector3.zero, 2.5f);
            yield return new WaitForSeconds(1.5f);
            burnerFrame.SetActive(false);
            burner.transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(Cooldown);
            CanFire = true;
        }

        protected void WeaponFacing()
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                burnerFrame.transform.localScale =
                    new Vector2(1, transform.localScale.y);
                eDotDamager.transform.rotation = Quaternion.identity;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                burnerFrame.transform.localScale =
                    new Vector2(-1, transform.localScale.y);
                eDotDamager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
        }

        private void SetStats()
        {
            CanFire = true;
            RateOfFire = stats.weaponLvls[stats.lvl].rateOfFire;
            AttackDuration = stats.weaponLvls[stats.lvl].duration;
            Cooldown = stats.weaponLvls[stats.lvl].coolDown;
            eDotDamager.damageInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            eDotDamager.damage = stats.weaponLvls[stats.lvl].damage;
        }

        public override void UpdateWeapon()
        {
            WeaponLevelUp();

            RateOfFire = stats.weaponLvls[stats.lvl].rateOfFire;
            AttackDuration = stats.weaponLvls[stats.lvl].duration;
            Cooldown = stats.weaponLvls[stats.lvl].coolDown;

            transform.localScale = Vector3.one * stats.weaponLvls[stats.lvl].range;
            eDotDamager.damageInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            eDotDamager.damage = stats.weaponLvls[stats.lvl].damage;
        }
    }
}