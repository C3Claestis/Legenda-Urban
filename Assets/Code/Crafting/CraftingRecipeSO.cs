using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Crafting Recipe")]
public class CraftingRecipeSO : ScriptableObject
{
    public List<string> ingredients;
    public GameObject result;
}
