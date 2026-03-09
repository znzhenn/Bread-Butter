using UnityEngine;

public class BakeButton : MonoBehaviour
{
    public Recipe recipeToBake;
    public BakingSystem bakingSystem;

    public void onPressBakeButton()
    {
        // Debug.Log ("Button clicked");
        bakingSystem.BakeBread(recipeToBake);
    }
}
