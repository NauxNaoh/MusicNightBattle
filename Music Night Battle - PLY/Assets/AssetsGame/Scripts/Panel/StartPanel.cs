using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : UIPanel
{
    [SerializeField] private Button btnBattle;
    [SerializeField] private RectTransform rectGameTitle;
    [SerializeField] private RectTransform rectBtnBattle;

    public override void ShowPanel()
    {
        base.ShowPanel();
        btnBattle.onClick.AddListener(OnClickBattleGameButton);
    }

    public override void HidePanel()
    {
        base.HidePanel();
        btnBattle.onClick.RemoveListener(OnClickBattleGameButton);

    }
    void OnClickBattleGameButton()
    {
        GameplayHandler.Instance.StartBattle();
    }

    public override void LandscapeUI()
    {
        rectGameTitle.pivot = new Vector2(0.5f, 1);
        rectGameTitle.sizeDelta = new Vector2(900, 380);
        rectGameTitle.anchoredPosition = new Vector2(0, -50f);

        rectBtnBattle.pivot = new Vector2(0.5f, 0);
        rectBtnBattle.anchoredPosition = new Vector2(0, 160);
    }

    public override void PortraitUI()
    {
        rectGameTitle.pivot = new Vector2(0.5f, 1);
        rectGameTitle.sizeDelta = new Vector2(900, 500);
        rectGameTitle.anchoredPosition = new Vector2(0, -250);

        rectBtnBattle.pivot = new Vector2(0.5f, 0);
        rectBtnBattle.anchoredPosition = new Vector2(0, 300);
    }


}
