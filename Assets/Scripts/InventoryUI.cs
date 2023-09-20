using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventoryObject inventory;
    public List<GameObject> inventorySlot;
    public Dictionary<InventorySlot, GameObject> inventoryDisplayed = new Dictionary<InventorySlot, GameObject>();
    
    public List<GameObject> inventorySlotNumber;

    void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {

        for (int i = 0; i < inventory.container.Count; i++)
        {
            if (inventoryDisplayed.ContainsKey(inventory.container[i]))
            {
                inventoryDisplayed[inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
                RefreshInventory();
            }
            else
            {
                for (int l = 0; l < inventorySlot.Count; l++)
                {
                    if (inventorySlot[l].transform.childCount == 0)
                    {
                        var obj = Instantiate(inventory.container[i].item.prefab, transform.position, Quaternion.identity, inventorySlot[l].transform);
                        obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString();
                        obj.GetComponentInChildren<AmountScript>().inventoryNumber = l ;
                        inventoryDisplayed.Add(inventory.container[i], obj);
                        inventorySlotNumber.Add(obj);
                        RefreshInventory();
                        break;
                    }
                }

            }

        }
    }

    public void RefreshInventory()
    {
        for (int i = 0; i < inventorySlotNumber.Count; i++)
        {
            AmountScript number = inventorySlotNumber[i].GetComponent<AmountScript>();
            number.inventoryNumber = i;
        }
    }
}
