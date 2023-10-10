using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float attackDistance = 2f;
    public float retreatDistance = 5f;

    private Transform playerTransform;
    private Transform enemyTransform;
    private GameObject currentBunker;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyTransform = transform;
    }

    void Update()
    {
        if (currentBunker == null)
        {
            // Move towards the player
            enemyTransform.position += (playerTransform.position - enemyTransform.position).normalized * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Take cover in the bunker
            enemyTransform.position += (currentBunker.transform.position - enemyTransform.position).normalized * moveSpeed * Time.deltaTime;
        }

        float distanceToPlayer = Vector3.Distance(enemyTransform.position, playerTransform.position);

        if (distanceToPlayer <= attackDistance)
        {
            // Attack the player
            Debug.Log("Attacking player");
        }
        else if (distanceToPlayer > retreatDistance)
        {
            // Retreat from the player
            Debug.Log("Retreating from player");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bunker")
        {
            // Take cover in the bunker
            currentBunker = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Bunker")
        {
            // Leave the bunker
            currentBunker = null;
        }
    }
}