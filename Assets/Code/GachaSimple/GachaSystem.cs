using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    public List<GachaItem> limited5StarItems;   // Karakter limited 5-star
    public List<GachaItem> weapon5StarItems;    // weapon limited 5-star
    public List<GachaItem> standard5StarItems;  // Karakter standard 5-star
    public List<GachaItem> fourStarItems;       // Karakter 4-star
    public List<GachaItem> threeStarItems;       // Weapon 3-star

    private int pullCount = 0;                  // Menghitung jumlah pull untuk pity
    private int pityThreshold = 50;             // Ambang batas pity

    void Start()
    {
        // Contoh penggunaan: menarik karakter
        GachaItem pulledItem = Pull();
        Debug.Log("You pulled: " + pulledItem.itemName + " (" + pulledItem.starRating + "*)");
    }

    public GachaItem Pull()
    {
        pullCount++;

        // Cek apakah mencapai pity
        if (pullCount >= pityThreshold)
        {
            pullCount = 0;
            return PullLimited5Star();
        }

        // Jika tidak, lakukan pull secara acak berdasarkan rarity
        int randomValue = Random.Range(0, 100);

        if (randomValue < 5) // 5% chance untuk 5-star
        {
            return Random.Range(0, 100) < 50 ? PullLimited5Star() : PullStandard5Star();
        }
        else // 95% chance untuk 4-star
        {
            return PullFourStar();
        }
    }

    private GachaItem PullLimited5Star()
    {
        int index = Random.Range(0, limited5StarItems.Count);
        return limited5StarItems[index];
    }

    private GachaItem PullStandard5Star()
    {
        int index = Random.Range(0, standard5StarItems.Count);
        return standard5StarItems[index];
    }

    private GachaItem PullFourStar()
    {
        int index = Random.Range(0, fourStarItems.Count);
        return fourStarItems[index];
    }
}
