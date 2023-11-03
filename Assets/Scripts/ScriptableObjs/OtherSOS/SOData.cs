using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data", order = 1)]
public class Data : ScriptableObject
{
    public float height;
    public string text;
}

// In your prefab script
public class MyCustomPrefab : MonoBehaviour
{
    public Data data;
    // other variable, methods, anything you want
}

// In your instantiation script
// MyCustomPrefab prefab = Instantiate(listViewItem, pos, spawnPoint.rotation);
// nextY += prefab.data.height;
// prefab.data.text = player.name;