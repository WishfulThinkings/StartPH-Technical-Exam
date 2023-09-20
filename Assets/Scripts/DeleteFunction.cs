using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteFunction : MonoBehaviour, IDropHandler
{
    public PlayerInventory playerInventory;
    public InventoryUI inventoryUI;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            AmountScript amountScriptRef = dropped.GetComponent<AmountScript>();
            Destroy(amountScriptRef.gameObject);
            inventoryUI.inventory.container.RemoveAt(amountScriptRef.inventoryNumber);
            inventoryUI.inventorySlotNumber.RemoveAt(amountScriptRef.inventoryNumber);
            inventoryUI.RefreshInventory();
        }
    }
}
