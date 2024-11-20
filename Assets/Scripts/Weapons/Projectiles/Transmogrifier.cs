using UnityEngine;

namespace Weapons.Projectiles
{
    public class Transmogrifier : MonoBehaviour
    {
        public int wid, pid, eid;
        private void MutateEnemy(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                if (collision) collision.gameObject.SetActive(false);
                var mutantPosition = collision.transform.position;
                var mutantSlug = MutantsAndBotsPoolManager.poolMutRob.mutAndRobPools[0].GetPooledGameObject();
                mutantSlug.SetActive(true);
                mutantSlug.transform.position = mutantPosition;
                MusicManager.Instance.sfxPlayerProjectiles2.FeedbacksList[pid].Play(transform.position);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            MutateEnemy(collision);
        }
    }
}
