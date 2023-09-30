using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        private Transform _target;
        void Start()
        {
            _target = FindObjectOfType<PlayerController>().transform;
        }

        void LateUpdate()
        {
            transform.position = new Vector3(_target.position.x, _target.position.y, -10f);
        }
    }
}
