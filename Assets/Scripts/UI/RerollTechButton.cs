using TMPro;
using UnityEngine;

namespace UI
{
    public class RerollTechButton : MonoBehaviour
    {
        public TMP_Text costText;
        [SerializeField] private int rerollCost;
        
        private void Start()
        {
            costText.text = rerollCost.ToString();
        }
    }
}
