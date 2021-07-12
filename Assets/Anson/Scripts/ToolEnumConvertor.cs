using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ToolEnumConvertor
{
    public static string ToName(Tools t)
    {
        switch (t)
        {
            default:
                return t.ToString();
        }
    }
}