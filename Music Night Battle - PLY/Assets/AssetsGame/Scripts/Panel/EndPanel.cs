using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : UIPanel
{
    [SerializeField] private RectTransform rectGameTitle;
    [SerializeField] private RectTransform rectBtnBattle;

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
