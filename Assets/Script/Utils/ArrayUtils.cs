using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayUtils
{
    public static List<T> SortOn<T>(List<T> arr, string fieldName)
    {
        T ch;
        for (var i = 0; i < arr.Count - 1; i++)
        {
            Type type = arr[i].GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("fieldName");
            float t = (float)propertyInfo.GetValue(arr[i], null);

            Type type1 = arr[i + 1].GetType();
            System.Reflection.PropertyInfo propertyInfo1 = type1.GetProperty("fieldName");
            float t1 = (float)propertyInfo1.GetValue(arr[i + 1], null);

            if (t > t1)
            {
                ch = arr[i + 1];
                arr[i + 1] = arr[i];
                arr[i] = ch;
                i = -1;
            }
        }
        return arr;
    }

    /// <summary>
    /// 是否存在对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="h"></param>
    /// <returns></returns>
    public static bool Has<T>(List<T> array, T h)
    {
        for (int i = 0; i < array.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(array[i], h))
            {
                return true;
            }
        }
        return false;
    }

    public static bool Has(ArrayList array, object h)
    {
        for (int i = 0; i < array.Count; i++)
        {
            if (h.Equals(array[i]))
            {
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// 克隆数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array1"></param>
    /// <param name="array2"></param>
    public static void CloneArray<T>(List<T> array1, List<T> array2)
    {
        if (array2 == null) array2 = new List<T>();
        for (int i = 0; i < array1.Count; i++)
        {
            array2.Add(array1[i]);
        }
    }

    /**
	 * 比较2个数组是否相等 
	 * @param aArrayA 数组A
	 * @param aArrayB 数组B
	 * @return true or false
	 * 
	 */
    public static bool Equals<T>(List<T> aArrayA, List<T> aArrayB)
    {
        if (aArrayA == null && aArrayB == null) return true;
        if (aArrayA.Count != aArrayB.Count)
        {
            return false;
        }
        for (int i = 0; i < aArrayA.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(aArrayA[i], aArrayB[i]))
            {
                return false;
            }
        }
        return true;
    }

    public static bool HasVector2(List<Vector2> array1, Vector2 point)
    {
        for (int i = 0; i < array1.Count; i++)
        {
            if (array1[i].Equals(point))
            {
                return true;
            }
        }
        return false;
    }

    public static void ConvertToList(object[] o)
    {

    }
}


