using UnityEngine;
public class Ingredient : MonoBehaviour
{
    public string ingredientName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CraftingArea"))
        {
            other.GetComponent<CraftingManager>().AddIngredient(ingredientName);
            Destroy(gameObject);
        }
    }
}
