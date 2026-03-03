using UnityEngine;
using UnityEngine.UI;

public class CustomerUI : MonoBehaviour
{
    public Text nameText;
    public Text breadText;
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