using System;

public class Vertex
{
    private float _x;
    private float _y;
    private float _z;
    private int _index;

    public Vertex(float x = 0, float y = 0, float z = 0, int index = 0)
    {
        _x = x;
        _y = y;
        _z = z;
        _index = index;
    }

    public int index
    {
        set { _index = value; }
        get { return _index; }
    }

    public float x
    {
        set { _x = value; }
        get { return _x; }
    }

    public float y
    {
        set { _y = value; }
        get { return _y; }
    }

    public float z
    {
        set { _z = value; }
        get { return _z; }
    }

    public Vertex clone()
    {
        return new Vertex(_x, _y, _z);
    }

    public String toString()
    {
        return _x + "," + _y + "," + _z;
    }
}


