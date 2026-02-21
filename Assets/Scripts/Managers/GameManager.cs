using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public enum GameState
    {
        StartDay, //Baking bread
        OpenShop, //Selling to customers (and baking bread with consequences)
        EndDay // upgrading the bakery
    }

    public GameState CurrentState
    {
        get; private set;
    }


    public int Day
    {
        get; private set;
    } = 1; //start the day counter at 1

    public int Money
    {
        get; private set;
    } = 0; // exact amount to be decided later
    
    private void Start()
    {
        StartNewDay();
    }

    // starts a new day
    public void StartNewDay()
    {
        CurrentState = GameState.StartDay;
        Debug.Log("Starting Day " + Day + "!");
    }

    //opens the shop 
    private void OpenShop()
    {
        CurrentState = GameState.OpenShop;
        Debug.Log("The Bakery is Open!");
    }

    //ends the day
    private void EndDay()
    {
        Debug.Log("End of Day " + Day);
        CurrentState = GameState.EndDay; // switches game state, space for other logic here


        Day++;

        // doesn't change the state
    }
    
    
}
