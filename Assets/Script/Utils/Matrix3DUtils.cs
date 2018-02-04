//using UnityEngine;
//using System.Collections.Generic;

//public class Matrix3DUtils
//{
//    public static Vector3 transformVector(Matrix4x4 matrix, Vector3 vector, Vector3 result)
//    {
//        if (!result.IsNull()) result = new Vector3();

//        float x = vector.x;
//        float y = vector.y;
//        float z = vector.z;
//        result.x = matrix.m00 * x + matrix.m10 * y + matrix.m20 * z + matrix.m30;
//        result.y = matrix.m01 * x + matrix.m11 * y + matrix.m21 * z + matrix.m31;
//        result.z = matrix.m02 * x + matrix.m12 * y + matrix.m22 * z + matrix.m32;
//        //result.w = m* x + n* y + o* z + p;
//        return result;
//    }
//}
