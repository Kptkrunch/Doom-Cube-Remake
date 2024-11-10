using Controllers;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class AttractableItem : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected bool isAttracted;

        private void Start()
        {
            moveSpeed = ItemAttractor.itemAttractor.pullSpeed;
        }

        private void Update()
        {
            if (isAttracted)
                transform.position = Vector3.MoveTowards(transform.position,
                    PlayerController.contPlayer.transform.position, moveSpeed * Time.deltaTime);
        }

        private void OnDisable()
        {
            isAttracted = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("ItemAttractor")) return;
            isAttracted = true;
            moveSpeed += PlayerStatsController.contStats.moveSpeed;
        }
    }
}