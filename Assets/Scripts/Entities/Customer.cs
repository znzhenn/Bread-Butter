using UnityEngine;

public class Customer
{
    public string customerName;
    public Recipe favoriteBread; //preferred recipe
    public float patience;
    public float maxPatience;
    public float mood;
    public bool isReturning;

    public Customer(string customerName, Recipe favoriteBread, float mood, bool isReturning)
    {
        this.customerName = customerName;
        this.favoriteBread = favoriteBread;
        this.mood = mood;
        this.isReturning = isReturning;
        maxPatience = 10f;
        patience = maxPatience;
    }

    //losing patience
    public void TickPatience(float deltaTime)
    {
        patience -= deltaTime;
        if (patience < 0) patience = 0;
    }

    // For UI slider display
    public float GetPatiencePercentage()
    {
        return patience / maxPatience;
    }

    //no want wait
    public bool IsImpatient()
    {
        return patience <= 0;
    }

}