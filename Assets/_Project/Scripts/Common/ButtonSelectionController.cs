using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonSelectionController : MonoBehaviour
{
    [SerializeField] private RectTransform[] buttons;
    [SerializeField] private float highlightScale = 1.1f;
    [SerializeField] private float transitionSpeed = 10f;

    private int selectedIndex = -1;

    private void Update()
    {
        if(Keyboard.current != null)
        {
            if(Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                MoveSelection(1);
            }
            else if(Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                MoveSelection(-1);
            }
            else if(Keyboard.current.enterKey.wasPressedThisFrame)
            {
                ClickSelectedButton();
            }
        }

        UpdateButtonScale();
    }

    private void MoveSelection(int direction)
    {
        if (buttons.Length == 0)
            return;
        
        // 첫 입력시 아래방향키는 0번 Index, 윗방향키면 마지막 Index를 가르킴
        if (selectedIndex == -1)
        {
            selectedIndex = direction > 0 ? 0 : buttons.Length - 1;
            return;
        }

        selectedIndex = (selectedIndex + direction + buttons.Length) % buttons.Length;
    }

    public void SelectButton(int index)
    {
        selectedIndex = index;
    }

    private void ClickSelectedButton()
    {
        if (selectedIndex == -1) return;

        Button button = buttons[selectedIndex].GetComponent<Button>();

        if(button != null && button.interactable)
        {
            button.onClick.Invoke();
        }
    }

    private void UpdateButtonScale()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            // 선택된 버튼은 크기 키우기, 다른 버튼들은 기존크기로
            Vector3 targetScale = i == selectedIndex 
                ? Vector3.one * highlightScale : Vector3.one;

            buttons[i].localScale = Vector3.Lerp(
                buttons[i].localScale,
                targetScale,
                Time.unscaledDeltaTime * transitionSpeed
            );
        }
    }
}
