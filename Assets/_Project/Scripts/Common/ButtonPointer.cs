using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPointer : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private ButtonSelectionController controller;
    [SerializeField] private int buttonIndex;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"마우스 진입! Index: {buttonIndex}");
        controller.SelectButton(buttonIndex);
    }
}
