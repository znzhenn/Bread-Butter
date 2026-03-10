using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public CustomerSystem customerSystem;
    public TextMeshProUGUI moneyText;

    void Start()
    {
        if (customerSystem == null)
        {
            customerSystem = FindObjectOfType<CustomerSystem>();
        }

        UpdateMoney();
    }

    public void UpdateMoney()
    {
        Debug.Log(customerSystem.money);
        moneyText.text = "Coins: " + customerSystem.money;
    }
}