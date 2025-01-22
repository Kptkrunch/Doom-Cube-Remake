using System.Collections;
using Controllers;
using UnityEngine;
using Weapons;
using Weapons.SpecificWeapons;

namespace GenUtilsAndTools
{
    public class ObjectPhaser : MonoBehaviour
    {
        public Weapon parent;
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
            
            yield return new WaitForSeconds(phaseOutInterval);
            if (phaseInParticleIndex > 0)
            {
                // get a pool that's yet to be made
                
            }
            obj.SetActive(false);
            parent.juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Firing);
            
            yield return new WaitForSeconds(phaseInInterval);
            if (phaseOutParticleIndex > 0)
            {
                // get a pool that's yet to be made
            }
            obj.SetActive(true);
            parent.juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Firing);
            _readyToPhase = true;
        }
    }
}