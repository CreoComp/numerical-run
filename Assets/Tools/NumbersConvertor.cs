using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NumbersConvertor
{
    public static Dictionary<int, char> format = new()
    {
        [3] = 'K',
        [6] = 'M',
        [9] = 'B',
        [12] = 'T'
    };

    public static string Convert(long num)
    {
        float sign = Mathf.Sign(num);
        num = Math.Abs(num);

        string numString = num.ToString();
        float zeroes = numString.Length-1;

        if (zeroes < 3)
            return num.ToString();

        int key = (int)Mathf.Floor(zeroes / 3f) * 3;

        if (key > 12)
            key = 12;

        float rNum = Mathf.Ceil((float)((decimal)num / (decimal)Math.Pow(10f, key-1))) / 10 * sign;
        //Debug.Log(num + " " + key);

        return rNum.ToString() + format[key];
    }
}
