using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path2D
{
    private Bisector _bisector;
    private Bisector _bisectorAlong;
    private Bisector _bisectorInverse;
    private Bisector _bisectorAlongN;
    private Bisector _bisectorInverseN;
    private Bisector _bisectorAlongO;
    private Bisector _bisectorInverseO;

    private Vector2 _from;
    private Vector2 _to;
    private float _thickness;

    public void setPoint(Vector2 from, Vector2 to, float thickness)
    {
        _from = from;
        _to = to;

        _thickness = thickness;


        update();
    }

    public float thickness
    {
        get { return _thickness; }
    }

    /**
     * 最后一次不同点的k和b 
     */
    private float _lk;
    private float _lb;

    public void update()
    {
        _bisector = new Bisector(_from, _to);

        _bisectorAlong = _bisector.Translation(_thickness / 2);
        _bisectorInverse = _bisector.Translation(_thickness / -2);

        _bisectorAlongN = _bisector.Translation(_thickness / 1.7f);
        _bisectorInverseN = _bisector.Translation(_thickness / -1.7f);

        _bisectorAlongO = _bisector.Translation(_thickness / .7f);
        _bisectorInverseO = _bisector.Translation(_thickness / -.7f);

        //如果出现相同点，维持最后一次的kb
        if (_from.Equals(_to))
        {
            _bisector.k = _lk;
            _bisector.b = _lb;
            _bisectorAlong.k = _lk;
            _bisectorAlong.b = _lb;
            _bisectorInverse.k = _lk;
            _bisectorInverse.b = _lb;
            _bisectorAlongN.k = _lk;
            _bisectorAlongN.b = _lb;
            _bisectorInverseN.k = _lk;
            _bisectorInverseN.b = _lb;
            _bisectorInverseO.k = _lk;
            _bisectorInverseO.b = _lb;
        }
        else
        {
            _lk = _bisector.k;
            _lb = _bisector.b;
        }
    }

    public Vector2 from
    {
        set
        {
            if (_from.Equals(value)) return;
            _from = value;

            update();
        }
        get { return _from; }
    }

    public Vector2 to
    {
        set
        {
            if (_to.Equals(value)) return;
            _to = value;

            update();
        }
        get { return _to; }
    }

    public bool isFrom(Vector2 p)
    {
        return _bisector.from.Equals(p);
    }

    public Bisector bisector
    {
        get { return _bisector; }
    }

    public Bisector bisectorAlong
    {
        get { return _bisectorAlong; }
    }

    public Bisector bisectorInverse
    {
        get { return _bisectorInverse; }
    }

    public Bisector bisectorAlongN
    {
        get { return _bisectorAlongN; }
    }

    public Bisector bisectorInverseN
    {
        get { return _bisectorInverseN; }
    }

    public Bisector bisectorAlongO
    {
        get { return _bisectorAlongO; }
    }

    public Bisector bisectorInverseO
    {
        get { return _bisectorInverseO; }
    }

}
