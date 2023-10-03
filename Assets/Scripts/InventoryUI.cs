using Firebase.Database;
using Google.MiniJSON;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventoryObject inventory;
    public List<GameObject> inventorySlot;
    public Dictionary<InventorySlot, GameObject> inventoryDisplayed = new Dictionary<InventorySlot, GameObject>();

    public List<GameObject> inventorySlotNumber;
    public DatabaseReference dbReference;

    private void Start()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;

        //UpdateDisplay();
        FetchDataFromFirebase();
    }

    public void UpdateDisplay()
    {

        for (int i = 0; i < inventory.container.Count; i++)
        {
            if (inventoryDisplayed.ContainsKey(inventory.container[i]))
            {
                inventoryDisplayed[inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
                dbReference.Child("Inventory Items").Child(inventory.container[i].item.name.ToString()).Child("amount").SetValueAsync(inventory.container[i].amount.ToString());
                RefreshInventory();
            }
            else
            {
                for (int l = 0; l < inventorySlot.Count; l++)
                {

                    if (inventorySlot[l].transform.childCount == 0)
                    {
                        if (inventory.container[i].inventorySlotNumber == 0)
                        {
                            inventory.container[i].inventorySlotNumber = l;
                        }
                        var obj = Instantiate(inventory.container[i].item.prefab, transform.position, Quaternion.identity, inventorySlot[inventory.container[i].inventorySlotNumber].transform);
                        obj.name = inventory.container[i].item.name.ToString();
                        obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString();
                        obj.GetComponentInChildren<AmountScript>().inventoryNumber = l;
                        inventoryDisplayed.Add(inventory.container[i], obj);
                        string json = JsonUtility.ToJson(inventory.container[i]);
                        inventorySlotNumber.Add(obj);
                        dbReference.Child("Inventory Items").Child(inventory.container[i].item.name.ToString()).SetRawJsonValueAsync(json.ToString());
                        RefreshInventory();
                        break;
                    }
                }

            }

        }
    }

    private async void FetchDataFromFirebase()
    {
        var dataSnapshot = await dbReference.Child("Inventory Items").GetValueAsync();

        if (dataSnapshot != null && dataSnapshot.Exists)
        {
            inventory.container.Clear();
            inventoryDisplayed.Clear();

            foreach (var itemSnapshot in dataSnapshot.Children)
            {
                string itemName = itemSnapshot.Key;
                string json = itemSnapshot.GetRawJsonValue();
                InventorySlot inventorySlot = JsonUtility.FromJson<InventorySlot>(json);
                inventory.container.Add(inventorySlot);
                UpdateDisplay();
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
