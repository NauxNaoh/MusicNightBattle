using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    private RectTransform rectTransform;
    private Button btnBattle;

    protected ButtonType buttonType;
    public ButtonType ButtonType => buttonType;

    public Vector2 GetPosSpawnNote()
    {
        return new Vector2(rectTransform.position.x, 1080);
    }

    public virtual void Initialized()
    {
        btnBattle = GetComponent<Button>();
        if (btnBattle == null)
            Debug.LogError($"BattleButton {buttonType} need add component Button");
        RegisterEvent();
        rectTransform = GetComponent<RectTransform>();
    }

    void RegisterEvent()
    {
        btnBattle.onClick.AddListener(ClickedButton);
    }
    void UnregisterEvent()
    {
        btnBattle.onClick.RemoveListener(ClickedButton);
    }

    void ClickedButton()
    {

    }
    private void OnDestroy()
    {
        UnregisterEvent();
    }
}
public enum ButtonType
{
    None = 0,
    Left = 1,
    Down = 2,
    Up = 3,
    Right = 4,
}
