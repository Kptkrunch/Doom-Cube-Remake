using System.Collections;
using Controllers;
using UnityEngine;

public class AnyAttack : MonoBehaviour
{
    public float damage, hits, hitsTimer;
    public ParticleSystem attackParticleSystem;
    public GameObject parent;
    private EnemyController _enemyController;

    private void FixedUpdate()
    {
        if (!attackParticleSystem.isPlaying)
        {
            parent.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _enemyController = collision.GetComponent<EnemyController>();
            StartCoroutine(SpecAttack(_enemyController));
        }
    }

    private IEnumerator SpecAttack(EnemyController enemy)
    {            
        attackParticleSystem.Play();
        for (int i = 0; i < hits; i++)
        {
            yield return new WaitForSeconds(hitsTimer);
            enemy.TakeDamage(damage);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(SpecAttack(_enemyController));
    }
}
