/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        14/9/2017
 *  Last updated:   14/9/2017
 *
 *  File:           GameData.cs
 *
 *  This class represents the data that is saved to our game.
 *
 *----------------------------------------------------------------*/

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<string> inventory;          // This is the list with names of inventory items that there are to the inventory.
    public List<Sprite> inventoryItems;     // This is the list with images of inventory items that there are to the inventory.
    public List<string> sceneCondition;     // This is the list with scenes that the player has played.

    /// <summary>
    /// The constructor initializes the three lists.
    /// </summary>
    public GameData()
    {
        inventory = new List<string> ();
        inventoryItems = new List<Sprite>();
        sceneCondition = new List<string>();
    }
}
