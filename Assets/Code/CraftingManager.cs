using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public List<CraftingRecipeSO> recipes;
    public Transform spawnPoint;

    private List<string> currentIngredients = new List<string>();

    public void AddIngredient(string ingredientName)
    {
        currentIngredients.Add(ingredientName);
        CheckRecipe();
    }

    private void CheckRecipe()
    {
        bool recipeMatched = false;

        foreach (var recipe in recipes)
        {
            if (IsMatch(recipe))
            {
                CraftItem(recipe.result);
                currentIngredients.Clear();
                recipeMatched = true;
                break;
            }
        }

        // Reset currentIngredients only if no recipe matched and at least 2 ingredients are added
        if (!recipeMatched && currentIngredients.Count >= 2) //Sesuaikan dengan jumlah kombinasi recipe. Apakah 2 atau mungkin 3
        {
            currentIngredients.Clear();
        }
    }

    private bool IsMatch(CraftingRecipeSO recipe)
    {
        if (currentIngredients.Count != recipe.ingredients.Count)
            return false;

        foreach (var ingredient in recipe.ingredients)
        {
            if (!currentIngredients.Contains(ingredient))
                return false;
        }

        return true;
    }

    private void CraftItem(GameObject result)
    {
        Instantiate(result, spawnPoint.position, spawnPoint.rotation);
    }
}
