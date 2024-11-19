using System.Collections;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class ObjectPhaser : MonoBehaviour
    {
        public GameObject obj;
        public float phaseOutInterval, phaseInInterval;
        public int phaseInParticleIndex, phaseOutParticleIndex;

        private bool _readyToPhase = true;
        
        
        private void FixedUpdate()
        {
            if (_readyToPhase) StartCoroutine(PhaseObject());
        }

        IEnumerator PhaseObject()
        {
            _readyToPhase = false;
            MusicManager.Instance.sfxPlayerProjectiles.FeedbacksList[14].Play(transform.position);

            yield return new WaitForSeconds(phaseOutInterval);
            if (phaseInParticleIndex > 0)
            {
                // get a pool that's yet to be made
            }
            obj.SetActive(false);
            
            MusicManager.Instance.sfxPlayerProjectiles.FeedbacksList[14].Stop(transform.position);

            yield return new WaitForSeconds(phaseInInterval);
            if (phaseOutParticleIndex > 0)
            {
                // get a pool that's yet to be made
            }
            obj.SetActive(true);
            _readyToPhase = true;
        }
    }
}