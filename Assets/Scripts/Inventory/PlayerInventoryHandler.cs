using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHandler : MonoBehaviour
{
    private static PlayerInventoryHandler instance;
    public static PlayerInventoryHandler Instance {  get { return instance; } }


    [SerializeField]
    private List<string> collectedItems = new List<string>(); //Construction


    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void AddItem(string item)
    {
        if (collectedItems.Contains(item)) return;

        collectedItems.Add(item);

        Debug.Log($"Collected Item: {item}");
    }

}
