using UnityEngine;

[CreateAssetMenu(fileName = "NewCustomerProfile", menuName = "Bakery/Customer Profile")]
public class CustomerProfile : ScriptableObject
{
    public string customerName;
    public Recipe favoriteBread;
    public float mood = 1f;
    public bool isReturning = false;
    public float maxPatience = 10f;
}