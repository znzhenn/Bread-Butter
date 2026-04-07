using UnityEngine;

[CreateAssetMenu(fileName="Item", menuName="Bakery/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public int sellPrice;
}