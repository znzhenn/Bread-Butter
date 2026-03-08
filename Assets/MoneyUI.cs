using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public CustomerSystem customerSystem;
    public TextMeshProUGUI moneyText;

    void Update()
    {
        Debug.Log(customerSystem.money);
        moneyText.text = "Coins: " + customerSystem.money;
    }
}