using UnityEngine;

public class Order
{
    public Customer customer;
    public Recipe requestedRecipe;
    public bool isCompleted;

    public Order(Customer customer, Recipe recipe)
    {
        this.customer = customer;
        this.requestedRecipe = recipe;
        isCompleted = false;
    }
}