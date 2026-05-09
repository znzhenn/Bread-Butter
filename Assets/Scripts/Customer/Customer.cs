using UnityEngine;

public class Customer
{
    public string customerName;
    public Recipe favoriteBread;
    public float patience;
    public float maxPatience;
    public float mood;
    public bool isReturning;

    public Customer(string customerName, Recipe favoriteBread, float mood, bool isReturning, float maxPatience)
    {
        this.customerName = customerName;
        this.favoriteBread = favoriteBread;
        this.mood = mood;
        this.isReturning = isReturning;
        this.maxPatience = maxPatience;
        this.patience = maxPatience;
    }

    public void TickPatience(float deltaTime)
    {
        patience -= deltaTime;
        if (patience < 0) patience = 0;
    }

    public float GetPatiencePercentage()
    {
        if (maxPatience <= 0) return 0f;
        return patience / maxPatience;
    }

    public bool IsImpatient()
    {
        return patience <= 0;
    }
}