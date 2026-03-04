using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomerUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI breadText;
    public Slider patienceSlider;

    private Customer customer;

    
    public void Setup(Customer customer)
    {
        
        this.customer = customer;
        nameText.text = customer.customerName;
        breadText.text = customer.favoriteBread.recipeName;
        patienceSlider.maxValue = 1f;
        patienceSlider.value = customer.GetPatiencePercentage();
    }

    void Update()
    {
        if (customer != null)
        {
            patienceSlider.value = customer.GetPatiencePercentage();
        }
    }
}