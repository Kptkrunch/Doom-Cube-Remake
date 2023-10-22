using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mutant : MonoBehaviour
{
    public int health = 1;
    public float moveSpeed;
    public int moveInterval;
    public int attackInterval;
    public GameObject attack;
    public Sprite mutantSprite;

    private Vector2 _direction;

    private void Start()
    {
        StartCoroutine(Wander());
        StartCoroutine(ProjectilePuke());
    }

    private void FixedUpdate()
    {
        transform.Translate(_direction * (moveSpeed * Time.deltaTime));

        if (health <= 0)
        {
            Debug.Log("health missing");
        }
    }

    IEnumerator Wander()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(moveInterval);
            GetRandomDirection();
            if (_direction.x < 0)
            {
                var transform1 = transform;
                var localScale = transform1.localScale;
                localScale = new Vector3(-1, localScale.y, localScale.z);
                transform1.localScale = localScale;
            }

            if (_direction.x > 0)
            {
                var transform1 = transform;
                var localScale = transform1.localScale;
                localScale = new Vector3(1, localScale.y, localScale.z);
                transform1.localScale = localScale;
            }

        }
    }

    IEnumerator ProjectilePuke()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(attackInterval);
            Attack();
        }

    }

    private void Attack()
    {
        attack.gameObject.SetActive(true);
    }

    private void GetRandomDirection()
    {
        var direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 1f).normalized;
        _direction = direction;
        Debug.Log(_direction);
        Debug.DrawRay(transform.position, _direction);
    }

    private void OnDisable()
    {
        StopCoroutine(Wander());
    }
}
