using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public GameObject itemDrop;

    public void DropItem(Vector3 dropLocation)
    {
        Instantiate(itemDrop, dropLocation, Quaternion.identity);
    }
}
