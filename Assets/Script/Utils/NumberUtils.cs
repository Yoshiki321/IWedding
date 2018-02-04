using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class NumberUtils
{

    /// <summary>
    /// 概率
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool probability(float value)
    {
        if (UnityEngine.Random.Range(0, 100) <= value)
        {
            return true;
        }
        return false;
    }

    private static List<float> _aUniqueIDs;

    /// <summary>
    /// 一个唯一的数字
    /// </summary>
    /// <returns></returns>
    public static float GetUnique()
    {
        if (_aUniqueIDs == null)
        {
            _aUniqueIDs = new List<float>();
        }

        System.DateTime dCurrent = System.DateTime.Now;
        int nID = dCurrent.Day + dCurrent.Month * 100 + dCurrent.Year * 1000 + dCurrent.Minute * 10000;

        while (!isUnique(nID))
        {
            nID += UnityEngine.Random.Range(nID, 2 * nID);
        }

        _aUniqueIDs.Add(nID);

        return nID;
    }

    /// <summary>
    /// 由连字符分隔的32位数字
    /// </summary>
    /// <returns></returns>
    public static string GetGuid()
    {
        System.Guid guid = new Guid();
        guid = Guid.NewGuid();
        return guid.ToString();
    }

    /// <summary>  
    /// 根据GUID获取16位的唯一字符串  
    /// </summary>  
    /// <param name=\"guid\"></param>  
    /// <returns></returns>  
    public static string GuidTo16String()
    {
        long i = 1;
        foreach (byte b in Guid.NewGuid().ToByteArray())
            i *= ((int)b + 1);
        return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }

    /// <summary>  
    /// 根据GUID获取19位的唯一数字序列  
    /// </summary>  
    /// <returns></returns>  
    public static long GuidToLongID()
    {
        byte[] buffer = Guid.NewGuid().ToByteArray();
        return BitConverter.ToInt64(buffer, 0);
    }

    private static bool isUnique(float nNumber)
    {
        for (int i = 0; i < _aUniqueIDs.Count; i++)
        {
            if (_aUniqueIDs[i] == nNumber)
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsNumeric(string value)
    {
        return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
    }

    public static bool IsInt(string value)
    {
        return Regex.IsMatch(value, @"^[+-]?\d*$");
    }

    public static bool IsUnsign(string value)
    {
        return Regex.IsMatch(value, @"^\d*[.]?\d*$");
    }

    public static bool isTel(string strInput)
    {
        return Regex.IsMatch(strInput, @"\d{3}-\d{8}|\d{4}-\d{7}");
    }
}
