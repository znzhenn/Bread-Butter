using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    public int ID;
    public string itemName;
    public Sprite icon;

    public GameObject prefab; 
}