using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Systems")]
    public CustomerSystem customerSystem;
    public BakingSystem bakingSystem;
    public ShopSystem shopSystem;

    public enum GameState
    {
        StartDay,
        OpenShop,
        EndDay
    }

    public GameState CurrentState { get; private set; }

    public int Day { get; private set; } = 1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartNewDay();
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        customerSystem.Tick(dt);
        bakingSystem.Tick(dt);
    }

    public void StartNewDay()
    {
        CurrentState = GameState.StartDay;
        Debug.Log("Starting Day " + Day + "!");

        //OpenShop();
    }

    public void OpenShop()
    {
        CurrentState = GameState.OpenShop;
        Debug.Log("The Bakery is Open!");
    }

    public void OnOpenShop(InputValue value)
    {
        if (!value.isPressed) return;

        if (CurrentState == GameState.StartDay)
        {
            OpenShop();
        }
    }
}