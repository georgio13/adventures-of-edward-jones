/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        18/9/2017
 *  Last updated:   18/9/2017
 *
 *  File:           InterractionItemsFunctions.cs
 *
 *  This class implements all the functions of the interractive
 *  items of the game.
 *
 *----------------------------------------------------------------*/

using UnityEngine;

public class InterractionItemsFunctions : MonoBehaviour
{
    /// <summary>
    /// We pass a photography of the item to the inventory.
    /// </summary>
    /// <param name="inventoryImage">The reference to the image that will pass to the inventory.</param>
    public void ThirdSceneInterraction(Sprite inventoryImage)
    {
        Inventory.instance.AddElement("ThirdScene/FrameIcon", inventoryImage);
        DataHandler.instance.gameData.inventory.Add("ThirdScene/FrameIcon");
        DataHandler.instance.SaveData();
    }

    /// <summary>
    /// We pass a photography of the item to the inventory.
    /// </summary>
    /// <param name="inventoryImage">The reference to the image that will pass to the inventory.</param>
    public void FourthSceneInterraction(Sprite inventoryImage)
    {
        Inventory.instance.AddElement("FourthScene/VesselIcon", inventoryImage);
        DataHandler.instance.gameData.inventory.Add("FourthScene/VesselIcon");
        DataHandler.instance.SaveData();
    }
}
