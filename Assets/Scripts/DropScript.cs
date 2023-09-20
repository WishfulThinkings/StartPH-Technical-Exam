using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropScript : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            AmountScript amountScriptRef = dropped.GetComponent<AmountScript>();
            amountScriptRef.topOfObject = transform;
            Debug.Log("Transform");
          
        }
    }

}
