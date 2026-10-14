using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PauseSlider : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Toggle toggle;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        toggle.isOn = false;
    }
}