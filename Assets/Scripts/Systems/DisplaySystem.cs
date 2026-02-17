using System;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySystem : MonoBehaviour
{
    public static DisplaySystem Instance { 
        get; 
        private set; }
    [SerializeField] private int maxSlots = 3; // starting slots
    public int MaxSlots => maxSlots;

    public List<Bread> breadsOnDisplay = new List<Bread>();

     private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool AddBread(Bread bread)
    {
        if (breadsOnDisplay.Count>= maxSlots)
        {
            Debug.Log("Oh no! The display box is already full!");
            return false;
        } breadsOnDisplay.Add(bread);
        Debug.Log(bread.breadName + " has been added to the display!");
        return true;
    }

    public Bread TakeBread()
    {   // later, the order of the bread shouldn't matter
        if (breadsOnDisplay.Count == 0)
        {
            return null;
        } 
        Bread bread = breadsOnDisplay[0];
        breadsOnDisplay.RemoveAt(0);
        return bread;
    }

    /* for later
    public void IncreaseMaxSlots(int amount)
    {
        if (amount <= 0) return;
        maxSlots += amount;
        Debug.Log("Display upgraded! Max slots: " + maxSlots);
    }

    */
}
