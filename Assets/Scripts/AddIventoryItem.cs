using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEditor.Progress;

public class AddIventoryItem : MonoBehaviour
{
    public ItemScript item;
    public InventoryObject inventory;
    public InventoryUI inventoryUI;
    public void AddItem()
    {
        inventoryUI.RefreshInventory();
        inventory.AddItem(item, 1);
    }

    public void DeleteItem()
    {
        inventory.ReduceItem(item, 1);

    }
}
