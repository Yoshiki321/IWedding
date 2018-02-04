using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils
{

    /**
         * 角度转弧度
         * @param angle
         * @return 
         * 
         */
    public static float ConvertToRadian(float angle)
    {
        return angle * Mathf.PI / 180;
    }

    /**
     * 弧度转角度
     * @param angle
     * @return 
     * 
     */
    public static float ConvertToDegree(float angle)
    {
        return 180 * angle / Mathf.PI;
    }
}
