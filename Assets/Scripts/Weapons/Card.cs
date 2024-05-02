using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    private bool isDragging = false;
    [SerializeField] Weapon realtiveWeapon;
    [HideInInspector] public RectTransform rectTrans;

    private CharacterPlayer.WeaponSlots endingSlot;

    private void Start()
    {
        rectTrans = GetComponent<RectTransform>();
    }

    public void __init__()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!isDragging)
            {
                isDragging = true;
                CardManager.instance.currentSelectedCard = this;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isDragging)
            {
                Vector3 globalMousePos;
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTrans, eventData.position, eventData.pressEventCamera, out globalMousePos))
                {
                    rectTrans.position = globalMousePos;
                }
                else
                {
                    print("111");
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isDragging)
            {
                //End Drag
                isDragging = false;
                //if(rectTrans.position)
                CardManager.instance.currentSelectedCard = null;
                FindFirstObjectByType(typeof(WeaponSlotPlaceHolder));
                WeaponActivate(CharacterPlayer.WeaponSlots.Up);
            }
        }
    }
    private void WeaponActivate(CharacterPlayer.WeaponSlots slot)
    {
        Instantiate(realtiveWeapon);  // Add to the correct slot. 
        CharacterPlayer.instance.OnChangingWeapon(slot, true);
    }
}
