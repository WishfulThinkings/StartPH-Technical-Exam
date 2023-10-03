using Firebase.Database;
using Google.MiniJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteFunction : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public PlayerInventory playerInventory;
    public InventoryUI inventoryUI;
    DatabaseReference dbReference;

    private Image _image;
    private AudioSource _audioSource;

    public Color[] colors;
    public Sprite[] changeImage;
    public Image binImage;
    private void Start()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        _image = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>(); 
    }
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
            dbReference.Child("Inventory Items").Child(amountScriptRef.gameObject.name).RemoveValueAsync();
            Debug.Log(amountScriptRef.gameObject.name);
            _audioSource.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = colors[0];
        binImage.sprite = changeImage[0];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = colors[1];
        binImage.sprite = changeImage[1];
    }
}
