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
        if (Instance != null && Instance != this)
    {
        Destroy(gameObject);
        return;
    }
        Instance = this;
    }

    private void Start() //start
    {
        StartNewDay();

    }

    public void StartNewDay()
    {
        CurrentState = GameState.StartDay;
        Debug.Log("Starting Day " + Day + "!");
        OpenShop();
    }

    private void OpenShop()
    {
        CurrentState = GameState.OpenShop;
        Debug.Log("The Shop is Open!");
    }

    void EndDay()
    {
        Debug.Log("End of Day " + Day);
        Day++;
        //ChangeState(GameState.StartDay);
    }

    private void Update()
    {
        if (CurrentState == GameState.EndDay && Input.GetKeyDown(KeyCode.N))
        {
            StartNewDay();
        }

        if (Input.GetKeyDown(KeyCode.C)) // press c to chang states
        {
            CycleState();
        }
    }
    //cycles through states
    private void CycleState()
    {
        switch (CurrentState)
        {
            case GameState.StartDay:
                OpenShop();
                break;
            case GameState.OpenShop:
                EndDay();
                break;
            case GameState.EndDay:
                StartNewDay();
                break;
        }
        Debug.Log("Current State: " + CurrentState);
    }
}
