using UnityEngine;

public class SlotItem : MonoBehaviour
{
    public int id;
    public string title;
    public string description;
    public Sprite image;

    public SlotItem()
    {
        id = -1;
        title = "";
        description = "";
    }

    public SlotItem(int id, string title, string description, Sprite image)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.image = image;
    }
}
