using ScriptableObjs.TechWeaponsSOS;
using UnityEngine;

namespace TechSkills
{
    public class TurretTopSectionTech : MonoBehaviour
    {
        private static readonly int GreyscaleBlend = Shader.PropertyToID("_GreyscaleBlend");
        public TechWeaponData turretWeaponData;
        public TurretBottomSectionTech turretBottom;
        public SpriteRenderer spriteRenderer;
        public float percentDamaged;
        private bool _baseHasBeenInitialized = false;
        private void Start()
        {
            ResetOrInitTurretTopObject();
            ResetOrInitTurretBaseObject();
        }

        private void Awake()
        {
            percentDamaged = 0f;

            if (!_baseHasBeenInitialized)
            {
                ResetOrInitTurretBaseObject();
            }
        }
        
        public void UpdateShaderGreyscaleBlend()
        {
            if (spriteRenderer != null && percentDamaged > 0f)
            {
                spriteRenderer.sharedMaterial.SetFloat(GreyscaleBlend, percentDamaged);
            }
        }


        private void ResetOrInitTurretBaseObject()
        {
            if (!turretBottom) return;
            turretBottom.fireRate = turretWeaponData.fireRate;
            turretBottom.range = turretWeaponData.range;
            turretBottom.pid = turretWeaponData.pid;
            turretBottom.rotationSpeed = turretWeaponData.rotationSpeed;
            _baseHasBeenInitialized = true;
        }

        private void ResetOrInitTurretTopObject()
        {
            percentDamaged = 0f;
            _baseHasBeenInitialized = false;
        }
    }
}
