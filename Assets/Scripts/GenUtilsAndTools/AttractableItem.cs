using Controllers;
using UnityEngine;
using UnityEngine.Serialization;

namespace GenUtilsAndTools
{
    public class AttractableItem : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool isAttracted;
    
        private void Start()
        {
            moveSpeed = ItemAttractor.itemAttractor.pullSpeed;
        }
    
        private void Update()
        {
            if (isAttracted)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    PlayerController.pController.transform.position, moveSpeed * Time.deltaTime);
            }
        }
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("ItemAttractor"))
            {
                isAttracted = true;
                moveSpeed += PlayerController.pController.moveSpeed;
            }
        }
    }
}
