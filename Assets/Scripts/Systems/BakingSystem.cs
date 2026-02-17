using UnityEngine;

public class BakingSystem : MonoBehaviour
{
    public DisplaySystem displaySystem;

    public void BakeBread()
    {
        if (GameManager.Instance.CurrentState!= GameManager.GameState.EndDay)
        {
            return; // no baking at the end of the day
        }
        int quality = Random.Range(50, 101);
        int value = quality/10;

        Bread bread = new Bread("White Loaf", quality, value);
        displaySystem.AddBread(bread);
        //prints what bread is added
            
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            BakeBread();
        }
    }

}
