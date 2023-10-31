using System.Collections;
using Controllers.Pools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class MineSlayer : Weapon
    {
        private float _fireInterval, _reloadInterval, _ammo;
        private bool _canDropMines;

        private void Start()
        {
            _canDropMines = true;
            SetStats();
        }

        private void FixedUpdate()
        {
            if (_canDropMines)
            {
                StartCoroutine(LayMines());
            }
        }

        IEnumerator LayMines()
        {
            _canDropMines = false;
            for (int i = 0; i < _ammo; i++)
            {
                yield return new WaitForSeconds(_fireInterval);
                DropMine();
            }
            yield return new WaitForSeconds(_reloadInterval);
            _canDropMines = true;
        }
    
        private void DropMine()
        {
            var mine = ProjectilePoolManager.poolProj.projPools[4].GetPooledGameObject();
            mine.transform.position = transform.position;
            mine.SetActive(true);
        }

        private void SetStats()
        {
            _fireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            _reloadInterval = stats.weaponLvls[stats.lvl].coolDown;
            _ammo = stats.weaponLvls[stats.lvl].ammo;
        }   
    }
}
