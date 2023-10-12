using UnityEngine;

namespace TechSkills
{
    public class CurvedMovement : MonoBehaviour
    {
        public GameObject[] waypoints;
        public float speed = 5.0f;
        public float reachDistance = 1.0f;
        public int currentWaypoint = 0;

        private void Update()
        {
            float distance = Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position);

            if (distance <= reachDistance)
            {
                currentWaypoint++;
            }

            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, Time.deltaTime * speed);
        }
    }
}