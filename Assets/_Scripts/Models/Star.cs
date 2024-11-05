using UnityEngine;

public class Star 
{
    public int Energy;
    public Vector2 Velocity;
    public int Index;

    public Star(int energy,Vector2 velocity, int index)
    {
        Energy = energy;
        Velocity = velocity;
        Index= index;
    }

    public Star(int index)
    {
        Velocity = Vector2.zero;
        Energy = FieldUtil.MaxEnergy;
        Index= index;
    }
}
