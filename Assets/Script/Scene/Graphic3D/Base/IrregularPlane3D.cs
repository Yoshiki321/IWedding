using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class IrregularPlane3D : BasePlane3D
{
    private MeshData _meshData;

    //private float _maxW = 100;
    //private float _minW = -100;
    //private float _maxH = 100;
    //private float _minH = -100;

    /*-----------------------------------------------------------------------------------*/
    /*-----------------------------------------------------------------------------------*/
    /*------buildGeometry----------------------------------------------------------------*/
    /*-----------------------------------------------------------------------------------*/
    /*-----------------------------------------------------------------------------------*/
    /*-----------------------------------------------------------------------------------*/

    public void BuildIrregularGeometry(MeshData meshData)
    {
        _meshData = meshData;

        BuildGeometry();
    }

    override protected void DrawBuildGeometry()
    {
        if (_meshData == null)
        {
            return;
        }

        for (int i = 0; i < _meshData.vertices.Length; i++)
        {
            AddVertexData(_meshData.vertices[i].x, _meshData.vertices[i].y, _meshData.vertices[i].z);
        }

        for (int i = 0; i < _meshData.triangles.Length; i += 3)
        {
            AddIndexData(_meshData.triangles[i], _meshData.triangles[i + 1], _meshData.triangles[i + 2]);
        }
    }
}
