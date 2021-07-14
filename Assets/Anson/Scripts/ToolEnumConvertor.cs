using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType {None, Stick, Gun, Hammer, Fuse};

public static class ToolEnumConvertor
{
    public static string ToName(ToolType t)
    {
        switch (t)
        {
            default:
                return t.ToString();
        }
    }

    public static Sprite GetSprite(ToolType t)
    {
        return null;
    }
}