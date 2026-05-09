public class Bread
{
    public Recipe recipe;
    public float quality;
    public float breadValue;
    public ItemData breadItem;


    public Bread(Recipe recipe, float quality)
    {
        this.recipe = recipe;
        this.quality = quality;
        CalculateBreadValue();
    }

    private void CalculateBreadValue()
    {
        breadValue = recipe.baseValue * quality;
    }
}