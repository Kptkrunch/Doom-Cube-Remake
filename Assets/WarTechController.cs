using Controllers;
using UnityEngine;

public class WarTechController : MonoBehaviour
{
    public static WarTechController contTech;

    private void Awake()
    {
        contTech = this;
    }


}
