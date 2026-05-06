using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int currentMoney;

    public void AddMoney(int amount)
    {
        currentMoney += amount;

        Debug.Log("Money: " + currentMoney);
    }
}