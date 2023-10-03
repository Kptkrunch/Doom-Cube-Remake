using Controllers;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class AttractableItem : MonoBehaviour
    {
        private float _moveSpeed;
        private bool _isAttracted;
    
        private void Start()
        {
            _moveSpeed = ItemAttractor.itemAttractor.pullSpeed;
        }
    
        private void Update()
        {
            if (_isAttracted)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    PlayerController.pController.transform.position, _moveSpeed * Time.deltaTime);
            }
        }
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("ItemAttractor"))
            {
                _isAttracted = true;
                _moveSpeed += PlayerController.pController.moveSpeed;
            }
        }
    }
}
