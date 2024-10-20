using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    [SerializeField] private Image imgGlowArrow;
    private RectTransform rectTransform;
    private Button btnBattle;
    protected ButtonType buttonType;
    public ButtonType ButtonType => buttonType;

    Coroutine hitArrowCoroutine;


    public RectTransform GetRectTransformBattleButton() => rectTransform;
    public virtual void Initialized()
    {
        btnBattle = GetComponent<Button>();
        if (btnBattle == null)
            Debug.LogError($"BattleButton {buttonType} need add component Button");
        RegisterEvent();
        rectTransform = GetComponent<RectTransform>();
        imgGlowArrow.enabled = false;
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
        BattleHandle.Instance.OnClickedBattleArrow(this);
        if (hitArrowCoroutine != null)
        {
            StopCoroutine(hitArrowCoroutine);
        }

        hitArrowCoroutine = StartCoroutine(GlowArrowButton(0.1f));
    }
    private void OnDestroy()
    {
        UnregisterEvent();
    }

    public void HitArrow()
    {
        //vfx song note
    }

    IEnumerator GlowArrowButton(float limitTime)
    {
        imgGlowArrow.enabled = true;
        yield return new WaitForSeconds(limitTime);
        imgGlowArrow.enabled = false;
        hitArrowCoroutine = null;
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
