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
    ItsATrap,
    CoffeeMug,
    Apple,
    ApplePie,
    MoonRock,
    Mug,
    Gun,
    Button,
    WaterContainer,
    Body,
    GasMask,
    Fertiliser,
    GoldenStick,
    CookingKnife,
    CookingSword,
    Screwdriver,
    WaterBucket,
    DogTag,
    Pizza,
    FrozenPizza,
    PepperGrinder,
    KeycardBlue,
    BakedPotato,
    RobotCore


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
            case (ToolType.ItsATrap):
                return "It's A Trap";
            case (ToolType.CoffeeMug):
                return "Coffee Mug";
            case (ToolType.ApplePie):
                return "Apple Pie";
            case (ToolType.MoonRock):
                return "Moon Rock";
            case (ToolType.WaterContainer):
                return "Water Container";
            case (ToolType.GunEmpty):
                return "Empty Gun";
            case ToolType.GasMask:
                return "Gas Mask";
            case ToolType.GoldenStick:
                return "Golden Stick";
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