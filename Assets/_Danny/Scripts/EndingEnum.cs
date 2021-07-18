using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ending{Ending0, Ending1, Ending2, Ending3, Ending4}
public static class EndingEnum 
{
    public static string GetEndingNameFromEnding(Ending ending)
    {
        switch (ending)
        {
            case Ending.Ending0:
                return "Robot Souls";
            case Ending.Ending1:
                return "Put You back together";
            case Ending.Ending2:
                return "a Good Meal";
            case Ending.Ending3:
                return "No Other Choice";
            case Ending.Ending4:
                return "SSSSHHHH its a secret ;)!";
            default:
                return "Ending";
        }
    }

    public static string GetStringFromEnding(Ending ending)
    {
        return $"Ending {(int)ending +1}";
    }
}
