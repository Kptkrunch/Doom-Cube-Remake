using GenUtilsAndTools;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class Nanite : MonoBehaviour
    {
        public ObjectPhaser phaser;
        private void MutateEnemy(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                if (collision) collision.gameObject.SetActive(false);
                var mutantPosition = collision.transform.position;
                var mutantSlug = MutantsAndBotsPoolManager.poolMutRob.mutAndRobPools[0].GetPooledGameObject();
                mutantSlug.SetActive(true);
                mutantSlug.transform.position = mutantPosition;
                phaser.ResetPhasingOnHit();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            MutateEnemy(collision);
        }
    }
}
