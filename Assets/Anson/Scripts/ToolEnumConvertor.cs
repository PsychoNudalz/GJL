using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tools { Stick, Hammer };

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

    public static Sprite GetSprite(Tools t)
    {
        return null;
    }
}