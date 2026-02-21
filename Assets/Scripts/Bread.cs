using UnityEngine;

public abstract class Bread
{
    public string breadName;
    public int quality;
    public int value;

    // constructor
    public Bread(string name, int quality, int value)
    {
        this.breadName = name;
        this.quality = quality;
        this.value = value;
    }

}
