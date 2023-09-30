using UnityEngine;

namespace EffectsTools
{
    public class ItemDropper : MonoBehaviour
    {
        public GameObject itemDrop;

        public void DropItem(Vector3 dropLocation)
        {
            Instantiate(itemDrop, dropLocation, Quaternion.identity);
        }
    }
}
