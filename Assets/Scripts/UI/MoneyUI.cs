using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public CustomerSystem customerSystem;
    public ShopSystem ShopSystem;
    public TextMeshProUGUI moneyText;

    [System.Obsolete]
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
        Debug.Log(ShopSystem.money);
        moneyText.text = "Gold: " + ShopSystem.money;
    }
}