using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        private Transform _target;

        private void Start()
        {
            _target = FindObjectOfType<PlayerController>().transform;
        }

        private void LateUpdate()
        {
            var position = _target.position;
            transform.position = new Vector3(position.x, position.y, -10f);
        }
    }
}