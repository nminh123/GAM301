using System;
using System.Collections.Generic;
using System.Numerics;

[Serializable]
public class Models
{
    public Dictionary<int, int> items = new Dictionary<int, int>();
    public int gasLeft;
    public float time;
    public Vector3 lastPosition;

    public Models()
    {
        items.Add(1,5);
        items.Add(2,15);
        items.Add(3,0);
        items.Add(4,55);
        items.Add(5,6);
        items.Add(6,1);
        items.Add(7,2);

        gasLeft = 5;
        time = 10;
        lastPosition = new Vector3(30,15,40);
    }
}