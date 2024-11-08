using Controllers;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Serialization;

namespace TechSkills
{
    public class TurretBottomSectionTech : Tech
    {
        private static readonly int GreyscaleBlend = Shader.PropertyToID("_GreyscaleBlend");
        public int pid;
        public float rotationSpeed = 10f, fireRate = 1f, range = 10f;
        public float currentHealth, maxHealth;

        public TurretTopSectionTech turretTop;
        public Rigidbody2D rb2d;
        private CircleCollider2D _circleCollider;
        private Transform _target;
        
        private float _nextFire, _percentDamaged;
        
        private void Start()
        {
            _circleCollider = GetComponent<CircleCollider2D>();
            _circleCollider.radius = range;
        }

        private void Update()
        {
            MaybeUpdateTurretTopSection();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy")) _target = collision.transform;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy")) _target = null;
        }
        
        public override void TakeDamage(float damage)
        {
            currentHealth -= damage;
            _percentDamaged = 1 - (currentHealth / maxHealth);
            
            ShowDamage(damage, 1f);
            UpdateShaderGreyscaleBlend();
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                // get destruction particle
            }
        }

        private void ShowDamage(float damage, float intensity = 1f)
        {
            var floatingText = TechDamageNumberController.ContTechDmgNum.player.GetFeedbackOfType<MMF_FloatingText>();
            var damageString = damage.ToString();
            floatingText.Value = damageString;

            if (rb2d)
            {
                if (TechDamageNumberController.ContTechDmgNum != null)
                    TechDamageNumberController.ContTechDmgNum.player.PlayFeedbacks(transform.position);
            }
        }

        private void UpdateShaderGreyscaleBlend()
        {
            if (spriteRenderer != null && _percentDamaged > 0f)
            {
                spriteRenderer.sharedMaterial.SetFloat(GreyscaleBlend, _percentDamaged);
            }
        }
        
        public override void ResetOrInitTechObject()
        {
            currentHealth = maxHealth;
            _percentDamaged = 0f;
            _target = null;
            _nextFire = Time.time + fireRate;
            UpdateShaderGreyscaleBlend();
            if (turretTop) turretTop.UpdateShaderGreyscaleBlend();
        }
        
        private void Fire()
        {
            var projectile = TechProjectilePools.ProjectilePools.GetProjectileById(pid);
            projectile.SetActive(true);
            projectile.transform.rotation = turretTop.transform.rotation;
            projectile.transform.position = turretTop.transform.position;
        }

        private void MaybeUpdateTurretTopSection()
        {
            if (_target == null) return;
            var direction = _target.position - turretTop.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            var targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            turretTop.transform.rotation = Quaternion.Slerp(turretTop.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Time.time > _nextFire)
            {
                Fire();
                _nextFire = Time.time + fireRate;
            }
        }
    }
}