using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RectPenetratePlane3D : BasePlane3D
{
    protected List<Penetrate> _rectList = new List<Penetrate>();
    protected List<Hashtable> _rectObjectList = new List<Hashtable>();

    public void SetPenetrate(List<Penetrate> list)
    {
        _rectList = new List<Penetrate>();
        for (int i = 0; i < list.Count; i++)
        {
            _rectList.Add(list[i]);
        }

        //与离墙初始点距离排序
        _rectList.Sort(delegate (Penetrate px, Penetrate py) {
            return px.x.CompareTo(py.x);
        });

        UpdataPenetrate();
    }

    private void UpdataPenetrate()
    {
        _rectObjectList = new List<Hashtable>();

        float xx = 0;
        Hashtable o;
        //			var penentrate:Object = {x:0,width:500,height:300,y:0};
        //			if(p)_rectList.push(penentrate);

        Penetrate penetrate;
        for (int i = 0; i < _rectList.Count; i++)
        {
            penetrate = _rectList[i];
            //				penentrate = {x:1860,width:1000,height:200,y:200}

            o = new Hashtable();
            o["x"] = (_w / 2 - (penetrate.x - xx) / 2) - xx;
            o["y"] = 0f;
            o["w"] = penetrate.x - xx;
            o["h"] = _h;
            _rectObjectList.Add(o);

            o = new Hashtable();
            o["x"] = -(penetrate.x + penetrate.width / 2) + _w / 2;
            o["w"] = penetrate.width;
            o["h"] = _h - penetrate.height - penetrate.y;
            o["y"] = -_h / 2 + (float)o["h"] / 2;
            _rectObjectList.Add(o);

            o = new Hashtable();
            o["x"] = -(penetrate.x + penetrate.width / 2) + _w / 2;
            o["y"] = _h / 2 - penetrate.y / 2;
            o["w"] = penetrate.width;
            o["h"] = penetrate.y;
            _rectObjectList.Add(o);

            xx = penetrate.x + penetrate.width;

            if (i == _rectList.Count - 1)
            {
                o = new Hashtable();
                o["x"] = -xx / 2;
                o["y"] = 0f;
                o["w"] = _w - xx;
                o["h"] = _h;
                _rectObjectList.Add(o);
            }
        }
    }

    /*-----------------------------------------------------------------------------------*/
    /*-----------------------------------------------------------------------------------*/
    /*------buildGeometry----------------------------------------------------------------*/
    /*-----------------------------------------------------------------------------------*/
    /*-----------------------------------------------------------------------------------*/
    /*-----------------------------------------------------------------------------------*/

    override protected void DrawBuildGeometry()
    {
		_nextVertexIndex = 3;
		_currentIndex = 0;

        if (_rectObjectList.Count == 0)
        {
            Hashtable h = new Hashtable();
            h["x"] = 0f;
            h["y"] = 0f;
            h["w"] = _w;
            h["h"] = _h;
            BuildGeometryRect(h);
        }
        else
        {
            Hashtable o;
            for (int i = 0; i < _rectObjectList.Count; i++)
            {
                o = _rectObjectList[i];
                if (double.IsNaN((float)o["w"]) || double.IsInfinity((float)o["w"])) return;
                if (double.IsNaN((float)o["h"]) || double.IsInfinity((float)o["h"])) return;

                BuildGeometryRect(o);

//					if(o.w > 0 && o.h > 0){
//						switch(o.t){
//							case StructureData.RECT:
//								buildGeometryRect(o);
//								break;
//							case StructureData.CIRCLE:
//								buildGeometryCircle(o);
//								break;
//							case StructureData.ARCHED:
//								buildGeometryArched(o);
//								break;
//						}
//					}
            }
        }
    }

    /**
	 * 矩形 
	 * @param o
	 * @param p 
	 * 
	 */
    private void BuildGeometryRect(Hashtable o)
    {
		float x = (float)o ["x"];
		float y = (float)o ["y"];
		float w = (float)o ["w"];
		float h = (float)o ["h"];

		AddVertexData (x - w / 2, 0, y - h / 2);
		AddVertexData (x + w / 2, 0, y + h / 2);
		AddVertexData (x - w / 2, 0, y + h / 2);
		AddIndexData (_nextVertexIndex - 1, _nextVertexIndex - 2, _nextVertexIndex - 3);
		AddVertexData (x + w / 2, 0, y - h / 2);
		AddVertexData (x + w / 2, 0, y + h / 2);
		AddVertexData (x - w / 2, 0, y - h / 2);
		AddIndexData (_nextVertexIndex - 1, _nextVertexIndex - 2, _nextVertexIndex - 3);
    }

    private float _w;
    private float _h;

    public float width
    {
        set { _w = value; }
        get { return _w; }
    }

    public float height
    {
        set { _h = value; }
        get { return _h; }
    }
}
