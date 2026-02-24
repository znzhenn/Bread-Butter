using UnityEngine;

public class Customer
{
    public string customerName;
    public Recipe favoriteBread; //preferred recipe
    public float patience;
    public float mood;
    public bool isReturning;

    public Customer(string customerName, Recipe favoriteBread, float mood, bool isReturning)
    {
        this.customerName = customerName;
        this.favoriteBread = favoriteBread;
        this.mood = mood;
        this.isReturning = isReturning;
        this.patience = 10f;
    }

}