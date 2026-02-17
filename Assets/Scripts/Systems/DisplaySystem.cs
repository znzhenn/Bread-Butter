using System;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySystem : MonoBehaviour
{
    public List<Bread> breadsOnDisplay = new List<Bread>();
    public int maxSlots = 3; //bakery upgrade later in game

    public Boolean AddBread(Bread bread)
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
}
