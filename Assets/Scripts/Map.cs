using UnityEngine;

public class Map : MonoBehaviour
{
    private GameObject mapPanel;

    private void Awake()
    {
        mapPanel = GameObject.Find("MapPanel");
        mapPanel.SetActive(false);
    }

    public void ToggleMap()
    {
        if (mapPanel.activeSelf)
            mapPanel.SetActive(false);
        else
            mapPanel.SetActive(true);
    }
}
