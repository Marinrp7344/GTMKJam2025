using UnityEngine;
using UnityEngine.EventSystems;
public class WeaponSlotUI : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerUpHandler,IPointerExitHandler
{
    public WeaponWheelUI weaponManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        weaponManager.heldSlot = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        weaponManager.hoveringSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        weaponManager.hoveringSlot = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        weaponManager.heldSlot = null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
