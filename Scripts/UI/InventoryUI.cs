using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel; // A reference to our panel.

    void Update()
    {
        // Check if the 'I' key is pressed down.
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Toggle the panel's active state.
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}