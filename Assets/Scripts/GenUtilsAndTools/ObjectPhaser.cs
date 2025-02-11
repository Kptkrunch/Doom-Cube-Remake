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

        public void UpdateIntervals(float phaseOut, float phaseIn)
        {
            phaseOutInterval = phaseOut;
            phaseInInterval = phaseIn;
        }

        IEnumerator PhaseObject()
        {
            _readyToPhase = false;
            
            yield return new WaitForSeconds(phaseOutInterval);
            obj.SetActive(false);
            
            yield return new WaitForSeconds(phaseInInterval);
            obj.SetActive(true);
            parent.juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Firing);
            
            _readyToPhase = true;
        }
    }
}