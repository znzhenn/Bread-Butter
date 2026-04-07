using UnityEngine;
public class TimeEngine : MonoBehaviour
{
    public float CurrentTime { get; private set; }

    public void Tick(float deltaTime)
    {
        CurrentTime += deltaTime;
    }
}