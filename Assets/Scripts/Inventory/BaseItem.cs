using UnityEngine;

public class BaseItem : MonoBehaviour, Interactable
{
    [SerializeField]
    protected string itemName;

    public void Interact()
    {
        PlayerInventoryHandler.Instance.AddItem(itemName);
        Destroy(gameObject);

    }
}
