/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        11/9/2017
 *  Last updated:   12/9/2017
 *
 *  File:           Map.cs
 *
 *  This class implements the functions of the map.
 *
 *----------------------------------------------------------------*/

using UnityEngine;

public class Map : MonoBehaviour
{
    private GameObject mapPanel;    // This is the reference to the map panel.

    /// <summary>
    /// On the initialization we take the reference to the map and hide it.
    /// </summary>
    private void Awake()
    {
        mapPanel = GameObject.Find("MapPanel");
        mapPanel.SetActive(false);
    }

    /// <summary>
    /// This function checks if the map is open to close it or else to open it.
    /// </summary>
    public void ToggleMap()
    {
        if (mapPanel.activeSelf)
            mapPanel.SetActive(false);
        else
            mapPanel.SetActive(true);
    }
}
