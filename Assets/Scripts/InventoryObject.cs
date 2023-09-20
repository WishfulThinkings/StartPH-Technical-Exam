using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
   public List<InventorySlot> container = new List<InventorySlot>();
   public void AddItem(ItemScript _item, int _amount)
    {
        bool hastItem = false;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == _item)
            {
                
                container[i].AddAmount(_amount);
                hastItem = true;
                break;
            }
        }
        if (!hastItem) { container.Add(new InventorySlot(_item, _amount)); }
    }

    public void ReduceItem(ItemScript _item, int _amount) 
    {
        bool canReduce = true;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == _item && canReduce == true)
            {
                if (container[i].amount == 0)
                {
                    
                    canReduce = false;
                    break;
                }

                else { container[i].ReduceAmount(_amount); }
            }
        }

    }
}

[System.Serializable]

public class InventorySlot
{
    public ItemScript item;
    public int amount;
    public InventorySlot(ItemScript _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void ReduceAmount(int value) 
    {
        amount -= value;
    }
}
