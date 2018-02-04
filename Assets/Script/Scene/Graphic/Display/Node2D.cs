using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Build2D
{
    public class Node2D : BuildSprite
    {
        public Node2D()
        {
            containNodes = new List<Node2D>();

            mousePoint.Null();
        }

        public Line2D line;

        public List<Node2D> containNodes;

        public Vector2 mousePoint;

        private float _x;
        private float _y;

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
        
        public Node2D opposite
        {
            get { return this == line.fromNode ? line.toNode : line.fromNode; }

        }

        private string _type = "";

        public string type
        {
            set { _type = value; }
            get { return _type; }
        }

        public bool isFrom
        {
            get
            {
                if (line.from.Equals(point))
                {
                    return true;
                }
                return false;
            }
        }

        private float _radius = 7;

        public float radius
        {
            set
            {
                _radius = value;
                Draw();
            }
        }

        public void Draw()
        {

        }

        public bool Equals(Node2D n2)
        {
            return (x == n2.x && y == n2.y);
        }

        public Vector2 point
        {
            set
            {
                x = value.x;
                y = value.y;

                //_label.text = "("+x+","+y+")";

                if (type == "from")
                {
                    line.path.from = new Vector2(x, y).Round();
                }
                else
                {
                    line.path.to = new Vector2(x, y).Round();
                }
            }

            get
            {
                return new Vector2(x, y).Round();
            }
        }
    }
}
