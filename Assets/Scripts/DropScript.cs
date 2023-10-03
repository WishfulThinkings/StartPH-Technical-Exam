using Google.MiniJSON;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropScript : MonoBehaviour, IDropHandler
{
    public int slotNumber;
    public InventoryUI inventoryUI;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            AmountScript amountScriptRef = dropped.GetComponent<AmountScript>();
            amountScriptRef.topOfObject = transform;
            inventoryUI.inventory.container[amountScriptRef.inventoryNumber].ChangeInventoryPosition(slotNumber);
            inventoryUI.dbReference.Child("Inventory Items").Child(amountScriptRef.gameObject.name).Child("inventorySlotNumber").SetValueAsync(slotNumber);
            Debug.Log("Transform");
          
        }
    }

}
