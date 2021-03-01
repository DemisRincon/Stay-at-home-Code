using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntractableObject
{
    private string gameName { get; set; }
    private string Action { get; set; }

   public IntractableObject(string Name)
    {
        gameName = Name;
        switch (Name)
        {
            case "Bed":
                Action = "Dormir";
                break;
            case "Basket":
                Action = "Tirar";
                break;
            case "Computer":
                Action = "Trabajar";
                break;
            default:
                break;
        }
    }


    public string getName()
    {
        return gameName;
    }

    public string getAction()
    {
        return Action;
    }
}
