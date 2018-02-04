using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane2D : ObjectSprite
{
    void Start()
    {
        //InitGrid(60, 60);
    }

    public static Texture2D texture2;

    void InitGrid(int rows, int column)
    {
        float totalWidth = (GetComponent<BoxCollider>().bounds.max.x - GetComponent<BoxCollider>().bounds.min.x) * 50;
        float totalHeight = (GetComponent<BoxCollider>().bounds.max.y - GetComponent<BoxCollider>().bounds.min.y) * 50;

        texture2 = new Texture2D((int)totalWidth, (int)totalHeight, TextureFormat.RGB24, false);
        GetComponent<MeshRenderer>().material.SetTexture(0, texture2);
        texture2.wrapMode = TextureWrapMode.Clamp;
        GetComponent<MeshRenderer>().material.mainTexture = texture2;

        float w = totalWidth / column;
        float h = totalHeight / rows;
        for (int i = 0; i <= column; i++)
        {
            if (i == column / 2 || i == 0 || i == column)
            {
                texture2.DrawLine(new Vector3(w * i, 0), new Vector3(w * i, totalHeight), Color.black);
            }
            else
            {
                texture2.DrawLine(new Vector3(w * i, 0), new Vector3(w * i, totalHeight), Color.grey);
            }
        }
        for (int i = 0; i <= rows; i++)
        {
            if (i == rows / 2 || i == 0 || i == rows)
            {
                texture2.DrawLine(new Vector3(0, h * i), new Vector3(totalWidth, h * i), Color.black);
            }
            else
            {
                texture2.DrawLine(new Vector3(0, h * i), new Vector3(totalWidth, h * i), Color.grey);
            }
        }

        texture2.Apply();
    }

    void Update()
    {

    }
}
