using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public enum GameState
    {
        StartDay, //baking bread with consequences
        OpenShop, //selling to customers (baking with consequences)
        EndDay //upgrading (ordering parts?)
    }

    public GameState CurrentState { 
        get; 
        private set; 
    }


    public int Day
    {
        get; private set;
    } = 1;
    public int Money
    {
        get;
        private set;
    } = 0; //increase starting amount later

     private void Awake()
    {
        Instance = this;
    }

    private void Start() //start
    {
      ChangeState(GameState.StartDay);
    }

    public void ChangeState(GameState newState){
        CurrentState = newState;
        switch (CurrentState)
        {
            case GameState.StartDay:
                StartDay();
                break;

            case GameState.OpenShop:
                Debug.Log("The Shop is Open!");
                break;

            case GameState.EndDay:
                EndDay();
                break;
        }

    }

    void StartDay()
    {
        Debug.Log("Day " + Day);
        ChangeState(GameState.OpenShop);
    
    }

    void EndDay()
    {
        Debug.Log("End of Day " + Day);
        Day++;
        ChangeState(GameState.StartDay);
    }
}
