using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MutantsAndRobots
{
    public class MutantSlug : MonoBehaviour
    {
        public float lifeSpan, moveSpeed, moveInterval, attackInterval;
        public GameObject attack, spriteAndAnimator;
        public Animator animator;
        public SpriteRenderer spriteRenderer;
        public Sprite pukingSprite, crawlingSprite;
        private Vector2 _direction;
        private float _lifeTimer;

        private void Start()
        {
            StartCoroutine(Wander());
            StartCoroutine(ProjectilePuke());
        }

        private void FixedUpdate()
        {
            _lifeTimer += Time.deltaTime;
            if (_lifeTimer >= lifeSpan)
            {
                _lifeTimer = 0;
                gameObject.SetActive(false);
            }
            
            transform.Translate(_direction * (moveSpeed * Time.deltaTime));
            if (!attack.gameObject.activeInHierarchy) spriteRenderer.sprite = crawlingSprite;
        }

        private IEnumerator Wander()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(moveInterval);
                GetRandomDirection();
            }
        }

        private IEnumerator ProjectilePuke()
        {
            while (gameObject.activeSelf)
            {
                Attack();
                yield return new WaitForSeconds(attackInterval);
            }
        }

        private void Attack()
        {
            attack.gameObject.SetActive(true);
            spriteRenderer.sprite = pukingSprite;
        }

        private void GetRandomDirection()
        {
            var direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 1f);
            MaybeFlipSelf(direction);
            _direction = direction;
        }

        private void MaybeFlipSelf(Vector3 direction)
        {
            if (direction.x < 0)
            {
                var scale = spriteAndAnimator.gameObject.transform.localScale;
                scale = new Vector3(
                    -scale.x,
                    scale.y, 1f);
                spriteAndAnimator.gameObject.transform.localScale = scale;
                var transform1 = transform;
                var localScale = transform1.localScale;
                localScale = new Vector3(-1, localScale.y, localScale.z);
                transform1.localScale = localScale;
            }

            if (direction.x > 0)
            {
                var scale = spriteAndAnimator.gameObject.transform.localScale;
                scale = new Vector3(
                    Math.Abs(scale.x),
                    scale.y, 1f);
                spriteAndAnimator.gameObject.transform.localScale = scale;
                var transform1 = transform;
                var localScale = transform1.localScale;
                localScale = new Vector3(1, localScale.y, localScale.z);
                transform1.localScale = localScale;
            }
        }

        private void OnDisable()
        {
            StopCoroutine(Wander());
        }
    }
}