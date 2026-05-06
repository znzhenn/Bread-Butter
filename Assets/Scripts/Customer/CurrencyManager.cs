using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int money = 0;

    public TMP_Text moneyText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;

        UpdateUI();

        Debug.Log("Money: " + money);
    }

    private void UpdateUI()
    {
        if(moneyText != null)
        {
            moneyText.text = "$" + money;
        }
    }
}