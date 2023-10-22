using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UI;
using UnityEngine;

namespace Controllers
{
    public class GeneralTextController : MonoBehaviour
    {
        public static GeneralTextController contGenText;
        public GeneralText text;
        public Transform textTransform;
        
        private readonly List<GeneralText> _tmpTexts = new();

        private void Awake()
        {
            contGenText = this;
        }

        public void ShowText([NotNull] string textString, Vector3 location)
        {
            if (textString == null) throw new ArgumentNullException(nameof(textString));
            var newText = GetFromPool();
            newText.Setup(textString);
            newText.gameObject.SetActive(true);

            newText.transform.position = location;
        }
    
    
        public GeneralText GetFromPool()
        {
            GeneralText pooledText;

            if (_tmpTexts.Count == 0)
            {
                pooledText = Instantiate(text, textTransform);
            }
            else
            {
                pooledText = _tmpTexts[0];
                _tmpTexts.RemoveAt(0);
            }
            return pooledText;
        }

        public void PlaceInPool(GeneralText textToAdd)
        {
            textToAdd.gameObject.SetActive(false);
            _tmpTexts.Add(textToAdd);
        }
    }
}
