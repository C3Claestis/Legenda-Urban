using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    [SerializeField] List<GachaItem> limited5StarItems;   // Karakter limited 5-star
    [SerializeField] List<GachaItem> weapon5StarItems;    // weapon limited 5-star
    [SerializeField] List<GachaItem> standard5StarItems;  // Karakter standard 5-star
    [SerializeField] List<GachaItem> fourStarItems;       // Karakter 4-star
    [SerializeField] List<GachaItem> threeStarItems;       // Weapon 3-star

    [SerializeField] GameObject rotatorLimited;
    [SerializeField] GameObject rotatorStandard;

    private int pullCountForLimitedCharacter = 0; // Menghitung jumlah pull untuk pity banner limited character
    private int pullCountForWeaponLimited = 0;  // Menghitung jumlah pull untuk pity banner limited weapon
    private int pullCountForStandard = 0;       // Menghitung jumlah pull untuk pity banner standard
    private int pityThreshold = 50;             // Ambang batas pity

    //Character Banner
    public GachaItem PullLimitedCharacter(int pullCount)
    {
        pullCountForLimitedCharacter += pullCount;
        rotatorLimited.SetActive(true);
        // Cek apakah mencapai pity
        if (pullCountForLimitedCharacter >= pityThreshold)
        {
            pullCountForLimitedCharacter = 0;            
            return PullLimited5StarCharacter();
        }

        // Jika tidak, lakukan pull secara acak berdasarkan rarity
        int randomValue = Random.Range(0, 100);

        if (randomValue < 5) // 5% chance untuk 5-star
        {
            return Random.Range(0, 100) < 50 ? PullLimited5StarCharacter() : PullStandard5Star();
        }
        else if (randomValue < 35) // 30% chance untuk 4-star (35 - 5 = 30)
        {
            return PullFourStar();
        }
        else // 65% chance untuk 3-star
        {
            return PullThereStar();
        }
    }

    //Weapon Banner
    public GachaItem PullLimitedWeapon(int pullCount)
    {
        pullCountForWeaponLimited += pullCount;
        rotatorLimited.SetActive(true);
        // Cek apakah mencapai pity
        if (pullCountForWeaponLimited >= pityThreshold)
        {
            pullCountForWeaponLimited = 0;
            return PullLimited5StarWeapon();
        }

        // Jika tidak, lakukan pull secara acak berdasarkan rarity
        int randomValue = Random.Range(0, 100);

        if (randomValue < 5) // 5% chance untuk 5-star
        {
            return Random.Range(0, 100) < 50 ? PullLimited5StarWeapon() : PullStandard5Star();
        }
        else if (randomValue < 35) // 30% chance untuk 4-star (35 - 5 = 30)
        {
            return PullFourStar();
        }
        else // 65% chance untuk 3-star
        {
            return PullThereStar();
        }
    }

    //Standard Banner
    public GachaItem PullStandard(int pullCount)
    {
        pullCountForStandard += pullCount;
        rotatorStandard.SetActive(true);
        // Cek apakah mencapai pity
        if (pullCountForStandard >= pityThreshold)
        {
            pullCountForStandard = 0;
            return PullStandard5Star();
        }

        // Jika tidak, lakukan pull secara acak berdasarkan rarity
        int randomValue = Random.Range(0, 100);

        if (randomValue < 5) // 5% chance untuk 5-star
        {
            return Random.Range(0, 100) < 50 ? PullStandard5Star() : PullStandard5Star();
        }
        else if (randomValue < 35) // 30% chance untuk 4-star (35 - 5 = 30)
        {
            return PullFourStar();
        }
        else // 65% chance untuk 3-star
        {
            return PullThereStar();
        }
    }

    private GachaItem PullLimited5StarCharacter()
    {
        int index = Random.Range(0, limited5StarItems.Count);
        return limited5StarItems[index];
    }
    private GachaItem PullLimited5StarWeapon()
    {
        int index = Random.Range(0, weapon5StarItems.Count);
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
    private GachaItem PullThereStar()
    {
        int index = Random.Range(0, threeStarItems.Count);
        return threeStarItems[index];
    }
}
