using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AddIventoryItem : MonoBehaviour
{
    public ItemScript item;
    public InventoryObject inventory;
    public InventoryUI inventoryUI;
    public void AddItem()
    {
        inventory.AddItem(item, 1);
        inventoryUI.RefreshInventory();
        inventoryUI.UpdateDisplay();
    }

    public void DeleteItem()
    {
        inventory.ReduceItem(item, 1);
        inventoryUI.UpdateDisplay();
    }
}
