using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodNameGenerator : MonoBehaviour {

    public List<string> headNames;
    public List<string> torsoNames;
    public List<string> legsNames;
    public List<string> stickNames;

    public string GenerateGodName(string head, string torso, string legs, string stick)
    {
        string name = FindName(head, headNames) + " " + FindName(torso, torsoNames) + " the " + FindName(legs, legsNames) + " " + FindName(stick, stickNames) ;
        return name;
    }

    string FindName(string totemPartName, List<string> nameList)
    {
        int i = 0;

        switch (totemPartName)
        {
            case "Rapace":
                i = 0;
                break;
            case "Lion":
                i = 1;
                break;
            case "Vache":
                i = 2;
                break;
            case "Elephant":
                i = 3;
                break;
            case "Ananas":
                i = 4;
                break;
            case "Pomme":
                i = 5;
                break;
            case "Champignon":
                i = 6;
                break;
            case "Ronce":
                i = 7;
                break;
            case "Amour":
                i = 8;
                break;
            case "Rire":
                i = 9;
                break;
            case "Haine":
                i = 10;
                break;
            case "Tristesse":
                i = 11;
                break;
        }

        return nameList[i];
    }
}
