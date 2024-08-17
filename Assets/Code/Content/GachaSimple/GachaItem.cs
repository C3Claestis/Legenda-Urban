using UnityEngine;

[System.Serializable]
public class GachaItem
{
    public string itemName;
    public Sprite image;
    public int starRating; // 3, 4 or 5
    public Color color;
    public bool isLimited; // True if the item is a limited character
}
