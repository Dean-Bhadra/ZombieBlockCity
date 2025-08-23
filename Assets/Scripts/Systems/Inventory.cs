using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // A list to hold the names of the items.
    public List<string> items = new List<string>();

    // A static reference to the Inventory instance to make it easily accessible from other scripts.
    public static Inventory instance;

    void Awake()
    {
        // Set up the singleton instance.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to add an item to the list.
    public void AddItem(string item)
    {
        items.Add(item);
        Debug.Log("Picked up: " + item);
        // We will later add a call here to update the UI.
    }
}