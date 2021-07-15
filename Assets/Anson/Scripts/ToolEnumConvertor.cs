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
    PapperBagMask,
    CoffeeMug,
    Apple,
    ApplePie,
    MoonRock,
    Mug,
    Gun,
    Button,
    WaterContainer

};

public static class ToolEnumConvertor
{
    public static string ToName(ToolType t)
    {
        switch (t)
        {
            case (ToolType.FingerOnStick):
                return "Finger On Stick";
            case (ToolType.HammerBig):
                return "Hammer Big";
            case (ToolType.BlowTorch):
                return "Blow Torch";
            case (ToolType.PapperBagMask):
                return "Papper Bag Mask";
            case (ToolType.CoffeeMug):
                return "Coffee Mug";
            case (ToolType.ApplePie):
                return "Apple Pie";
            case (ToolType.MoonRock):
                return "Moon Rock";
            case (ToolType.WaterContainer):
                return "Water Container";
            default:
                return t.ToString();
        }
    }

    public static Sprite GetSprite(ToolType t)
    {
        return null;
    }
}