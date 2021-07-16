using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType {
    None,
    Stick,
    GunEmpty,
    Hammer,
    Fuse,
    HammerBig,
    BlowTorch,
    Spanner,
    FingerOnStick,
    Crowbar,
    Potato,
    PaperBagMask,
    CoffeeMug,
    Apple,
    ApplePie,
    MoonRock,
    Mug,
    Gun,
    Button,
    WaterContainer,
    Body

};

public static class ToolEnumConvertor
{
    public static string ToName(ToolType t)
    {
        switch (t)
        {
            case ToolType.None:
                return "None";
                break;
            case ToolType.Stick:
                return "Stick";
                break;
            case ToolType.GunEmpty:
                return "Empty Gun";
                break;
            case ToolType.Hammer:
                return "Hammer";
                break;
            case ToolType.Fuse:
                return "Fuse";
                break;
            case ToolType.HammerBig:
                return "Big Hammer";
                break;
            case ToolType.BlowTorch:
                return "Blow Torch";
                break;
            case ToolType.Spanner:
                return "Spanner";
                break;
            case ToolType.FingerOnStick:
                break;
            case ToolType.Crowbar:
                break;
            case ToolType.Potato:
                break;
            case ToolType.PaperBagMask:
                break;
            case ToolType.CoffeeMug:
                break;
            case ToolType.Apple:
                break;
            case ToolType.ApplePie:
                break;
            case ToolType.MoonRock:
                break;
            case ToolType.Mug:
                break;
            case ToolType.Gun:
                break;
            case ToolType.Button:
                break;
            case ToolType.WaterContainer:
                break;
            default:
                return t.ToString();
        }
    }

    public static Sprite GetSprite(ToolType t)
    {
        Sprite toReturn;

        try
        {
            toReturn = Resources.Load<Sprite>($"Tools/Sprites/{t.ToString()}");
        }
        catch (Exception e)
        {
            toReturn = null;
            Debug.LogError($"Couldn't find sprite for {t.ToString()}");
        }

        return toReturn;
    }
}