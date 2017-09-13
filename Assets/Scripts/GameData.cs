using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<string> inventory;
    public List<Sprite> inventoryItems;
    public List<string> sceneCondition;

    public GameData()
    {
        inventory = new List<string> ();
        inventoryItems = new List<Sprite>();
        sceneCondition = new List<string>();
    }
}
